using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Abstractions.BuildingBlocks.Messaging;
using OrderManagement.Application.Orders.Queries;

namespace OrderManagement.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.Prefix + "/orders")]
public class OrdersController(IDispatcher dispatcher) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] OrderPageFilter filter, CancellationToken ct = default)
    {
        var query = new OrderPageQuery(filter);
        var result = await dispatcher.SendAsync(query, ct);
        return Ok(result);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> Detail([FromRoute] Guid orderId, CancellationToken ct = default)
    {
        var request = new OrderDetailQuery(orderId);
        var result = await dispatcher.SendAsync(request, ct);

        return Ok(result);
    }

    [HttpGet("error")]
    public IActionResult ThrowError()
    {
        throw new InvalidOperationException("This is a test exception for logging");
    }
}