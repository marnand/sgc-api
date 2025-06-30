using sgc.Domain.Entities;

namespace sgc.Domain.EntitiesValidations.BankDetails;

public record class BankDetailsValidationContext(
    string Bank,
    string Agency,
    string Account,
    AccountTypeEnum AccountType
);
