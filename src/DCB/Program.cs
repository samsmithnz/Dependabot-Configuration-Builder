using CommandLine;

namespace DCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Parse arguments
            string workingDirectory = Environment.CurrentDirectory;
            string[] assignees = null;
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                if (string.IsNullOrEmpty(o.Directory) == false)
                {
                    workingDirectory = o.Directory;
                }
                if (string.IsNullOrEmpty(o.Assignees) == false)
                {
                    assignees = o.Assignees.Split(',');
                }
            });

            //Get a list of package files
            List<string> files = FileSearch.GetFilesForDirectory(workingDirectory);
            Console.WriteLine(files.Count + " files found in " + workingDirectory);

            //Create the yaml
            string yaml = YAMLParser.CreateDependabotConfiguration(workingDirectory, files, assignees);
            Console.WriteLine(yaml);
        }

        public class Options
        {
            [Option('d', "directory", Required = false, HelpText = "set working directory")]
            public string? Directory { get; set; }

            [Option('a', "assignees", Required = false, HelpText = "set assignees, comma separated")]
            public string? Assignees { get; set; }
        }
    }
}