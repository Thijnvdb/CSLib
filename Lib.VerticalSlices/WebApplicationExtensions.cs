using static Lib.VerticalSlices.ReflectionUtils;

namespace Lib.VerticalSlices;

public static class WebApplicationExtensions
{
    public static WebApplication AddSliceEndpointsFromAssembly<T>(this WebApplication app)
    {
        var slices = GetSlicesFromAssembly<T>();

        foreach (var slice in slices)
        {
            slice.RegisterEndpoints(app);
        }

        return app;
    }
}