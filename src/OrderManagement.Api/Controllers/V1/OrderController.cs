using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Api.Controllers.V1;

[Route(ApiRoutes.Prefix + "/orders")]
public class OrderController : ApiControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "Order Management API is alive" });
    }
}
