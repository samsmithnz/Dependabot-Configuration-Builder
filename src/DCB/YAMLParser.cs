using DCB.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DCB
{
    public class YAMLParser
    {
        public string CreateDependabotConfiguration(string startingDirectory, List<string> files)
        {
            Root root = new();

            List<Package> packages = new();
            foreach (string file in files)
            {
                Package package = new();
                package.package_ecosystem = "nuget";
                package.directory = file.Replace(startingDirectory + "\\", "");
                package.schedule = new();
                package.schedule.interval = "daily";
                packages.Add(package);
            }
            root.updates = packages;

            //        var root = new Root
            //        {
            //            Name = "Abe Lincoln",
            //            Age = 25,
            //            HeightInInches = 6f + 4f / 12f,
            //            Addresses = new Dictionary<string, Address>{
            //    { "home", new  Address() {
            //            Street = "2720  Sundown Lane",
            //            City = "Kentucketsville",
            //            State = "Calousiyorkida",
            //            Zip = "99978",
            //        }},
            //    { "work", new  Address() {
            //            Street = "1600 Pennsylvania Avenue NW",
            //            City = "Washington",
            //            State = "District of Columbia",
            //            Zip = "20500",
            //        }},
            //}
            //        };

            ISerializer? serializer = new SerializerBuilder()
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults) //New as of YamlDotNet 8.0.0:
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            string? yaml = serializer.Serialize(root);
            //System.Console.WriteLine(yaml);

            return yaml;

        }


    }
}
