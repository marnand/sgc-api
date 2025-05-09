using sgc.Domain.Dtos.User;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Services;

public interface IAuthenticationService
{
    Task<ResultData<string>> Login(LoginRequest req);
}
