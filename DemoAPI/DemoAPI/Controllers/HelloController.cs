using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Hello World!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name)
        {
            return Ok("Hello " + name + " ! Welcome to System");
        }
    }
}
