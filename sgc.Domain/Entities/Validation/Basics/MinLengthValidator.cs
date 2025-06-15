using sgc.Domain.Entities.Validation.Interfaces;

namespace sgc.Domain.Entities.Validation.Basics;

public class MinLengthValidator(int minLength, string fieldName) : IValidator<string>
{
    private readonly int _minLength = minLength;
    private readonly string _fieldName = fieldName;

    public IValidationResult Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
            return ValidationResult.Success(); // Deixa a validação de required para RequiredValidator

        return value.Length < _minLength
            ? ValidationResult.Failure($"{_fieldName} deve conter pelo menos {_minLength} caracteres")
            : ValidationResult.Success();
    }
}
