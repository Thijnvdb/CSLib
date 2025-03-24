using System.Linq.Expressions;

namespace Lib.Validation;

public class IntValidationRuleBuilder<T> : ValidationRuleBuilder<T, int>
{
    public IntValidationRuleBuilder(Expression<Func<T, int>> propertySelector) : base(propertySelector)
    {
    }

    public IntValidationRuleBuilder<T> Min(int minimumValue)
    {
        Should((parent, prop) => prop >= minimumValue, $"{PropertyName} should be at least {minimumValue}");
        return this;
    }

    public IntValidationRuleBuilder<T> Max(int maximumValue)
    {
        Should((parent, prop) => prop <= maximumValue, $"{PropertyName} should be at most {maximumValue}");
        return this;
    }

    public IntValidationRuleBuilder<T> Length(Range range)
    {
        Should((parent, prop) => range.Start.Value <= prop && prop < range.End.Value, $"{PropertyName} should be in range [{range.Start.Value}..{range.End.Value}]");
        return this;
    }
}