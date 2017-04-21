using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures
{
    public class ApiVersionThreeFixture : ApiVersionFixtureBase
    {
        public ApiVersionThreeFixture() : base(typeof(Startup), "swagger/v3/swagger.json")
        {
        }
    }
}
