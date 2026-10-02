using Serilog;

namespace OrderManagement.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void UseApiPipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.MapControllers();
        app.MapHealthChecks("/health");
    }
}