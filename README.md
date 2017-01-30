# Swashbuckle ASP.NET Versioning Shim
[![Build status](https://ci.appveyor.com/api/projects/status/wjwi5jpn7oov6i96?svg=true)](https://ci.appveyor.com/project/rh072005/swashbuckleaspnetversioningshim)

This library aids the use of [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle) and [ASP NET Web API Versioning](https://github.com/Microsoft/aspnet-api-versioning) together and started from my attempt at resolving [Swashbuckle.AspNetCore issue 244](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/244)

Note: Development has been carried out with [URL path versioning](https://github.com/Microsoft/aspnet-api-versioning/wiki/Versioning-via-the-URL-Path) in mind. I've not tested it with the other versioning conventions that API versioning provide.

## Getting started

- Start by creating a new ASP.NET Core Web Application
- Install the SwashbuckleAspNetVersioningShim NuGet package
  - Short term this will be a case of building from source 
  - Mid term it will be hosted on MyGet
  - Long term it will be hosted on NuGet
- Add the following code blocks to Startup.cs

```csharp
using SwashbuckleAspNetVersioningShim;
```

```csharp
public class Startup
{
    private IMvcBuilder _mvcBuilder;
    private SwaggerVersioner _swaggerVersioning = new SwaggerVersioner();
    ...
```

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // This replaces services.AddMvc(); because we need access to IMvcBuilder's ApplicationPartManager later on
    _mvcBuilder = services.AddMvc(c =>
        c.Conventions.Add(new ApiExplorerGroupPerVersionConvention(_swaggerVersioning))
    );

    services.AddApiVersioning();
    services.AddSwaggerGen(c =>
    {
        _swaggerVersioning.ConfigureSwaggerGen(c, _mvcBuilder.PartManager);
    });
    ...
```

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    ...
    app.UseSwagger();
    app.UseSwaggerUi(c =>
    {
        _swaggerVersioning.ConfigureSwaggerUi(c, _mvcBuilder.PartManager);
    });
    ...
}
```

All being well you can now continue to use ASP NET Web API Versioning as per it's documentation.
As a minimum your web API controller will want an ApiVersionAttribute and a RouteAttribute.
Using the default ValueController that's created with a new Web API project it would look like this

```csharp
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ValuesController : Controller
{
    // GET api/values
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }
    ...
```

## License
See the [LICENSE](LICENSE) file for license rights and limitations (MIT).