using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.CustomDocs
{
    public class ApiVersionOnePointOneFixture : ApiVersionFixtureBase
    {
        public ApiVersionOnePointOneFixture() : base(typeof(StartupCustomRoutesAndDocs), "swagger/v1.1/swagger.json")
        {
        }
    }
}
