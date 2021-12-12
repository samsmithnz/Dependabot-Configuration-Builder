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
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.Parent.FullName;
            string fileToSearch = "*.csproj";

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory, fileToSearch);

            YAMLParser yamlParser = new();
            string yaml = yamlParser.CreateDependabotConfiguration(projectDirectory, files);

            //assert
            string expected = @"version: 2
updates:
- packageEcosystem: nuget
  directory: src\DCB.Tests\DCB.Tests.csproj
  schedule:
    interval: daily
- packageEcosystem: nuget
  directory: src\DCB\DCB.csproj
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
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.Parent.FullName;
            projectDirectory += "\\Samples\\Pom";
            string fileToSearch = "pom.xml";

            //act
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory, fileToSearch);

            YAMLParser yamlParser = new();
            string yaml = yamlParser.CreateDependabotConfiguration(projectDirectory, files);

            //assert
            string expected = @"version: 2
updates:
- packageEcosystem: maven
  directory: pom.xml
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