namespace Lib.Results;

public record Error
{
    public int Code { get; init; }
    public Exception? Exception { get; init; }
    public string Message { get; init; } = "An error occurred";

    public Exception AsException() => Exception ?? new Exception(Message);
    public static Error Generic(string message) => new Error() { Message = message };
    public static Error FromException(Exception exception) => new Error() { Exception = exception, Message = $"An exception occurred: {exception.Message}" };
}