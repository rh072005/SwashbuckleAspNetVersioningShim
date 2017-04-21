using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures
{
    public class ApiVersionOneFixture : ApiVersionFixtureBase
    {
        public ApiVersionOneFixture() : base(typeof(Startup), "swagger/v1/swagger.json")
        {
        }
    }
}
