using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace demo.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{


    public HelloController() { }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok(new { message = "Hello World!" });
    }
}
