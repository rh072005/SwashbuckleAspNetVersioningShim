using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleAspNetVersioningShim
{
    public class SetVersionInPathsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(
                path => path.Key.Replace("v{version}", $"v{swaggerDoc.Info.Version}"),
                path => path.Value
                );
        }
    }
}
