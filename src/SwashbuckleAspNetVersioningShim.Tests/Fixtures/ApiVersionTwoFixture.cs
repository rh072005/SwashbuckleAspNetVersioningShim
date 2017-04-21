using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures
{
    public class ApiVersionTwoFixture : ApiVersionFixtureBase
    {
        public ApiVersionTwoFixture() : base(typeof(Startup), "swagger/v2/swagger.json")
        {
        }
    }
}
