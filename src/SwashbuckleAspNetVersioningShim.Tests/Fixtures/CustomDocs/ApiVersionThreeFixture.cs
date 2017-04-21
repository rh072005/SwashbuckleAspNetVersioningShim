using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.CustomDocs
{
    public class ApiVersionThreeFixture : ApiVersionFixtureBase
    {
        public ApiVersionThreeFixture() : base(typeof(StartupCustomRoutesAndDocs), "swagger/v3/swagger.json")
        {
        }
    }
}
