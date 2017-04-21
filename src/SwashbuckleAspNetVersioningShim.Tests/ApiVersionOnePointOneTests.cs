using FluentAssertions;
using SwashbuckleAspNetVersioningShim.Tests.Fixtures;
using Xunit;

namespace SwashbuckleAspNetVersioningShim.Tests
{
    public class ApiVersionOnePointOneTests : IClassFixture<ApiVersionOnePointOneFixture>
    {
        private ApiVersionOnePointOneFixture _fixture;

        public ApiVersionOnePointOneTests(ApiVersionOnePointOneFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void SwaggerDocumentInfoVersionShouldBeOnePointOne()
        {
            string infoVersion = _fixture.SwaggerDocument?.info?.version;
            infoVersion.Should().Be("1.1");
        }

        [Fact]
        public void SwaggerDocumentShouldNotContainHelloWorldPath()
        {
            var helloWorldPathKey = _fixture?.SwaggerDocument?.paths?["/api/v1.1/helloworld"];
            ((object)helloWorldPathKey).Should().BeNull("there should be hello world path");
        }
               
        [Fact]
        public void SwaggerDocumentShouldContainValuesPath()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/v1.1/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
        }

        [Fact]
        public void ValuesPathShouldHaveGetMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/v1.1/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.get).Should().NotBeNull("this should support get");
        }

        [Fact]
        public void ValuesPathShouldNotHavePostMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/v1.1/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.post).Should().BeNull("this method only supports get");
        }

        [Fact]
        public void ValuesPathShouldNotHaveDeleteMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/v1.1/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.delete).Should().BeNull("this method only supports get");
        }

        [Fact]
        public void ValuesPathShouldNotHavePatchMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/v1.1/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.patch).Should().BeNull("this method only supports get");
        }

        [Fact]
        public void ValuesPathShouldNotHavePutMethod()
        {
            var valuesPathKey = _fixture?.SwaggerDocument?.paths?["/api/v1.1/values"];
            ((object)valuesPathKey).Should().NotBeNull("there should be values path");
            ((object)valuesPathKey?.put).Should().BeNull("this method only supports get");
        }
    }
}
