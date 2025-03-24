namespace Lib.Validation;

public record ValidationResult
{
    public bool IsValid => ValidationErrors?.Any() ?? true;
    public ICollection<ValidationError>? ValidationErrors { get; init; }
}