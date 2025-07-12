using sgc.Domain.Dtos.BankDetails;
using sgc.Domain.Entities.Handlers;

namespace sgc.Domain.Interfaces.Services;

public interface IBankDetailsService
{
    Task<ResultData<bool>> Register(BankDetailsDto dto);
}
