namespace DCB.Models
{
    public class Root
    {
        public Root()
        {
            version = "2";
            updates = new();
        }

        public string version { get; set; }

        public List<Package> updates { get; set; }
    }
}
