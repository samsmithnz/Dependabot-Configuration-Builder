using CommandLine;
using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Serialization;
using YamlDotNet.RepresentationModel;

namespace DCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Parse arguments
            string workingDirectory = Environment.CurrentDirectory;
            List<string>? assignees = null;
            int openPRRequestsLimit = 0;
            string? interval = null;
            string? time = null;
            string? timezone = null;
            int? cooldownDefaultDays = null;
            int? cooldownSemverMajorDays = null;
            int? cooldownSemverMinorDays = null;
            int? cooldownSemverPatchDays = null;
            string? cooldownInclude = null;
            string? cooldownExclude = null;
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                if (!string.IsNullOrEmpty(o.Directory))
                {
                    workingDirectory = o.Directory;
                }
                if (!string.IsNullOrEmpty(o.Assignees))
                {
                    assignees = o.Assignees.Split(',').ToList<string>();
                }
                if (!string.IsNullOrEmpty(o.OpenPullRequestsLimit))
                {
                    if (int.TryParse(o.OpenPullRequestsLimit, out openPRRequestsLimit))
                    {
                        //do nothing
                    }
                }
                if (!string.IsNullOrEmpty(o.Interval))
                {
                    interval = o.Interval;
                }
                if (!string.IsNullOrEmpty(o.Time))
                {
                    time = o.Time;
                }
                if (!string.IsNullOrEmpty(o.TimeZone))
                {
                    timezone = o.TimeZone;
                }
                cooldownDefaultDays = o.CooldownDefaultDays;
                cooldownSemverMajorDays = o.CooldownSemverMajorDays;
                cooldownSemverMinorDays = o.CooldownSemverMinorDays;
                cooldownSemverPatchDays = o.CooldownSemverPatchDays;
                cooldownInclude = o.CooldownInclude;
                cooldownExclude = o.CooldownExclude;
            });

            //Get a list of package files
            List<string> files = FileSearch.GetFilesForDirectory(workingDirectory);
            //Console.WriteLine(files.Count + " files found in " + workingDirectory);

            //Create the yaml
            string yaml = DependabotSerialization.Serialize(workingDirectory, files, interval, time, timezone, assignees, openPRRequestsLimit);
            if (cooldownDefaultDays.HasValue ||
                cooldownSemverMajorDays.HasValue ||
                cooldownSemverMinorDays.HasValue ||
                cooldownSemverPatchDays.HasValue ||
                !string.IsNullOrWhiteSpace(cooldownInclude) ||
                !string.IsNullOrWhiteSpace(cooldownExclude))
            {
                YamlStream yamlStream = new();
                yamlStream.Load(new StringReader(yaml));
                YamlMappingNode root = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                YamlSequenceNode updates = (YamlSequenceNode)root.Children[new YamlScalarNode("updates")];

                foreach (YamlMappingNode update in updates.Children)
                {
                    YamlMappingNode cooldown = new();
                    AddCooldownValue(cooldown, "default-days", cooldownDefaultDays);

                    YamlScalarNode ecosystem = (YamlScalarNode)update.Children[new YamlScalarNode("package-ecosystem")];
                    if (ecosystem.Value != "github-actions")
                    {
                        AddCooldownValue(cooldown, "semver-major-days", cooldownSemverMajorDays);
                        AddCooldownValue(cooldown, "semver-minor-days", cooldownSemverMinorDays);
                        AddCooldownValue(cooldown, "semver-patch-days", cooldownSemverPatchDays);
                    }

                    AddCooldownDependencies(cooldown, "include", cooldownInclude);
                    AddCooldownDependencies(cooldown, "exclude", cooldownExclude);

                    if (cooldown.Children.Count > 0)
                    {
                        update.Add(new YamlScalarNode("cooldown"), cooldown);
                    }
                }

                using StringWriter writer = new();
                yamlStream.Save(writer, assignAnchors: false);
                yaml = writer.ToString();
            }
            Console.WriteLine(yaml);
        }

        private static void AddCooldownValue(YamlMappingNode cooldown, string name, int? value)
        {
            if (value.HasValue)
            {
                cooldown.Add(new YamlScalarNode(name), new YamlScalarNode(value.Value.ToString()));
            }
        }

        private static void AddCooldownDependencies(YamlMappingNode cooldown, string name, string? dependencies)
        {
            if (!string.IsNullOrWhiteSpace(dependencies))
            {
                string[] values = dependencies.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                YamlSequenceNode sequence = new();
                foreach (string value in values)
                {
                    sequence.Add(new YamlScalarNode(value));
                }
                if (sequence.Children.Count > 0)
                {
                    cooldown.Add(new YamlScalarNode(name), sequence);
                }
            }
        }

    }
}