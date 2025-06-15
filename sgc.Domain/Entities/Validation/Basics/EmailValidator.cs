using sgc.Domain.Entities.Validation.Interfaces;
using System.Text.RegularExpressions;

namespace sgc.Domain.Entities.Validation.Basics;

public class EmailValidator : IValidator<string>
{
    private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public IValidationResult Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ValidationResult.Success(); // Deixa a validação de required para RequiredValidator

        return !Regex.IsMatch(value, EmailPattern)
            ? ValidationResult.Failure("Email inválido")
            : ValidationResult.Success();
    }
}
