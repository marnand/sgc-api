using sgc.Domain.Dtos.User;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using sgc.Domain.Interfaces.Services;
using System.Net;

namespace sgc.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenHandlers _tokenHandlers;
    private readonly ICollaboratorRepository _collaboratorRepository;

    public AuthenticationService(ITokenHandlers tokenHandlers, 
        ICollaboratorRepository collaboratorRepository)
    {
        _tokenHandlers = tokenHandlers;
        _collaboratorRepository = collaboratorRepository;
    }

    public async Task<ResultData<string>> Login(LoginRequest req)
    {
        var resultUser = await _collaboratorRepository.GetByUsername(req.Username);
        if (!resultUser.IsSuccess)
            return ResultData<string>.Failure(resultUser.Message, resultUser.StatusCode);

        var user = resultUser.Data!;

        var (isValid, message) = PasswordHandler.VerifyPassword(req.Password, user.Password);
        if (!isValid) return ResultData<string>.Failure(message, HttpStatusCode.Unauthorized);

        var token = _tokenHandlers.GenerateToken(user.Id.ToString(), user.Email, user.RoleId);
        return ResultData<string>.Success(token);
    }
}
