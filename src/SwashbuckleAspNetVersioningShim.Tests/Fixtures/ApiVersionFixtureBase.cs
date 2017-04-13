using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures
{
    public class ApiVersionFixtureBase : IDisposable
    {
        public dynamic SwaggerDocument { get; private set; }

        public ApiVersionFixtureBase(Type startupClassType, string swaggerUrl)
        {
            var testServer = new TestServer(new WebHostBuilder().UseStartup(startupClassType));
            var httpClient = testServer.CreateClient();
            var response = httpClient.GetAsync(swaggerUrl).Result;
            response.EnsureSuccessStatusCode();
            var swaggerJson = response.Content.ReadAsStringAsync().Result;
            SwaggerDocument = JsonConvert.DeserializeObject(swaggerJson.ToLower());
        }

        public void Dispose()
        {

        }
    }
}
