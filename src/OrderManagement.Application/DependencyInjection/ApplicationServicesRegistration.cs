using Microsoft.Extensions.DependencyInjection;

namespace OrderManagement.Application.DependencyInjection;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        var handlerType = typeof(IHandler<,>);
        var types = typeof(ApplicationServicesRegistration).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false });

        foreach (var type in types)
        foreach (var i in type.GetInterfaces()
                     .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType))
            services.AddScoped(i, type);

        return services;
    }
}