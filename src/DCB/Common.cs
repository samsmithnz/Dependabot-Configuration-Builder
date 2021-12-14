namespace DCB
{
    public class Common
    {
        public static List<string> GetFileTypesToSearch()
        {
            List<string> files = new List<string>();
            files.Add("*.csproj");
            files.Add("pom.xml");
            return files;
        }

        public static string GetPackageEcoSystemFromFileName(FileInfo fileInfo)
        {
            string result = "";
            if (fileInfo.Name == "pom.xml")
            {
                result = "maven";
            }
            else if (fileInfo.Name == "package.json")
            {
                result = "npm";
            }
            else if (fileInfo.Extension == ".csproj")
            {
                result = "nuget";
            }
            return result;
        }
    }
}
