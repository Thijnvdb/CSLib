using System.Reflection;

namespace Lib.Utility;

public static class Reflection
{
    /// <summary>
    /// Check if <paramref name="type"/> implements <typeparamref name="TInterface"/>
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static bool Implements<TInterface>(this Type type) where TInterface : class
    {
        var interfaceType = typeof(TInterface);

        if (!interfaceType.IsInterface)
            throw new InvalidOperationException("Only interfaces can be implemented.");

        return interfaceType.IsAssignableFrom(type);
    }

    /// <summary>
    /// Get all types in the same assembly as <typeparamref name="TAssemblyType"/>, which implement interface <typeparamref name="TInterface"/>
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="TAssemblyType"></typeparam>
    /// <returns></returns>
    public static ICollection<TypeInfo> GetTypesImplementingInterfaceFromAssembly<TInterface, TAssemblyType>() where TInterface : class
    {
        var assembly = Assembly.GetAssembly(typeof(TAssemblyType));
        return assembly?.DefinedTypes.Where(e => e.Implements<TInterface>()).ToList() ?? [];
    }
}
