namespace DCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string startingDirectory = @"C:\Users\samsm\source\repos\Dependabot-Configuration-Builder";
            string fileToSearch = "*.csproj";
            List<string> files = GetFilesForDirectory(startingDirectory, fileToSearch);
            Console.WriteLine(files.Count + " files found");

            YAMLParser yamlParser = new YAMLParser();
            string yaml = yamlParser.CreateDependabotConfiguration(files);
            Console.WriteLine(yaml);
        }

        public static List<string> GetFilesForDirectory(string startingDirectory, string fileToSearch)
        {
            string[] files = Directory.GetFiles(startingDirectory, fileToSearch, SearchOption.AllDirectories);
            return files.ToList<string>();
        }
    }
}