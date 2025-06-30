using sgc.Domain.Entities;
using sgc.Domain.Entities.Validation;
using sgc.Domain.Entities.Validation.Basics;
using sgc.Domain.Entities.Validation.Interfaces;

namespace sgc.Domain.EntitiesValidations.BankDetails;

public class BankDetailsValidator : IValidator<BankDetailsValidationContext>
{
    private readonly ObjectValidator<BankDetailsValidationContext> _validator;

    public BankDetailsValidator()
    {
        _validator = new ObjectValidator<BankDetailsValidationContext>()
            .For(c => c.Bank, new CompositeValidator<string>()
                .Add(new RequiredStringValidator("Banco"))
                .Add(new MinLengthValidator(3, "Banco"))
                .Add(new MaxLengthValidator(50, "Banco")))
            .For(c => c.Agency, new CompositeValidator<string>()
                .Add(new RequiredStringValidator("Agência"))
                .Add(new MinLengthValidator(2, "Agência"))
                .Add(new MaxLengthValidator(10, "Agência")))
            .For(c => c.Account, new CompositeValidator<string>()
                .Add(new RequiredStringValidator("Conta"))
                .Add(new MinLengthValidator(5, "Conta"))
                .Add(new MaxLengthValidator(20, "Conta")))
            .For(c => c.AccountType, new CompositeValidator<AccountTypeEnum>()
                .Add(new RequiredValidator<AccountTypeEnum>("Tipo de Conta")));
    }

    public IValidationResult Validate(BankDetailsValidationContext value) => _validator.Validate(value);
}
