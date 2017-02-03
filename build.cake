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
var versionType = Argument("VersionType", "patch");
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
// SYSTEM TASKS
///////////////////////////////////////////////////////////////////////////////
Task("Version")
	.Does(() =>
	{
		var semVersion = "";
		int major = 0;
		int minor = 1;
		int patch = 0;
		GitVersion assertedVersions = GitVersion(new GitVersionSettings
		{
			OutputType = GitVersionOutput.Json,
		});
		major = assertedVersions.Major;
		minor = assertedVersions.Minor;
		patch = assertedVersions.Patch;
		switch (versionType.ToLower())
		{
			case "patch":
				patch += 1; break;
			case "minor":
				minor += 1; patch = 0; break;
			case "major":
				major += 1;	minor = 0; patch = 0; break;			
		};
		semVersion = string.Format("{0}.{1}.{2}", major, minor, patch);
		GitTag(".", semVersion);
		Information("Changing version: {0} to {1}", assertedVersions.LegacySemVerPadded, semVersion);
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
	
Task("Restore")
	.IsDependentOn("Clean").Does(() =>
	{
		DotNetCoreRestore("./src/SwashbuckleAspNetVersioningShim.sln");
	});
	
Task("Build")
	.IsDependentOn("Restore")
	.Does(() =>
	{
		Information("Running Build...");
		DotNetCoreBuild("./src/SwashbuckleAspNetVersioningShim.sln", new DotNetCoreBuildSettings {
			Configuration = "Release"
		});
});

Task("Package")
	.IsDependentOn("Build")
	.Does(() =>
	{
		Information("Running Packaging...");
		var nuGetPackSettings   = new NuGetPackSettings {
									 Id                      = "SwashbuckleAspNetVersioningShim",
									 Version                 = versionInfo.SemVer,
									 Title                   = "Swashbuckle ASP.NET Versioning Shim",
									 Authors                 = new[] {"Ryan Hird"},
									 Description             = "Library to aid the combined use of Swashbuckle and ASP NET API Versioning",
									 ProjectUrl              = new Uri("https://github.com/rh072005/SwashbuckleAspNetVersioningShim"),
									 Copyright               = "Ryan Hird 2017",
									 RequireLicenseAcceptance= false,
									 Symbols                 = false,
									 NoPackageAnalysis       = true,
									 Dependencies            = new [] {
																		new NuSpecDependency {Id="Microsoft.AspNetCore.Mvc.Versioning", Version="1.0.3"},
																		new NuSpecDependency {Id="Swashbuckle.AspNetCore", Version="1.0.0-rc1"},
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
