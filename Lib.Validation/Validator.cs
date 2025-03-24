using System.Linq.Expressions;

namespace Lib.Validation;

public interface IValidator<T>
{
    ValidationResult Validate(T input);
}

public abstract class Validator<T> : IValidator<T>
{
    protected abstract IEnumerable<IValidationRule<T>> Rules { get; }

    public virtual ValidationResult Validate(T input)
    {
        ICollection<ValidationError> errors = [];
        foreach (var rule in Rules)
        {
            var error = rule.Validate(input);
            if (error != null)
            {
                errors.Add(error);
            }
        }

        return new()
        {
            ValidationErrors = errors
        };
    }

    public virtual ValidationRuleBuilder<T> This()
    {
        return new ValidationRuleBuilder<T>();
    }

    public virtual StringValidationRuleBuilder<T> Property(Expression<Func<T, string>> selector)
    {
        return new StringValidationRuleBuilder<T>(selector);
    }

    public virtual IntValidationRuleBuilder<T> Property(Expression<Func<T, int>> selector)
    {
        return new IntValidationRuleBuilder<T>(selector);
    }

    public virtual EnumerableValidationRuleBuilder<T, TProp> Property<TProp>(Expression<Func<T, IEnumerable<TProp>>> selector)
    {
        return new EnumerableValidationRuleBuilder<T, TProp>(selector);
    }

    public virtual ValidationRuleBuilder<T, TProp> Property<TProp>(Expression<Func<T, TProp>> selector)
    {
        return new ValidationRuleBuilder<T, TProp>(selector);
    }
}
