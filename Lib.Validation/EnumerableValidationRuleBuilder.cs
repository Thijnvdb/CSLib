using System.Linq.Expressions;

namespace Lib.Validation;

public class EnumerableValidationRuleBuilder<T, TProp> : ValidationRuleBuilder<T, IEnumerable<TProp>>
{
    public EnumerableValidationRuleBuilder(Expression<Func<T, IEnumerable<TProp>>> propertySelector) : base(propertySelector)
    {
    }

    public EnumerableValidationRuleBuilder<T, TProp> Length(Range range)
    {
        Should((parent, prop) => range.Start.Value <= prop.Count() && prop.Count() < range.End.Value, $"{PropertyName} length should be in range [{range.Start.Value}..{range.End.Value}]");
        return this;
    }

    public EnumerableValidationRuleBuilder<T, TProp> Empty()
    {
        Should((parent, prop) => !prop.Any(), $"{PropertyName} should be empty");
        return this;
    }

    public EnumerableValidationRuleBuilder<T, TProp> NotEmpty()
    {
        Should((parent, prop) => prop.Any(), $"{PropertyName} cannot be empty");
        return this;
    }

    public EnumerableValidationRuleBuilder<T, TProp> Contains(TProp element)
    {
        Should((parent, prop) => prop.Contains(element), $"{PropertyName} did not contain '{element}'");
        return this;
    }

    public EnumerableValidationRuleBuilder<T, TProp> DoesNotContain(TProp element)
    {
        Should((parent, prop) => !prop.Contains(element), $"{PropertyName} should not contain '{element}'");
        return this;
    }

    public EnumerableValidationRuleBuilder<T, TProp> All(Expression<Func<T, TProp, bool>> predicate)
    {
        Should((parent, prop) => prop.All(element => predicate.Compile()(parent, element)), $"{PropertyName} elements did not match the predicate '{predicate}'");
        return this;
    }

    public EnumerableValidationRuleBuilder<T, TProp> Any(Expression<Func<T, TProp, bool>> predicate)
    {
        Should((parent, prop) => prop.Any(element => predicate.Compile()(parent, element)), $"no element in '{PropertyName}' matched the predicate '{predicate}'");
        return this;
    }
}