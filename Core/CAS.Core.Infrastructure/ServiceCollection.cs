using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Linq;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatRHandlersFromNamespace(this IServiceCollection services, string namespacePrefix)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.StartsWith(namespacePrefix) && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

                foreach (var @interface in interfaces)
                {
                    services.AddTransient(@interface, type);
                }
            }
        }

        return services;
    }
}
