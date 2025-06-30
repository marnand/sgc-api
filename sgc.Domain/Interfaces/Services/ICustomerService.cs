using sgc.Domain.Dtos.Customer;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<ResultData<bool>> Register(RegisterCustomerDto dto);
    Task<ResultData<IEnumerable<Customer>>> GetAll();
}
