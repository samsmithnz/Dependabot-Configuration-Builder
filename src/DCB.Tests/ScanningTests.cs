using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

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
  directory: src\DCB\DCB.csproj
  schedule:
    interval: daily
- packageEcosystem: nuget
  directory: src\DCB.Tests\DCB.Tests.csproj
  schedule:
    interval: daily
";

//If it's a Linux runner, reverse the brackets
#if (LINUX)
    expected = expected.Replace("\\","/");
#endif
            Assert.AreEqual(expected, yaml);
        }
    }
}