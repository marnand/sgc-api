using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Repositories;

public interface IAddressRepository : IBaseRepository
{
    Task<ResultData<bool>> Create(Address address);
    Task<ResultData<Address?>> GetByCustomerId(Guid customerId);
}
