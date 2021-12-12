namespace DCB
{
    public class FileSearch
    {
        public static List<string> GetFilesForDirectory(string? startingDirectory)
        {
            if (startingDirectory == null)
            {
                return new();
            }
            List<string> fileTypes = Common.GetFileTypesToSearch();
            List<string> sortedFiles = new();
            foreach (string fileType in fileTypes)
            {
                string[] files = Directory.GetFiles(startingDirectory, fileType, SearchOption.AllDirectories);
                //Order files alphabetically - there is some variation on different OS's on order
                sortedFiles.AddRange(files.ToList<string>());
            }
            sortedFiles.Sort();
            return sortedFiles;
        }
    }
}
