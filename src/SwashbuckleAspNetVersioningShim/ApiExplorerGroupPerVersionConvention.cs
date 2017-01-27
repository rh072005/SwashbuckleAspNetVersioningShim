using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace SwashbuckleAspNetVersioningShim
{
    public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
    {
        SwaggerVersioner _swaggerVersioner;
        public ApiExplorerGroupPerVersionConvention(SwaggerVersioner swaggerVersioning)
        {
            _swaggerVersioner = swaggerVersioning;
        }

        public void Apply(ControllerModel controller)
        {
            var versions = _swaggerVersioner.GetApiVersionsForController(controller.ControllerType);
            if (versions.Any())
            {
                controller.ApiExplorer.GroupName = string.Join("#", versions);
            }
        }
    }
}
