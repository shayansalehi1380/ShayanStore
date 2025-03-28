using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.UI.Controllers.Api;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseApiController : ControllerBase
{
}