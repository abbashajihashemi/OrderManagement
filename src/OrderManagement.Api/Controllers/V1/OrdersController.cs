using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.Prefix + "/orders")]
public class OrdersController : ApiControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "Order Management API is alive" });
    }

    [HttpGet("error")]
    public IActionResult ThrowError()
    {
        throw new InvalidOperationException("This is a test exception for logging");
    }
}
