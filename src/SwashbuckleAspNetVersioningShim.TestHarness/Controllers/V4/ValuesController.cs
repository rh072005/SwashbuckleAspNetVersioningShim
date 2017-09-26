using Microsoft.AspNetCore.Mvc;

namespace SwashbuckleAspNetVersioningShim.TestHarness.Controllers.V4
{
    [ApiVersion("4.0")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public string Get([FromQuery] int randomId) => "version 4.0";
    }
}
