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
            string yaml = yamlParser.CreateDependabotConfiguration(files);

            //assert
            string expected = @"version: 2
updates: 
";
            Assert.AreEqual(expected, yaml);
        }
    }
}