using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Lib.Validation;

public class StringValidationRuleBuilder<T> : ValidationRuleBuilder<T, string>
{
    public StringValidationRuleBuilder(Expression<Func<T, string>> propertySelector) : base(propertySelector)
    {
    }

    public StringValidationRuleBuilder<T> NotNullOrEmpty()
    {
        Should((parent, prop) => !string.IsNullOrEmpty(prop), $"{PropertyName} cannot be null or empty");
        return this;
    }

    public StringValidationRuleBuilder<T> MinLength(int minimalLength)
    {
        Should((parent, prop) => prop.Length >= minimalLength, $"{PropertyName} length should be at least {minimalLength} characters long");
        return this;
    }

    public StringValidationRuleBuilder<T> MaxLength(int maximumLength)
    {
        Should((parent, prop) => prop.Length <= maximumLength, $"{PropertyName} length should be no more than {maximumLength} characters long");
        return this;
    }

    public StringValidationRuleBuilder<T> Length(Range range)
    {
        Should((parent, prop) => range.Start.Value <= prop.Length && prop.Length < range.End.Value, $"{PropertyName} length should be in range [{range.Start.Value}..{range.End.Value}]");
        return this;
    }

    public StringValidationRuleBuilder<T> Contains(string subString)
    {
        Should((parent, prop) => !prop.Contains(subString), $"{PropertyName} did not contain {subString}");
        return this;
    }

    public StringValidationRuleBuilder<T> Matches(string regex)
    {
        var regexPattern = new Regex(regex);
        Should((parent, prop) => !regexPattern.IsMatch(prop), $"{PropertyName} did not match pattern {regex}");
        return this;
    }
}