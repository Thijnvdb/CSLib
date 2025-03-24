namespace Lib.Results;

public record Result<TData> : Result<TData, Error>
{
    public static implicit operator Result<TData>(Error error) => new(error); 
    public static implicit operator Result<TData>(TData data) => new(data); 

    public new sealed record Ok(TData Data) : Result<TData, Error>;

    public new sealed record Err(Error Error) : Result<TData, Error>;
}

public abstract record Result<T, TError> 
{
    public bool IsSuccess(out T data)
    {
        if (this is Ok(T Data))
        {
            data = Data;
            return true;
        }
        else {
            data = default!;
            return false;
        }
    }

    public bool IsError(out TError error)
    {
        if (this is Err(TError err))
        {
            error = err;
            return true;
        } else {
            error = default!;
            return false;
        }
    }

    public void Handle(Action<T> onSuccess, Action<TError> onError)
    {
        if(IsSuccess(out var data)) 
        {
            onSuccess(data);
        } else if (IsError(out var error)) {
            onError(error);
        }
    }

    public sealed record Ok(T Data) : Result<T, TError>;

    public sealed record Err(TError Error) : Result<T, TError>;

    public static implicit operator Result<T, TError>(TError error) => new Err(error); 
    public static implicit operator Result<T, TError>(T data) => new Ok(data); 
}