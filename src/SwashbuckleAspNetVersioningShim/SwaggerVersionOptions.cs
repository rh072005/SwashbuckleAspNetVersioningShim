
namespace SwashbuckleAspNetVersioningShim
{
    public class SwaggerVersionOptions
    {
        /// <summary>
        /// Template for locating swagger documents inside Swagger Ui.
        /// Document version number (without "v") will be substituted for "{0}".
        /// </summary>
        public string RouteTemplate { get; set; } = "/swagger/v{0}/swagger.json";

        /// <summary>
        /// Template for description of each swagger doc in the ui.
        /// Document version number (without "v") will be substituted for "{0}".
        /// </summary>
        public string DescriptionTemplate { get; set; } = "v{0} Docs";
    }
}
