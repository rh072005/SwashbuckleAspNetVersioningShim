using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures
{
    public class ApiVersionFourFixture : ApiVersionFixtureBase
    {
        public ApiVersionFourFixture() : base(typeof(Startup), "swagger/4.0/swagger.json")
        {
        }
    }
}
