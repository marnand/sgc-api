using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Repositories;

public interface IBankDetailsRepository : IBaseRepository
{
    Task<ResultData<bool>> Create(BankDetails bankDetails);
    Task<ResultData<BankDetails?>> GetByCustomerId(Guid customerId);
}
