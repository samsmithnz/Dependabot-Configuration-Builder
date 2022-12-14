using CommandLine;

namespace DCB
{
    public class Options
    {
        [Option('d', "directory", Required = false, HelpText = "set working directory")]
        public string? Directory { get; set; }

        [Option('a', "assignees", Required = false, HelpText = "set assignees, comma separated")]
        public string? Assignees { get; set; }

        [Option('p', "prlimit", Required = false, HelpText = "set max number of open pull requests")]
        public string? OpenPullRequestsLimit { get; set; }

        [Option('i', "interval", Required = false, HelpText = "Inteval, e.g. daily")]
        public string? Interval { get; set; }

        [Option('t', "time", Required = false, HelpText = "Time, e.g. 06:00")]
        public string? Time { get; set; }

        [Option('z', "timezone", Required = false, HelpText = "Timezone to use, e.g. America/New_York")]
        public string? TimeZone { get; set; }

    }
}
