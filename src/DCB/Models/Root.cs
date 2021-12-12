namespace DCB.Models
{
    public class Root
    {
        public Root()
        {
            version = "2";
        }

        public string version { get; set; }

        public List<Package> package_ecosystem { get; set; }
    }
}
