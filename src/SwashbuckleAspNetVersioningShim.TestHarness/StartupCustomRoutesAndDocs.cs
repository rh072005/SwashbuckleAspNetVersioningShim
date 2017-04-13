using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace SwashbuckleAspNetVersioningShim.TestHarness
{
    public class StartupCustomRoutesAndDocs
    {
        public StartupCustomRoutesAndDocs(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            var mvcBuilder = services.AddMvc();

            services.AddApiVersioning();
            services.AddSwaggerGen(c =>
            {
                c.ConfigureSwaggerVersions(mvcBuilder.PartManager, "Welcome to the documentation for version {0} of my API");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationPartManager partManager)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var versionOptions = new SwaggerVersionOptions { DescriptionTemplate = "Version {0} docs", RouteTemplate = "/swagger/v{0}/swagger.json" };
                c.ConfigureSwaggerVersions(partManager, versionOptions);
            });
        }
    }
}
