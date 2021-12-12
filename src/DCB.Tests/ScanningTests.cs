using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DCB.Tests
{
    [TestClass]
    public class ScanningTests
    {
        [TestMethod]
        public void ScanTest()
        {
            //arrange
            string startingDirectory = @"C:\Users\samsm\source\repos\Dependabot-Configuration-Builder";
            string fileToSearch = "*.csproj";

            //act
            List<string> files = FileSearch.GetFilesForDirectory(startingDirectory, fileToSearch);

            YAMLParser yamlParser = new();
            string yaml = yamlParser.CreateDependabotConfiguration(startingDirectory, files);

            //assert
            string expected = @"version: 2
updates:
- packageEcosystem: nuget
  directory: src\DCB\DCB.csproj
  schedule: 
- packageEcosystem: nuget
  directory: src\DCB.Tests\DCB.Tests.csproj
  schedule: 
";
            Assert.AreEqual(expected, yaml);
        }
    }
}