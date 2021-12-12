using System.Linq;

namespace DCB
{
    public class FileSearch
    {
        public static List<string> GetFilesForDirectory(string? startingDirectory, string fileToSearch)
        {
            if (startingDirectory == null)
            {
                return new();
            }
            string[] files = Directory.GetFiles(startingDirectory, fileToSearch, SearchOption.AllDirectories);
            //Order files alphabetically - there is some variation on different OS's on order
            List<string> sortedFiles = files.ToList<string>();
            sortedFiles.Sort();
            return sortedFiles;
        }
    }
}
