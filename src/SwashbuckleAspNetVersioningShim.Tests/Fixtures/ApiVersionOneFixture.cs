using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures
{
    public class ApiVersionOneFixture : ApiVersionFixtureBase
    {
        public ApiVersionOneFixture() : base(typeof(Startup), "swagger/1.0/swagger.json")
        {
        }
    }
}
