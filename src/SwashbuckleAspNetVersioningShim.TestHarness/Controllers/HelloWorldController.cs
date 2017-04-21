using Microsoft.AspNetCore.Mvc;

namespace SwashbuckleAspNetVersioningShim.TestHarness.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public string Get() => "Hello world!";
    }    
}
