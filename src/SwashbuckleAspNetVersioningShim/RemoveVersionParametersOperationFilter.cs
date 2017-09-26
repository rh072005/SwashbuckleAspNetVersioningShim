using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleAspNetVersioningShim
{
    public class RemoveVersionParametersOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {            
            var apiVersion = context?.ApiDescription?.GetApiVersion();
            if (apiVersion != null)
            {
                var versionParameter = operation?.Parameters?.SingleOrDefault(p => p.Name == "api-version" && p.In == "path");
                if (versionParameter != null)
                {
                    operation.Parameters.Remove(versionParameter);
                }
                else
                {
                    var versionParameterInQuery = operation?.Parameters?.SingleOrDefault(p => p.Name == "api-version" && p.In == "query");
                    if (versionParameterInQuery != null)
                    {
                        operation.Parameters.Remove(versionParameterInQuery);
                    }
                    operation?.Parameters?.Add(new NonBodyParameter()
                    {
                        Name = "api-version",
                        Required = true,
                        Default = apiVersion.ToString(),
                        In = "query",
                        Type = "string"
                    });
                }
            }
        }
    }
}
