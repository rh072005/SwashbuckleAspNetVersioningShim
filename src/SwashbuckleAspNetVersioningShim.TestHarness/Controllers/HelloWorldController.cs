using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleAspNetVersioningShim.TestHarness.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public string Get() => "Hello world!";

        [HttpGet("{id}")]
        [SwaggerOperation("HelloWorldCustomOperationName")]
        public string GetCustom(int id) => "Hello custom world!";
    }    
}
