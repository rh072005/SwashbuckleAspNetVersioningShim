using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SwashbuckleAspNetVersioningShim
{
    public class SwaggerVersioner
    {
        public bool SetDocInclusions(string version, ApiDescription apiDescription)
        {
            var versions = apiDescription.GroupName?.Split('#');
            if (versions == null)
            {
                return false;
            }
            if (!versions.Contains(version.Replace("v", "")))
            {
                return false;
            }

            var versionRegex = new Regex("^(v\\d|v{version})$");
            var values = apiDescription.RelativePath.Split('/').Select(v => versionRegex.Replace(v, version));
            apiDescription.RelativePath = string.Join("/", values);
            var versionParameter = apiDescription.ParameterDescriptions.SingleOrDefault(p => p.Name == "version");

            if (versionParameter != null)
            {
                apiDescription.ParameterDescriptions.Remove(versionParameter);
            }

            foreach (var parameter in apiDescription.ParameterDescriptions)
            {
                parameter.Name = char.ToLowerInvariant(parameter.Name[0]) + parameter.Name.Substring(1);
            }

            return true;
        }

        public List<string> GetAllApiVersions(ApplicationPartManager partManager)
        {
            var controllerFeature = new ControllerFeature();
            partManager.PopulateFeature(controllerFeature);
            var versionList = new List<string>();
            var versionAttributes = controllerFeature.Controllers.Select(x => IntrospectionExtensions.GetTypeInfo(x.AsType()).GetCustomAttributes<ApiVersionAttribute>());
            versionList = versionAttributes.SelectMany(x => x.Select(y => y.Versions.FirstOrDefault().ToString())).ToList();
            versionList = versionList.Distinct().OrderByDescending(x => x).ToList();
            return versionList;
        }

        public List<string> GetApiVersionsForController(TypeInfo controllerTypeInfo)
        {
            var versionList = new List<string>();
            var versionAttributes = controllerTypeInfo.GetCustomAttributes<ApiVersionAttribute>();
            versionList = versionAttributes.Select(x => x.Versions.FirstOrDefault().ToString()).ToList();
            return versionList;
        }

        public void ConfigureSwaggerGen(SwaggerGenOptions options, ApplicationPartManager partManager)
        {
            var versions = GetAllApiVersions(partManager);
            options.DocInclusionPredicate((version, apiDescription) =>
            {
                return SetDocInclusions(version, apiDescription);
            });

            foreach (var version in versions)
            {
                options.SwaggerDoc(string.Format($"v{version}"), new Info { Version = version, Title = string.Format($"API V{version}") });
            }
        }

        public void ConfigureSwaggerUi(Swashbuckle.AspNetCore.SwaggerUi.SwaggerUiOptions c, ApplicationPartManager partManager)
        {
            var versions = GetAllApiVersions(partManager);
            foreach (var version in versions)
            {
                c.SwaggerEndpoint(string.Format($"/swagger/v{version}/swagger.json"), string.Format($"V{version} Docs"));
            }
        }
    }
}
