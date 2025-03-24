using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Lib.Validation;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidatorsFromAssembly<T>(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(T));
        var validators = assembly?.DefinedTypes.Where(e => e.ImplementedInterfaces.Contains(typeof(IValidator<>)));
        foreach (var validator in validators ?? [])
        {
            var validatorInterface = validator.GetInterfaces().Where(e => e.GetGenericTypeDefinition() == typeof(IValidator<>)).FirstOrDefault();
            if (validatorInterface != null)
            {
                services.AddSingleton(validatorInterface, validator);
            }
        }

        return services;
    }
}