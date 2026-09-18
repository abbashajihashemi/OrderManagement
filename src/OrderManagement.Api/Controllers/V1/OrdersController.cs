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
}
