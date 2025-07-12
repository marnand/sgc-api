using sgc.Domain.Dtos.Customer;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<ResultData<bool>> Register(CompleteCustomerDto dto);
    Task<ResultData<IEnumerable<CompleteCustomerDto>>> GetAllInfo();
}
