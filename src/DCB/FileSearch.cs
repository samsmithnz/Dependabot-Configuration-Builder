namespace DCB
{
    public class FileSearch
    {
        public static List<string> GetFilesForDirectory(string startingDirectory, string fileToSearch)
        {
            string[] files = Directory.GetFiles(startingDirectory, fileToSearch, SearchOption.AllDirectories);
            return files.ToList<string>();
        }
    }
}
