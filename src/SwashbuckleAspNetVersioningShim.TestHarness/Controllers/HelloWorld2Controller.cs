using Microsoft.AspNetCore.Mvc;

namespace SwashbuckleAspNetVersioningShim.TestHarness.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{api-version:apiVersion}/helloworld")]
    public class HelloWorld2Controller : Controller
    {
        [HttpGet, MapToApiVersion("2.0")]
        public string Get() => "Hello world v2!";

        [HttpGet, MapToApiVersion("3.0")]
        public string GetV3() => "Hello world v3!";
    }
}
