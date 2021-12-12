﻿namespace DCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string startingDirectory = @"C:\Users\samsm\source\repos\Dependabot-Configuration-Builder";
            string fileToSearch = "*.csproj";
            List<string> files = FileSearch.GetFilesForDirectory(startingDirectory, fileToSearch);
            Console.WriteLine(files.Count + " files found");

            string yaml = YAMLParser.CreateDependabotConfiguration(startingDirectory, files);
            Console.WriteLine(yaml);
        }
    }
}