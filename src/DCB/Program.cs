﻿using CommandLine;
using GitHubActionsDotNet.Helpers;
using GitHubActionsDotNet.Serialization;

namespace DCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Parse arguments
            string workingDirectory = Environment.CurrentDirectory;
            List<string>? assignees = null;
            int openPRRequestsLimit = 0;
            string? interval = null;
            string? time = null;
            string? timezone = null;
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                if (!string.IsNullOrEmpty(o.Directory))
                {
                    workingDirectory = o.Directory;
                }
                if (!string.IsNullOrEmpty(o.Assignees))
                {
                    assignees = o.Assignees.Split(',').ToList<string>();
                }
                if (!string.IsNullOrEmpty(o.OpenPullRequestsLimit))
                {
                    if (int.TryParse(o.OpenPullRequestsLimit, out openPRRequestsLimit))
                    {
                        //do nothing
                    }
                }
                if (!string.IsNullOrEmpty(o.Interval))
                {
                    interval = o.Interval;
                }
                if (!string.IsNullOrEmpty(o.Time))
                {
                    time = o.Time;
                }
                if (!string.IsNullOrEmpty(o.TimeZone))
                {
                    timezone = o.TimeZone;
                }
            });

            //Get a list of package files
            List<string> files = FileSearch.GetFilesForDirectory(workingDirectory);
            //Console.WriteLine(files.Count + " files found in " + workingDirectory);

            //Create the yaml
            string yaml = DependabotSerialization.Serialize(workingDirectory, files, interval, time, timezone, assignees, openPRRequestsLimit);
            Console.WriteLine(yaml);
        }

    }
}