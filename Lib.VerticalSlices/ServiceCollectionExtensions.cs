using static Lib.VerticalSlices.ReflectionUtils;

namespace Lib.VerticalSlices;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSliceDependenciesFromAssembly<T>(this IServiceCollection services)
    {
        var slices = GetSlicesFromAssembly<T>();
        foreach (var slice in slices)
        {
            slice.RegisterDependencies(services);
        }

        return services;
    }
}