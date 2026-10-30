using Microsoft.AspNetCore.Http;
using OrderManagement.Infrastructure.Logging;

namespace OrderManagement.Infrastructure.Http;

public class CorrelationIdHandler(
    IHttpContextAccessor httpContextAccessor
) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var correlationId = httpContextAccessor.HttpContext?
            .Items[LogProperties.CorrelationId]?
            .ToString();

        if (!string.IsNullOrEmpty(correlationId) &&
            !request.Headers.Contains(LogProperties.CorrelationIdHeader))
        {
            request.Headers.Add(LogProperties.CorrelationIdHeader, correlationId);
        }

        return base.SendAsync(request, ct);
    }
}