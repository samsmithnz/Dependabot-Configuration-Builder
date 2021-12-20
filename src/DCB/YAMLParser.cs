﻿using DCB.Models;
using YamlDotNet.Serialization;

namespace DCB
{
    public class YAMLParser
    {
        public static string CreateDependabotConfiguration(string? startingDirectory,
            List<string> files,
            string[]? assignees = null,
            int openPRLimit = 0,
            bool includeActions = true)
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
                package.package_ecosystem = Common.GetPackageEcoSystemFromFileName(fileInfo);
                string cleanedFile = file.Replace(startingDirectory + "/", "");
                cleanedFile = cleanedFile.Replace(startingDirectory + "\\", "");
                cleanedFile = cleanedFile.Replace(fileInfo.Name, "");
                cleanedFile = "/" + cleanedFile.Replace("\\", "/");
                package.directory = cleanedFile;
                package.schedule = new()
                {
                    interval = "daily"
                };
                //package.assignees = assignees;
                if (openPRLimit > 0)
                {
                    package.open_pull_requests_limit = openPRLimit.ToString();
                }
                packages.Add(package);
            }
            //Add actions
            if (includeActions == true)
            {
                Package actionsPackage = new();
                actionsPackage.package_ecosystem = "github-actions";
                actionsPackage.directory = "/";
                actionsPackage.schedule = new()
                {
                    interval = "daily"
                };
                //actionsPackage.assignees = assignees;
                if (openPRLimit > 0)
                {
                    actionsPackage.open_pull_requests_limit = openPRLimit.ToString();
                }
                packages.Add(actionsPackage);
            }
            root.updates = packages;

            //Serialize the object into YAML
            ISerializer? serializer = new SerializerBuilder()
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults) //New as of YamlDotNet 8.0.0:
                .Build();
            string? yaml = serializer.Serialize(root);

            //I can't use - in variable names, so replace _ with -
            yaml = yaml.Replace("package_ecosystem", "package-ecosystem");
            yaml = yaml.Replace("open_pull_requests_limit", "open-pull-requests-limit");
            //yaml = yaml.Replace("*o0", "");
            //yaml = yaml.Replace("&o0", "");

            return yaml;
        }

    }
}
