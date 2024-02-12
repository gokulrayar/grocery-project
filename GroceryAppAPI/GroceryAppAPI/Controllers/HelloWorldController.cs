using GroceryAppAPI.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAppAPI.Controllers
{
    // Controller for a simple "Hello World" message
    [Route("[controller]")]
    [ApiController]
    [CommonExceptionFilter] // Apply the CommonExceptionFilterAttribute to handle common exceptions
    public class HelloWorldController : ControllerBase
    {
        // Endpoint to handle HTTP GET requests
        [HttpGet]
        public IActionResult HelloWorld()
        {
            // Return an Ok result with a simple "Hello World" message
            return Ok(new { Message = "Hello World! This is GroceryAppAPI!" });
        }
    }
}
