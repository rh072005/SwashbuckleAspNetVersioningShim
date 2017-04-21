using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.CustomDocs
{
    public class ApiVersionFourFixture : ApiVersionFixtureBase
    {
        public ApiVersionFourFixture() : base(typeof(StartupCustomRoutesAndDocs), "swagger/v4/swagger.json")
        {
        }
    }
}
