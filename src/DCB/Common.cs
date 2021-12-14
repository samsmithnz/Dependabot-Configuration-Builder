namespace DCB
{
    public class Common
    {
        public static List<string> GetFileTypesToSearch()
        {
            List<string> files = new List<string>();
            files.Add("pom.xml");
            files.Add("package.json");
            files.Add("nuget.config");
            files.Add("*.csproj");
            files.Add("*.vbproj");
            return files;
        }

        public static string GetPackageEcoSystemFromFileName(FileInfo fileInfo)
        {
            string packageEcosystem = "";
            if (fileInfo.Name == "pom.xml")
            {
                packageEcosystem = "maven";
            }
            else if (fileInfo.Name == "package.json")
            {
                packageEcosystem = "npm";
            }
            //else if (fileInfo.Name == "ruby.gems")
            //{
            //    result = "bundler";
            //}
            else if (fileInfo.Name == "nuget.config" ||
                fileInfo.Extension == ".csproj" ||
                fileInfo.Extension == ".vbproj")
            {
                packageEcosystem = "nuget";
            }
            return packageEcosystem;
        }
    }
}
