using System.Reflection;

namespace Lib.VerticalSlices;

internal static class ReflectionUtils
{
    public static IEnumerable<ISlice> GetSlicesFromAssembly<T>()
    {
        var assembly = Assembly.GetAssembly(typeof(T));
        var slices = assembly?.DefinedTypes.Where(e => e.ImplementedInterfaces.Contains(typeof(ISlice)));

        return slices?.Select(e => e as ISlice).Where(e => e != null).Select(e => e!) ?? [];
    }
}