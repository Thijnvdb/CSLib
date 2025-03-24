public interface IEndpoint<TRequest, TResponse>
{
    public Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}