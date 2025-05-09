using sgc.Domain.Dtos.Collaborator;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Services;

public interface ICollaboratorService
{
    Task<ResultData<bool>> Register(CollaboratorDto dto);
    Task<ResultData<GetUserResponse>> GetByToken(string token);
}
