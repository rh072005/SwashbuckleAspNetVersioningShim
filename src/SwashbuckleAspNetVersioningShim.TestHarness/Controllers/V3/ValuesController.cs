using Microsoft.AspNetCore.Mvc;

namespace SwashbuckleAspNetVersioningShim.TestHarness.Controllers.V3
{
    [ApiVersion("3.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public string Get() => "version 3.0";

        [HttpPost]
        public IActionResult Post([FromBody] string request)
        {
            return Accepted();
        }
    }
}
