using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.CustomDocs
{
    public class ApiVersionOneFixture : ApiVersionFixtureBase
    {
        public ApiVersionOneFixture() : base(typeof(StartupCustomRoutesAndDocs), "swagger/v1.0/swagger.json")
        {
        }
    }
}
