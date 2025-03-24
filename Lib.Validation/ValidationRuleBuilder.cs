using System.Collections;
using System.Linq.Expressions;

namespace Lib.Validation;


public class ValidationRuleBuilder<T> : IEnumerable<ValidationRule<T>>
{
    private readonly ICollection<ValidationRule<T>> _rules = [];

    public virtual ValidationRuleBuilder<T> Should(Expression<Func<T, bool>> expression, string? failMessage = null)
    {
        var message = failMessage ?? $"Rule '{expression}' not satisfied";
        _rules.Add(new ValidationRule<T>((parent) => expression.Compile()(parent) ? null : new ValidationError(message)));
        return this;
    }

    public IEnumerator<ValidationRule<T>> GetEnumerator()
    {
        return _rules.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<ValidationRule<T>> When(Func<T, bool> condition)
    {
        return _rules.Select(rule => rule with { Condition = condition });
    }
}

public class ValidationRuleBuilder<T, TProp> : IEnumerable<ValidationRule<T, TProp>>
{
    protected string PropertyName => _selector.Body.ToString();
    private readonly Expression<Func<T, TProp>> _selector;
    private readonly ICollection<ValidationRule<T, TProp>> _rules = [];

    public ValidationRuleBuilder(Expression<Func<T, TProp>> selector)
    {
        _selector = selector;
    }

    public virtual ValidationRuleBuilder<T, TProp> NotEqual(TProp other)
    {
        Should((parent, prop) => prop == null || !prop.Equals(other), $"{PropertyName} should not be equal to {other}");
        return this;
    }

    public virtual ValidationRuleBuilder<T, TProp> Equal(TProp other)
    {
        Should((parent, prop) => prop != null && prop.Equals(other), $"{PropertyName} is not equal to {other}");
        return this;
    }

    public virtual ValidationRuleBuilder<T, TProp> NotNull()
    {
        Should((parent, prop) => prop != null, $"{PropertyName} cannot be null");
        return this;
    }

    public virtual ValidationRuleBuilder<T, TProp> Should(Expression<Func<TProp, bool>> expression, string? failMessage = null)
    {
        return Should((parent, prop) => expression.Compile()(prop), failMessage);
    }

    public virtual ValidationRuleBuilder<T, TProp> Should(Expression<Func<T, TProp, bool>> expression, string? failMessage = null)
    {
        var message = failMessage ?? $"Rule '{expression}' not satisfied by property '{PropertyName}'";
        _rules.Add(new ValidationRule<T, TProp>(_selector, (parent, property) => expression.Compile()(parent, property) ? null : new ValidationError(message)));
        return this;
    }

    public IEnumerator<ValidationRule<T, TProp>> GetEnumerator()
    {
        return _rules.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<ValidationRule<T, TProp>> When(Func<T, bool> condition)
    {
        return _rules.Select(rule => rule with { Condition = condition });
    }
}