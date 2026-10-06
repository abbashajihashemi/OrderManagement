namespace OrderManagement.Api.Extensions;
using Serilog;

public static class LoggingExtensions
{
    public static void UseSerilogLogging(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithProperty("Application", "OrderManagement.Api")
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        );
    }
}