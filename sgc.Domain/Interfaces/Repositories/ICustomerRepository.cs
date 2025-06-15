using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<ResultData<bool>> Create(Customer customer);
    Task<ResultData<IEnumerable<Customer>>> GetAll();
}
