using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderManagement.Application.Inventory;
using OrderManagement.Infrastructure.Clients.Inventory;
using OrderManagement.Infrastructure.Http;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<CorrelationIdHandler>();

        services.AddOptions<InventoryClientOptions>()
            .BindConfiguration(InventoryClientOptions.Section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        if (configuration.GetValue<bool>($"{InventoryClientOptions.Section}:UseInMemory"))
        {
            services.AddSingleton<IInventoryClient, InMemoryInventoryClient>();
        }
        else
        {
            services.AddHttpClient<IInventoryClient, InventoryClient>((sp, client) =>
                {
                    var options = sp.GetRequiredService<IOptions<InventoryClientOptions>>();
                    client.BaseAddress = new Uri(options.Value.BaseUrl);
                })
                .AddHttpMessageHandler<CorrelationIdHandler>()
                .AddStandardResilienceHandler();
        }

        services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
    }
}