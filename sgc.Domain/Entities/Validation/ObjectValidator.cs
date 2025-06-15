using sgc.Domain.Entities.Validation.Interfaces;
using System.Linq.Expressions;

namespace sgc.Domain.Entities.Validation;

// Classe para validação de objetos complexos
public class ObjectValidator<T>
{
    private readonly Dictionary<string, Func<T, IValidationResult>> _propertyValidators;

    public ObjectValidator()
    {
        _propertyValidators = [];
    }

    public ObjectValidator<T> For<TProperty>(
        Expression<Func<T, TProperty>> propertyExpression,
        IValidator<TProperty> validator)
    {
        var propertyName = GetPropertyName(propertyExpression);
        _propertyValidators[propertyName] = obj =>
        {
            var propertyValue = propertyExpression.Compile()(obj);
            return validator.Validate(propertyValue);
        };
        return this;
    }

    public ObjectValidator<T> For<TProperty>(
        Expression<Func<T, TProperty>> propertyExpression,
        Func<TProperty, IValidationResult> validationFunc)
    {
        var propertyName = GetPropertyName(propertyExpression);
        _propertyValidators[propertyName] = obj =>
        {
            var propertyValue = propertyExpression.Compile()(obj);
            return validationFunc(propertyValue);
        };
        return this;
    }

    public IValidationResult Validate(T obj)
    {
        var result = new ValidationResult();

        foreach (var validator in _propertyValidators.Values)
        {
            var validationResult = validator(obj);
            if (!validationResult.IsValid)
            {
                result.AddErrors(validationResult.ErrorMessages);
            }
        }

        return result;
    }

    private string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
    {
        if (propertyExpression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
        throw new ArgumentException("Expression must be a property access", nameof(propertyExpression));
    }
}
