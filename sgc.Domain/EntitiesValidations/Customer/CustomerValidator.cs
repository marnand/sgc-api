using sgc.Domain.Entities;
using sgc.Domain.Entities.Validation;
using sgc.Domain.Entities.Validation.Basics;
using sgc.Domain.Entities.Validation.Interfaces;

namespace sgc.Domain.EntitiesValidations.Customer;

public class CustomerValidator : IValidator<CustomerValidationContext>
{
    private readonly ObjectValidator<CustomerValidationContext> _validator;

    public CustomerValidator()
    {
        _validator = new ObjectValidator<CustomerValidationContext>()
            .For(c => c.Name, new CompositeValidator<string>()
                .Add(new RequiredStringValidator("Nome"))
                .Add(new MinLengthValidator(4, "Nome"))
                .Add(new MaxLengthValidator(50, "Nome")))
            .For(c => c.Type, new CompositeValidator<TypeCustomerEnum>()
                .Add(new RequiredValidator<TypeCustomerEnum>("Tipo Cliente")))
            .For(c => c.DocumentType, new CompositeValidator<DocumentTypeEnum>()
                .Add(new RequiredValidator<DocumentTypeEnum>("Tipo Documento")))
            .For(c => c.Email, new CompositeValidator<string>()
                .Add(new RequiredStringValidator("Email"))
                .Add(new EmailValidator()))
            .For(c => c.Phone, new CompositeValidator<string>()
                .Add(new RequiredStringValidator("Telefone")));
    }

    public IValidationResult Validate(CustomerValidationContext value) => _validator.Validate(value);
}
