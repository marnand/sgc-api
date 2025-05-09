using sgc.Domain.Dtos.Collaborator;
using sgc.Domain.Dtos.User;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Domain.Interfaces.Services;

namespace sgc.Application.Services;

public class CollaboratorService : ICollaboratorService
{
    private readonly ITokenHandlers _tokenHandlers;
    private readonly ICollaboratorRepository _collaboratorRepository;

    public CollaboratorService(ITokenHandlers tokenHandlers, ICollaboratorRepository collaboratorRepository)
    {
        _tokenHandlers = tokenHandlers;
        _collaboratorRepository = collaboratorRepository;
    }

    public async Task<ResultData<bool>> Register(CollaboratorDto dto)
    {
        var result = new Collaborator().Create(dto.Username, dto.Password, dto.Name, dto.Email);
        if (!result.IsSuccess)
            return ResultData<bool>.Failure(result.Message, result.StatusCode);

        var resultCreate = await _collaboratorRepository.Create(result.Data!);
        return resultCreate;
    }

    public async Task<ResultData<GetUserResponse>> GetByToken(string token)
    {
        var resultToken = _tokenHandlers.GetUserFromToken(token);
        if (!resultToken.IsSuccess)
            return ResultData<GetUserResponse>.Failure(resultToken.Message, resultToken.StatusCode);

        var resultCollaborator = await _collaboratorRepository.GetByEmail(resultToken.Data.CollaboratorEmail);
        if (!resultCollaborator.IsSuccess)
            return ResultData<GetUserResponse>.Failure(resultCollaborator.Message, resultCollaborator.StatusCode);

        return ResultData<GetUserResponse>.Success(new GetUserResponse().Mapping(resultCollaborator.Data!));
    }
}
