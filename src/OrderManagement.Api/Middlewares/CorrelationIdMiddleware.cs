using OrderManagement.Api.Logging;

namespace OrderManagement.Api.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeader = "X-Correlation-Id";

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault()
                            ?? Guid.NewGuid().ToString("N");
        
        context.Items[LogProperties.CorrelationId] = correlationId;
        context.Response.Headers[CorrelationIdHeader] = correlationId;

        using (Serilog.Context.LogContext.PushProperty(LogProperties.CorrelationId, correlationId))
        {
            await next(context);
        }
    }
}