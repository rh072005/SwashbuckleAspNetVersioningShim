///////////////////////////////////////////////////////////////////////////////
// Tools and Addins
///////////////////////////////////////////////////////////////////////////////
#tool "GitVersion.CommandLine"
#addin nuget:?package=Cake.Git
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var buildFolder = MakeAbsolute(Directory(Argument("buildFolder", "./src/SwashbuckleAspNetVersioningShim/bin/Release")));
var artifacts = MakeAbsolute(Directory(Argument("artifactPath", "./artifacts")));

var versionInfo = GitVersion(new GitVersionSettings { UpdateAssemblyInfo = true });
///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});
	
///////////////////////////////////////////////////////////////////////////////
// USER TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
	{
		CleanDirectory(artifacts);
		CleanDirectory(buildFolder);
	});
	
Task("Build")
	.IsDependentOn("Clean")
	.Does(() =>
	{
		Information("Running build...");
		DotNetCoreBuild("./src/SwashbuckleAspNetVersioningShim.sln", new DotNetCoreBuildSettings {
			Configuration = "Release"
		});
});

Task("Test")
	.IsDependentOn("Build")
	.Does(() =>
	{
		Information("Running tests...");
		DotNetCoreTest("./src/SwashbuckleAspNetVersioningShim.Tests/SwashbuckleAspNetVersioningShim.Tests.csproj");
});

Task("Package")
	.IsDependentOn("Test")
	.Does(() =>
	{
		Information("Running packaging...");
		var nuGetPackSettings   = new NuGetPackSettings {
									 Id                      = "SwashbuckleAspNetVersioningShim",
									 Version                 = versionInfo.LegacySemVer,
									 Title                   = "Swashbuckle ASP.NET Versioning Shim",
									 Authors                 = new[] {"Ryan Hird"},
									 Description             = "Library to aid the combined use of Swashbuckle and ASP NET API Versioning",
									 ProjectUrl              = new Uri("https://github.com/rh072005/SwashbuckleAspNetVersioningShim"),
									 Copyright               = $"Ryan Hird {DateTime.Now.Year}",
									 Tags                    = new []{"Swashbuckle", "ASP.NET", "Versioning", "Shim", "WebAPI", "Swagger", "AspNetCore", "MVC"},
									 RequireLicenseAcceptance= false,
									 Symbols                 = false,
									 NoPackageAnalysis       = true,
									 Dependencies            = new [] {
																		new NuSpecDependency {Id="Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer", Version="2.2.0"},
																		new NuSpecDependency {Id="Swashbuckle.AspNetCore", Version="3.0.0"},
																	  },
									 Files					 = new [] {new NuSpecContent {Source = "**/SwashbuckleAspNetVersioningShim.dll", Target = "lib"}},
									 BasePath                = buildFolder,
									 OutputDirectory         = artifacts
								 };

		NuGetPack(nuGetPackSettings);
	});
	
Task("Default")
    .IsDependentOn("Package");
RunTarget(target);
