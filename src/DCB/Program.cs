namespace DCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            //string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            List<string> files = FileSearch.GetFilesForDirectory(workingDirectory);
            Console.WriteLine(files.Count + " files found in " + workingDirectory);

            string yaml = YAMLParser.CreateDependabotConfiguration(workingDirectory, files);
            Console.WriteLine(yaml);
        }
    }
}