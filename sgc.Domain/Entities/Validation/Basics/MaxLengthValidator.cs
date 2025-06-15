using sgc.Domain.Entities.Validation.Interfaces;

namespace sgc.Domain.Entities.Validation.Basics;

public class MaxLengthValidator(int maxLength, string fieldName) : IValidator<string>
{
    private readonly int _maxLength = maxLength;
    private readonly string _fieldName = fieldName;

    public IValidationResult Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
            return ValidationResult.Success();

        return value.Length > _maxLength
            ? ValidationResult.Failure($"{_fieldName} não pode exceder {_maxLength} caracteres")
            : ValidationResult.Success();
    }
}
