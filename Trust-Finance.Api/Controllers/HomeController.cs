namespace TF.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TF.Models;

    [ApiController]
    [Route("")]

    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(new { message = "Welcome to the TF API" });
        }
    }
}