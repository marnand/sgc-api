using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Repositories;

public interface ICollaboratorRepository
{
    Task<ResultData<bool>> Create(Collaborator collaborator);
    Task<ResultData<Collaborator>> GetByUsername(string username);
    Task<ResultData<Collaborator>> GetByEmail(string email);
}
