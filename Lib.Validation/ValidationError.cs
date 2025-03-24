namespace Lib.Validation;

public record ValidationError
{
    public ValidationError(string message)
    {
        Message = message;
    }

    public bool Fatal { get; init; } = false;
    public string Message { get; init; }
}