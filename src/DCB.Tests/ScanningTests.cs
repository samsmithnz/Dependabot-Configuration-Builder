using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace DCB.Tests
{
    [TestClass]
    public class ScanningTests
    {
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

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);

            string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files);

            //assert
            string expected = @"version: 2
updates:
- package-ecosystem: nuget
  directory: /samples/dotnet/
  schedule:
    interval: daily
- package-ecosystem: maven
  directory: /samples/java/
  schedule:
    interval: daily
- package-ecosystem: npm
  directory: /samples/npm/
  schedule:
    interval: daily
- package-ecosystem: nuget
  directory: /src/DCB.Tests/
  schedule:
    interval: daily
- package-ecosystem: nuget
  directory: /src/DCB/
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

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);

            string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files);

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

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);

            string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files);

            //assert
            string expected = @"version: 2
updates:
- package-ecosystem: nuget
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
}