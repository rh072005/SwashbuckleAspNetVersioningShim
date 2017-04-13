using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.Static
{
    public class ApiVersionOneFixture : ApiVersionFixtureBase
    {
        public ApiVersionOneFixture() : base(typeof(StartupStatic), "swagger/v1.0/swagger.json")
        {
        }
    }
}
