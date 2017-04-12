using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SwashbuckleAspNetVersioningShim
{
    public static class SwaggerVersioner
    {
        [Obsolete("Use ConfigureSwaggerVersions instead.")]
        public static void ConfigureSwaggerGen(SwaggerGenOptions swaggerOptions, ApplicationPartManager partManager)
        {
            swaggerOptions.ConfigureSwaggerVersions(partManager);
        }

        public static void ConfigureSwaggerVersions(this SwaggerGenOptions swaggerOptions, ApplicationPartManager partManager)
        {
            var allVersions = GetAllApiVersions(partManager);
            foreach (var version in allVersions)
            {
                swaggerOptions.SwaggerDoc($"v{version}", new Info { Version = version, Title = $"API Version {version}" });
            }

            swaggerOptions.DocInclusionPredicate((version, apiDescription) =>
            {
                var actionVersions = apiDescription.ActionAttributes().OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions);
                var controllerVersions = apiDescription.ControllerAttributes().OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
                var controllerAndActionVersionsOverlap = controllerVersions.Intersect(actionVersions).Any();
                if (controllerAndActionVersionsOverlap)
				{
					return actionVersions.Any(v => $"v{v.ToString()}" == version);
				}
				return controllerVersions.Any(v => $"v{v.ToString()}" == version);
            });

            swaggerOptions.OperationFilter<RemoveVersionParametersOperationFilter>();
            swaggerOptions.DocumentFilter<SetVersionInPathsDocumentFilter>();
        }

        public static List<string> GetAllApiVersions(this ApplicationPartManager partManager)
        {
            var controllerFeature = new ControllerFeature();
            partManager.PopulateFeature(controllerFeature);
            var versionList = new List<string>();
            var versionAttributes = controllerFeature.Controllers.Select(x => IntrospectionExtensions.GetTypeInfo(x.AsType()).GetCustomAttributes<ApiVersionAttribute>());
            versionList = versionAttributes.SelectMany(x => x.Select(y => y.Versions.FirstOrDefault().ToString())).ToList();
            versionList = versionList.Distinct().OrderByDescending(x => x).ToList();
            return versionList;
        }

        [Obsolete("Use ConfigureSwaggerVersions instead.")]
        public static void ConfigureSwaggerUI(SwaggerUIOptions swaggerUIOptions, ApplicationPartManager partManager)
        {
            swaggerUIOptions.ConfigureSwaggerVersions(partManager);
        }

        public static void ConfigureSwaggerVersions(this SwaggerUIOptions swaggerUIOptions, ApplicationPartManager partManager)
        {
            var versions = GetAllApiVersions(partManager);
            foreach (var version in versions)
            {
                swaggerUIOptions.SwaggerEndpoint($"/swagger/v{version}/swagger.json", $"v{version} Docs");
            }
        }
    }
}
