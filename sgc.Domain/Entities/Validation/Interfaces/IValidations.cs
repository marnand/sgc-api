namespace sgc.Domain.Entities.Validation.Interfaces;

// Interface para resultado de validação
public interface IValidationResult
{
    bool IsValid { get; }
    IReadOnlyList<string> ErrorMessages { get; }
}

// Interface para validador individual
public interface IValidator<T>
{
    IValidationResult Validate(T value);
}

// Interface para validador de contexto (quando precisa de múltiplos valores)
public interface IContextValidator<T>
{
    IValidationResult Validate(T context);
}
