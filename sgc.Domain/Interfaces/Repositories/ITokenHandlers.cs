using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Repositories;

public interface ITokenHandlers
{
    string GenerateToken(string userId, string email, RoleEnum role);
    ResultData<(string CollaboratorId, string CollaboratorEmail)> GetUserFromToken(string token);
}
