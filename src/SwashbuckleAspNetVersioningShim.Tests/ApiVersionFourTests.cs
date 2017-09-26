using Xunit;
using FluentAssertions;
using SwashbuckleAspNetVersioningShim.Tests.Fixtures;

namespace SwashbuckleAspNetVersioningShim.Tests
{
    public class ApiVersionFourTests : IClassFixture<ApiVersionFourFixture>
    {
        private ApiVersionFourFixture _fixture;

        public ApiVersionFourTests(ApiVersionFourFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void SwaggerDocumentInfoVersionShouldBeFourPointZero()
        {
            string infoVersion = _fixture.SwaggerDocument?.info?.version;
            infoVersion.Should().Be("4.0");
        }

        [Fact]
        public void SwaggerDocumentShouldContainValuesPath()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
        }

        [Fact]
        public void ValuesPathShouldHaveGetMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.get).Should().NotBeNull("this should support get");
        }

        [Fact]
        public void ValuesPathShouldNotHavePostMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.post).Should().BeNull("this method only supports get");
        }

        [Fact]
        public void ValuesPathShouldNotHaveDeleteMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.delete).Should().BeNull("this method only supports get");
        }

        [Fact]
        public void ValuesPathShouldNotHavePatchMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.patch).Should().BeNull("this method only supports get");
        }

        [Fact]
        public void ValuesPathShouldNotHavePutMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.put).Should().BeNull("this method only supports get");
        }

        [Fact]
        public void ValuesGetMethodsOperationIdShouldBeApiValuesGet()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.get).Should().NotBeNull("this should support get");
            ((object)valuesPathKey?.get?.operationid).ToString().Should().Be("apivaluesget");
        }

        [Fact]
        public void ValuesPathShouldNotExistWithAVersion()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/v4.0/values"];
            ((object)valuesPathKey).Should().BeNull("there should not be a version in the values path");
        }

        [Fact]
        public void ValuesPathGetShouldHaveTwoParameters()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.get).Should().NotBeNull("this should support get");
            ((object)valuesPathKey?.get?.operationid).ToString().Should().Be("apivaluesget");
            ((object)valuesPathKey?.get?.parameters?.Count).Should().Be(2, "there should be 2 parameters, one for the api-version and the randomId to represent another parameter");
        }
    }
}
