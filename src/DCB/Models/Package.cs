namespace DCB.Models
{
    public class Package
    {
        public string? package_ecosystem { get; set; }
        public string? directory { get; set; }
        public Schedule? schedule { get; set; }
        public string[]? assignees { get; set; }
        public string? open_pull_requests_limit {get;set;}
    }
}
