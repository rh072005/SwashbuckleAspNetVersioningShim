using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.CustomDocs
{
    public class ApiVersionOnePointOneFixture : ApiVersionFixtureBase
    {
        public ApiVersionOnePointOneFixture() : base(typeof(StartupCustomRoutesAndDocs), "swagger/1.1/swagger.json")
        {
        }
    }
}
