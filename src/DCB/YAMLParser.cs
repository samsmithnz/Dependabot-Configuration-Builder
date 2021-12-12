using DCB.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DCB
{
    public class YAMLParser
    {
        public static string CreateDependabotConfiguration(string? startingDirectory, List<string> files)
        {
            if (startingDirectory == null)
            {
                return "";
            }

            Root root = new();

            List<Package> packages = new();
            foreach (string file in files)
            {
                Package package = new();
                FileInfo fileInfo = new(file);
                if (fileInfo.Name == "pom.xml")
                {
                    package.package_ecosystem = "maven";
                }
                else if (fileInfo.Extension == ".csproj")
                {
                    package.package_ecosystem = "nuget";
                }
                string cleanedFile = file.Replace(startingDirectory + "/", "");
                cleanedFile = cleanedFile.Replace(startingDirectory + "\\", "");
                package.directory = cleanedFile;
                package.schedule = new();
                package.schedule.interval = "daily";
                packages.Add(package);
            }
            root.updates = packages;

            //Serialize the object into YAML
            ISerializer? serializer = new SerializerBuilder()
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults) //New as of YamlDotNet 8.0.0:
                                                                                    //.WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            string? yaml = serializer.Serialize(root);

            //I can't use - in variable names, so replace _ with -
            yaml = yaml.Replace("package_ecosystem", "package-ecosystem");

            return yaml;

        }


    }
}
