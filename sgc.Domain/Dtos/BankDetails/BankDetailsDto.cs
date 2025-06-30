using sgc.Domain.Entities;

namespace sgc.Domain.Dtos.BankDetails;

public record class BankDetailsDto(
    Guid? CustomerId,
    string Bank,
    string Agency,
    string Account,
    AccountTypeEnum AccountType
);
