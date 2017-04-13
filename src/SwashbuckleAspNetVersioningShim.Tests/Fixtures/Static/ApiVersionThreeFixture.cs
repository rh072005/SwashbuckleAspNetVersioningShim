using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.Static
{
    public class ApiVersionThreeFixture : ApiVersionFixtureBase
    {
        public ApiVersionThreeFixture() : base(typeof(StartupStatic), "swagger/v3.0/swagger.json")
        {
        }
    }
}
