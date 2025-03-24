namespace Lib.Results;

public static class ResultTypes
{
    public static Result<T, Error> Ok<T>(T data)
    {
        return new Result<T, Error>.Ok(data);
    }
    public static Result<T, TError> Ok<T, TError>(T data)
    {
        return new Result<T, TError>.Ok(data);
    }
    public static Result<T, Error> Err<T>(Error error)
    {
        return new Result<T, Error>.Err(error);
    }
    public static Result<T, TError> Err<T, TError>(TError error)
    {
        return new Result<T, TError>.Err(error);
    }
}