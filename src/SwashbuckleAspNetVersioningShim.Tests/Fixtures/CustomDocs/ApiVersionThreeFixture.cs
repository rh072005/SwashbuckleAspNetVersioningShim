using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.CustomDocs
{
    public class ApiVersionThreeFixture : ApiVersionFixtureBase
    {
        public ApiVersionThreeFixture() : base(typeof(StartupCustomRoutesAndDocs), "swagger/3.0/swagger.json")
        {
        }
    }
}
