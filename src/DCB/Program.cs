namespace DCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            List<string> files = FileSearch.GetFilesForDirectory(projectDirectory);
            Console.WriteLine(files.Count + " files found");

            string yaml = YAMLParser.CreateDependabotConfiguration(projectDirectory, files);
            Console.WriteLine(yaml);
        }
    }
}