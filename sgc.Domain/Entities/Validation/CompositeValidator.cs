using sgc.Domain.Entities.Validation.Interfaces;

namespace sgc.Domain.Entities.Validation;

// Validador composto que executa múltiplos validadores
public class CompositeValidator<T> : IValidator<T>
{
    private readonly List<IValidator<T>> _validators;

    public CompositeValidator()
    {
        _validators = [];
    }

    public CompositeValidator<T> Add(IValidator<T> validator)
    {
        _validators.Add(validator);
        return this;
    }

    public IValidationResult Validate(T value)
    {
        var result = new ValidationResult();

        foreach (var validator in _validators)
        {
            var validationResult = validator.Validate(value);
            if (!validationResult.IsValid)
            {
                result.AddErrors(validationResult.ErrorMessages);
            }
        }

        return result;
    }
}
