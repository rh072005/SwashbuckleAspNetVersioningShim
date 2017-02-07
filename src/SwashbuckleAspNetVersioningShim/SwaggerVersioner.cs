using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUi;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SwashbuckleAspNetVersioningShim
{
    public static class SwaggerVersioner
    {
        public static void ConfigureSwaggerGen(SwaggerGenOptions swaggerOptions, ApplicationPartManager partManager)
        {
            var allVersions = GetAllApiVersions(partManager);
            foreach (var version in allVersions)
            {
                swaggerOptions.SwaggerDoc(string.Format($"v{version}"), new Info { Version = version, Title = string.Format($"API Version {version}") });
            }

            swaggerOptions.DocInclusionPredicate((version, apiDescription) =>
            {
                var actionVersions = apiDescription.ActionAttributes().OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToList();
                var controllerVersions = apiDescription.ControllerAttributes().OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions).ToList();
                var controllerAndActionVersionsOverlap = controllerVersions.Intersect(actionVersions).Any();
                if(controllerAndActionVersionsOverlap)
                {
                    return false;
                }
                return controllerVersions.Any(v => $"v{v.ToString()}" == version);
            });

            swaggerOptions.OperationFilter<RemoveVersionParametersOperationFilter>();
            swaggerOptions.DocumentFilter<SetVersionInPathsDocumentFilter>();
        }

        public static List<string> GetAllApiVersions(ApplicationPartManager partManager)
        {
            var controllerFeature = new ControllerFeature();
            partManager.PopulateFeature(controllerFeature);
            var versionList = new List<string>();
            var versionAttributes = controllerFeature.Controllers.Select(x => IntrospectionExtensions.GetTypeInfo(x.AsType()).GetCustomAttributes<ApiVersionAttribute>());
            versionList = versionAttributes.SelectMany(x => x.Select(y => y.Versions.FirstOrDefault().ToString())).ToList();
            versionList = versionList.Distinct().OrderByDescending(x => x).ToList();
            return versionList;
        }

        public static void ConfigureSwaggerUi(SwaggerUiOptions swaggerUiOptions, ApplicationPartManager partManager)
        {
            var versions = GetAllApiVersions(partManager);
            foreach (var version in versions)
            {
                swaggerUiOptions.SwaggerEndpoint(string.Format($"/swagger/v{version}/swagger.json"), string.Format($"v{version} Docs"));
            }
        }
    }
}
