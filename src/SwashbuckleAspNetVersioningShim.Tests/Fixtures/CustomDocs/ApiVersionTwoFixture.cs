using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.CustomDocs
{
    public class ApiVersionTwoFixture : ApiVersionFixtureBase
    {
        public ApiVersionTwoFixture() : base(typeof(StartupCustomRoutesAndDocs), "swagger/2.0/swagger.json")
        {
        }
    }
}
