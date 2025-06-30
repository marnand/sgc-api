using System;
using sgc.Domain.Entities.Validation;
using sgc.Domain.Entities.Validation.Basics;
using sgc.Domain.Entities.Validation.Interfaces;

namespace sgc.Domain.EntitiesValidations.Address;

public class AddressValidator : IValidator<AddressValidationContext>
{
    private readonly ObjectValidator<AddressValidationContext> _validator;

    public AddressValidator()
    {
        _validator = new ObjectValidator<AddressValidationContext>()
          .For(c => c.Street, new CompositeValidator<string>()
            .Add(new RequiredStringValidator("Rua"))
            .Add(new MinLengthValidator(3, "Rua"))
            .Add(new MaxLengthValidator(100, "Rua")))
          .For(c => c.EstablishmentNumber, new CompositeValidator<string>()
            .Add(new RequiredStringValidator("Número Estabelecimento")))
          .For(c => c.Neighborhood, new CompositeValidator<string>()
            .Add(new RequiredStringValidator("Bairro"))
            .Add(new MinLengthValidator(3, "Bairro"))
            .Add(new MaxLengthValidator(50, "Bairro")))
          .For(c => c.City, new CompositeValidator<string>()
            .Add(new RequiredStringValidator("Cidade"))
            .Add(new MinLengthValidator(3, "Cidade"))
            .Add(new MaxLengthValidator(50, "Cidade")))
          .For(c => c.State, new CompositeValidator<string>()
            .Add(new RequiredStringValidator("Estado"))
            .Add(new MinLengthValidator(2, "Estado"))
            .Add(new MaxLengthValidator(2, "Estado")))
          .For(c => c.ZipCode, new CompositeValidator<string>()
            .Add(new RequiredStringValidator("CEP")));
    }


    public IValidationResult Validate(AddressValidationContext value) => _validator.Validate(value);
}
