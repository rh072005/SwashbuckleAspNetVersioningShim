using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SwashbuckleAspNetVersioningShim
{
    public static class SwaggerVersioner
    {
        public static void ConfigureSwaggerVersions(this SwaggerGenOptions swaggerOptions, IApiVersionDescriptionProvider provider)
        {
            swaggerOptions.ConfigureSwaggerVersions(provider, "API Version {0}");
        }

        public static void ConfigureSwaggerVersions(this SwaggerGenOptions swaggerOptions, IApiVersionDescriptionProvider provider, string titleTemplate)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                var title = string.Format(titleTemplate, description.ApiVersion);
                swaggerOptions.SwaggerDoc(description.GroupName, new Info { Version = description.ApiVersion.ToString(), Title = title });                
            }

            swaggerOptions.OperationFilter<CorrectOperationIdsOperationFilter>();
            swaggerOptions.OperationFilter<RemoveVersionParametersOperationFilter>();
            swaggerOptions.DocumentFilter<SetVersionInPathsDocumentFilter>();
        }

        public static void ConfigureSwaggerVersions(this SwaggerUIOptions swaggerUIOptions, IApiVersionDescriptionProvider provider)
        {
            swaggerUIOptions.ConfigureSwaggerVersions(provider, new SwaggerVersionOptions());
        }

        public static void ConfigureSwaggerVersions(this SwaggerUIOptions swaggerUIOptions, IApiVersionDescriptionProvider provider, SwaggerVersionOptions versionOptions)
        {
            foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
            {
                var url = string.Format(versionOptions.RouteTemplate, apiVersionDescription.GroupName);
                var description = string.Format(versionOptions.DescriptionTemplate, apiVersionDescription.GroupName);
                swaggerUIOptions.SwaggerEndpoint(url, description);
            }
        }
    }
}
