using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures
{
    public class ApiVersionTwoFixture : ApiVersionFixtureBase
    {
        public ApiVersionTwoFixture() : base(typeof(Startup), "swagger/2.0/swagger.json")
        {
        }
    }
}
