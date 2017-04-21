using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures
{
    public class ApiVersionOnePointOneFixture : ApiVersionFixtureBase
    {
        public ApiVersionOnePointOneFixture() : base(typeof(Startup), "swagger/v1.1/swagger.json")
        {
        }
    }
}
