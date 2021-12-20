using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace DCB.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class ScanningTests
{

    [TestMethod]
    public void ScanDotnetSampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\dotnet";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        string[] assignees = new string[] { "samsmithnz" };
        int openPRLimit = 10;
        string interval = "daily";
        string time = "06:00";
        string timezone = "America/New_York";

        //act
        List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
        string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files, interval, time, timezone, assignees, openPRLimit);

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: nuget
  directory: /
  schedule:
    interval: daily
    time: 06:00
    timezone: America/New_York
  open-pull-requests-limit: 10
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
    time: 06:00
    timezone: America/New_York
  open-pull-requests-limit: 10
";

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
        }
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void ScanThisProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory?.Replace("\\", "/");
        }
        string[] assignees = new string[] { "samsmithnz" };
        int openPRLimit = 10;

        //act
        List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
        string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files, "daily", null, null, assignees, openPRLimit);

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: nuget
  directory: /samples/dotnet/
  schedule:
    interval: daily
  open-pull-requests-limit: 10
- package-ecosystem: maven
  directory: /samples/java/
  schedule:
    interval: daily
  open-pull-requests-limit: 10
- package-ecosystem: npm
  directory: /samples/javascript/
  schedule:
    interval: daily
  open-pull-requests-limit: 10
- package-ecosystem: pip
  directory: /samples/python/
  schedule:
    interval: daily
  open-pull-requests-limit: 10
- package-ecosystem: bundler
  directory: /samples/ruby/
  schedule:
    interval: daily
  open-pull-requests-limit: 10
- package-ecosystem: nuget
  directory: /src/DCB.Tests/
  schedule:
    interval: daily
  open-pull-requests-limit: 10
- package-ecosystem: nuget
  directory: /src/DCB/
  schedule:
    interval: daily
  open-pull-requests-limit: 10
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
  open-pull-requests-limit: 10
";

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
        }
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void ScanPomSampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\java";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        string interval = "daily";

        //act
        List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
        string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files, interval);

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: maven
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
";

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
        }
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void ScanNPMSampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\javascript";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        string interval = "daily";

        //act
        List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
        string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files, interval);

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: npm
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
";

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
        }
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void ScanRubySampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\ruby";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        string interval = "daily";

        //act
        List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
        string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files, interval);

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: bundler
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
";

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
        }
        Assert.AreEqual(expected, yaml);
    }

    [TestMethod]
    public void ScanPythonSampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\python";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        string interval = "daily";

        //act
        List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
        string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files, interval);

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: pip
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
";

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
        }
        Assert.AreEqual(expected, yaml);
    }

}
