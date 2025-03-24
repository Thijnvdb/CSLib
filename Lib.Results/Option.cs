namespace Lib.Results;

public abstract record Option<T>
{
    public sealed record Some(T Value) : Option<T>;
    public sealed record None() : Option<T>;

    public T GetValueOr(T other)
    {
        return this is Some(T value) ? value : other;
    }
}