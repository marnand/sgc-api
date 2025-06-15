using sgc.Domain.Entities.Validation.Interfaces;

namespace sgc.Domain.Entities.Validation.Basics;

public class RequiredValidator<T>(string fieldName) : IValidator<T>
{
    private readonly string _fieldName = fieldName;

    public IValidationResult Validate(T value)
    {
        if (IsEmpty(value))
        {
            return ValidationResult.Failure($"{_fieldName} não pode ser vazio");
        }

        return ValidationResult.Success();
    }

    private static bool IsEmpty(T value)
    {
        // Verifica se é null
        if (value == null)
            return true;

        // Para strings, verifica se é null, empty ou whitespace
        if (value is string str)
            return string.IsNullOrWhiteSpace(str);

        // Para tipos nullable (int?, DateTime?, etc.)
        if (IsNullableType(typeof(T)) && EqualityComparer<T>.Default.Equals(value, default))
            return true;

        // Para coleções (List, Array, etc.)
        if (value is System.Collections.IEnumerable enumerable and not string)
        {
            return !enumerable.Cast<object>().Any();
        }

        // Para tipos de valor (int, DateTime, bool, etc.) nunca são considerados "vazios"
        // exceto quando são nullable e têm valor null
        if (typeof(T).IsValueType && !IsNullableType(typeof(T)))
            return false;

        // Para outros tipos de referência, considera vazio apenas se for null
        return false;
    }

    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}

// Versões específicas para conveniência (mantendo compatibilidade)
public class RequiredStringValidator(string fieldName) : RequiredValidator<string>(fieldName)
{
}

public class RequiredIntValidator(string fieldName) : RequiredValidator<int?>(fieldName)
{
}

public class RequiredDateTimeValidator(string fieldName) : RequiredValidator<DateTime?>(fieldName)
{
}

public class RequiredGuidValidator(string fieldName) : RequiredValidator<Guid?>(fieldName)
{
}

public class RequiredListValidator<T>(string fieldName) : RequiredValidator<IEnumerable<T>>(fieldName)
{
}
