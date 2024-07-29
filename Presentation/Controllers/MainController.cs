using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MainController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("healthy!");
    }
}