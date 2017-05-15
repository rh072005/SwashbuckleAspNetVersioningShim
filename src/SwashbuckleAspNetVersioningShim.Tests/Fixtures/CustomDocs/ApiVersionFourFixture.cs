using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.CustomDocs
{
    public class ApiVersionFourFixture : ApiVersionFixtureBase
    {
        public ApiVersionFourFixture() : base(typeof(StartupCustomRoutesAndDocs), "swagger/4.0/swagger.json")
        {
        }
    }
}
