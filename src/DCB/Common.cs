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

    }
}
