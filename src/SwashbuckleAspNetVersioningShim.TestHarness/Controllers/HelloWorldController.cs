using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SwashbuckleAspNetVersioningShim.TestHarness.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public string Get() => "Hello world!";

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "HelloWorldCustomOperationName")]
        public string GetCustom(int id) => "Hello custom world!";
    }    
}
