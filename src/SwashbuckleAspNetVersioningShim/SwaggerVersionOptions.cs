
namespace SwashbuckleAspNetVersioningShim
{
    public class SwaggerVersionOptions
    {
        /// <summary>
        /// Template for locating swagger documents inside Swagger Ui.
        /// Document version number (with "v") will be substituted for "{0}".
        /// </summary>
        public string RouteTemplate { get; set; } = "/swagger/{0}/swagger.json";

        /// <summary>
        /// Template for description of each swagger doc in the ui.
        /// Document version number (with "v") will be substituted for "{0}".
        /// </summary>
        public string DescriptionTemplate { get; set; } = "{0} Docs";
    }
}
