using OrderManagement.Api.Middlewares;
using Serilog;

namespace OrderManagement.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void UseApiPipeline(this WebApplication app)
    {
        app.UseRequestLogging();

        app.MapControllers();
        app.MapHealthChecks("/health");
    }

    public static void UseRequestLogging(this WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();

        app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = (httpContext, _, ex) =>
            {
                if (httpContext.Request.Path.StartsWithSegments("/health"))
                    return Serilog.Events.LogEventLevel.Verbose;

                return ex != null || httpContext.Response.StatusCode >= 500
                    ? Serilog.Events.LogEventLevel.Error
                    : Serilog.Events.LogEventLevel.Information;
            };
        });
    }
}