using OrderManagement.Infrastructure.Logging;

namespace OrderManagement.Api.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers[LogProperties.CorrelationIdHeader].FirstOrDefault()
                            ?? Guid.NewGuid().ToString("N");

        context.Items[LogProperties.CorrelationId] = correlationId;
        context.Response.Headers[LogProperties.CorrelationIdHeader] = correlationId;

        using (Serilog.Context.LogContext.PushProperty(LogProperties.CorrelationId, correlationId))
        {
            await next(context);
        }
    }
}