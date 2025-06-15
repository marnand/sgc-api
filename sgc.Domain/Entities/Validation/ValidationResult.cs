using sgc.Domain.Entities.Validation.Interfaces;

namespace sgc.Domain.Entities.Validation;

public class ValidationResult : IValidationResult
{
    private readonly List<string> _errorMessages;

    public bool IsValid => _errorMessages.Count == 0;
    public IReadOnlyList<string> ErrorMessages => _errorMessages.AsReadOnly();

    public ValidationResult()
    {
        _errorMessages = [];
    }

    public ValidationResult(IEnumerable<string> errorMessages)
    {
        _errorMessages = [.. errorMessages];
    }

    public void AddError(string message)
    {
        _errorMessages.Add(message);
    }

    public void AddErrors(IEnumerable<string> messages)
    {
        _errorMessages.AddRange(messages);
    }

    public static ValidationResult Success() => new();
    public static ValidationResult Failure(string message) => new([message]);
    public static ValidationResult Failure(IEnumerable<string> messages) => new(messages);
}
