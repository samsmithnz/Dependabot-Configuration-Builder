using CommandLine;

namespace DCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;

            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                if (string.IsNullOrEmpty(o.Directory) == false)
                {
                    workingDirectory = o.Directory;
                    //Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                    //Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                }
                //else
                //{
                //    Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                //    Console.WriteLine("Quick Start Example!");
                //}
            });

            List<string> files = FileSearch.GetFilesForDirectory(workingDirectory);
            Console.WriteLine(files.Count + " files found in " + workingDirectory);

            string yaml = YAMLParser.CreateDependabotConfiguration(workingDirectory, files);
            Console.WriteLine(yaml);
        }

        public class Options
        {
            [Option('d', "dir", Required = false, HelpText = "set working directory")]
            public string Directory { get; set; }
        }
    }
}