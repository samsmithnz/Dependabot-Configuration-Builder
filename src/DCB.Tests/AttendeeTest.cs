using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace DCB.Tests
{
    [TestClass]
    public class AttendeeTest
    {
        [TestMethod]
        public void SerializationTest()
        {
            //Arrange
            var root = new
            {
                version = "2",
                updates = new[] {
                    new {
                        package_ecosystem = "nuget",
                        directory = "/",
                        schedule = new {
                            interval= "daily"
                        },
                        attendees = new List<string>()
                        {
                            "Sam"
                        }
                    },
                    new {
                        package_ecosystem = "github-actions",
                        directory = "/",
                        schedule = new {
                            interval= "daily"
                        },
                        attendees = new List<string>()
                        {
                            "Sam"
                        }
                    }
                }
            };

            //Act
            ISerializer serializer = new SerializerBuilder()
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults) //New as of YamlDotNet 8.0.0:
                .Build();
            string yaml = serializer.Serialize(root);

            //Assert
            string expected = @"version: 2
updates:
- package_ecosystem: nuget
  directory: /
  schedule:
    interval: daily
  attendees:
  - Sam
- package_ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
  attendees:
  - Sam
";

            Assert.AreEqual(expected, yaml);
        }
    }
}
