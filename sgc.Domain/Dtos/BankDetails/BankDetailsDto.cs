using sgc.Domain.Entities;

namespace sgc.Domain.Dtos.BankDetails;

public record class BankDetailsDto(
    string Bank,
    string Agency,
    string Account,
    AccountTypeEnum AccountType,
    DateTime? DeactivatedAt
);
