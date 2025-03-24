public interface ISlice
{
    public void RegisterEndpoints(WebApplication app);
    public void RegisterDependencies(IServiceCollection services);
}