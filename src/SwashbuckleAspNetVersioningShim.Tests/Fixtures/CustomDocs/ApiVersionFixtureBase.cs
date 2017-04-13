using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using SwashbuckleAspNetVersioningShim.TestHarness;

namespace SwashbuckleAspNetVersioningShim.Tests.Fixtures.CustomDocs
{
    public class ApiVersionFixtureBase : IDisposable
    {
        public dynamic SwaggerDocument { get; private set; }

        public ApiVersionFixtureBase(string swaggerUrl)
        {
            var testServer = new TestServer(new WebHostBuilder().UseStartup<StartupCustomRoutesAndDocs>());
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
