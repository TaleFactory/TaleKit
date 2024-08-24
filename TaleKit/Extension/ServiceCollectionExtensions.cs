using Microsoft.Extensions.DependencyInjection;

namespace TaleKit.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddImplementingTypes<T>(this IServiceCollection services)
    {
        var types = typeof(T).Assembly.GetTypes()
            .Where(x => typeof(T).IsAssignableFrom(x))
            .Where(x => !x.IsInterface && !x.IsAbstract)
            .Where(x => x.IsPublic);

        foreach (var type in types)
        {
            services.AddTransient(typeof(T), type);
        }

        return services;
    }
}