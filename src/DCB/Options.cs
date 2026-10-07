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

        [Option('c', "cooldown-default-days", Required = false, HelpText = "Default number of days to wait before creating update pull requests")]
        public int? CooldownDefaultDays { get; set; }

        [Option("cooldown-semver-major-days", Required = false, HelpText = "Number of days to wait before creating major update pull requests")]
        public int? CooldownSemverMajorDays { get; set; }

        [Option("cooldown-semver-minor-days", Required = false, HelpText = "Number of days to wait before creating minor update pull requests")]
        public int? CooldownSemverMinorDays { get; set; }

        [Option("cooldown-semver-patch-days", Required = false, HelpText = "Number of days to wait before creating patch update pull requests")]
        public int? CooldownSemverPatchDays { get; set; }

        [Option("cooldown-include", Required = false, HelpText = "Comma-separated dependency names to apply cooldown to")]
        public string? CooldownInclude { get; set; }

        [Option("cooldown-exclude", Required = false, HelpText = "Comma-separated dependency names to exclude from cooldown")]
        public string? CooldownExclude { get; set; }

    }
}
