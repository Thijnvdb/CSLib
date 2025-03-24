using System.Linq.Expressions;

namespace Lib.Validation;

public interface IValidationRule<T>
{
    ValidationError? Validate(T input);
}

public record ValidationRule<T> : IValidationRule<T>
{
    public Func<T, bool> Condition { get; init; } = (_) => true;
    private readonly Expression<Func<T, ValidationError?>> _rule;

    public ValidationRule(Expression<Func<T, ValidationError?>> rule)
    {
        _rule = rule;
    }

    public ValidationError? Validate(T input)
    {
        return _rule.Compile()(input);
    }
}

public record ValidationRule<T, TProp> : IValidationRule<T>
{
    public Func<T, bool> Condition { get; init; } = (_) => true;
    private readonly Expression<Func<T, TProp>> _selector;
    private readonly Expression<Func<T, TProp, ValidationError?>> _rule;

    public ValidationRule(Expression<Func<T, TProp>> propertySelector, Expression<Func<T, TProp, ValidationError?>> rule)
    {
        _selector = propertySelector;
        _rule = rule;
    }

    public ValidationError? Validate(T input)
    {
        return _rule.Compile()(input, _selector.Compile()(input));
    }
}