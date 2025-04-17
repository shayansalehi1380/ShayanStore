using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers.Api;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController : ControllerBase
{
}