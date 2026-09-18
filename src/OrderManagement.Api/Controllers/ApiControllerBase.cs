using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class ApiControllerBase : ControllerBase;