using Microsoft.AspNetCore.Mvc;

namespace SwashbuckleAspNetVersioningShim.TestHarness.Controllers.V1Point1
{
    [ApiVersion("1.1")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public string Get() => "version 1.1";
    }
}
