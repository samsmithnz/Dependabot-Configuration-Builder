using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace DCB.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class ScanningTests
{

    [TestMethod]
    public void ScanDotnetSampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\dotnet";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        List<string> assignees = new() { "samsmithnz" };
        string assigneeList = "";
        for (int i = 0; i <= assignees.Count - 1; i++)
        {
            if (i + 1 == assignees.Count)
            {
                assigneeList += assignees[i].Trim();
            }
            else
            {
                assigneeList += assignees[i].Trim() + ",";
            }
        }
        int openPRLimit = 10;
        string interval = "daily";
        string time = "06:00";
        string timezone = "America/New_York";

        //act
        string actual = "";
        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            Program.Main(new string[] { "-d", projectDirectory, "-a", assigneeList, "-p", openPRLimit.ToString(), "-i", interval, "-t", time, "-z", timezone });
            actual = sw.ToString();
        }

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: nuget
  directory: /
  schedule:
    interval: daily
    time: ""06:00""
    timezone: America/New_York
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
    time: ""06:00""
    timezone: America/New_York
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
" + Environment.NewLine;

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
            actual = actual.Replace("\\", "/");
        }
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScanThisProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory?.Replace("\\", "/");
        }
        List<string> assignees = new() { "samsmithnz" };
        string assigneeList = "";
        for (int i = 0; i <= assignees.Count - 1; i++)
        {
            if (i + 1 == assignees.Count)
            {
                assigneeList += assignees[i].Trim();
            }
            else
            {
                assigneeList += assignees[i].Trim() + ",";
            }
        }
        string interval = "daily";
        int openPRLimit = 10;

        //act
        string actual = "";
        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            if (projectDirectory != null)
            {
                Program.Main(new string[] { "-d", projectDirectory, "-a", assigneeList, "-p", openPRLimit.ToString(), "-i", interval });
            }
            actual = sw.ToString();
        }

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: nuget
  directory: /samples/dotnet/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: maven
  directory: /samples/java/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: npm
  directory: /samples/javascript/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: pip
  directory: /samples/python/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: bundler
  directory: /samples/ruby/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: nuget
  directory: /src/DCB.Tests/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: nuget
  directory: /src/DCB/
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
  assignees:
  - samsmithnz
  open-pull-requests-limit: 10
" + Environment.NewLine;

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
            actual = actual.Replace("\\", "/");
        }
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScanPomSampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\java";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        string interval = "daily";

        //act
        string actual = "";
        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            Program.Main(new string[] { "-d", projectDirectory, "-i", interval });
            actual = sw.ToString();
        }

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: maven
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
" + Environment.NewLine;

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
            actual = actual.Replace("\\", "/");
        }
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScanNPMSampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\javascript";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        string interval = "daily";

        //act
        string actual = "";
        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            Program.Main(new string[] { "-d", projectDirectory, "-i", interval });
            actual = sw.ToString();
        }

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: npm
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
" + Environment.NewLine;

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
            actual = actual.Replace("\\", "/");
        }
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScanRubySampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\ruby";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        string interval = "daily";

        //act
        string actual = "";
        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            Program.Main(new string[] { "-d", projectDirectory, "-i", interval });
            actual = sw.ToString();
        }

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: bundler
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
" + Environment.NewLine;

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
            actual = actual.Replace("\\", "/");
        }
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScanPythonSampleProjectTest()
    {
        //arrange
        string workingDirectory = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        projectDirectory += "\\samples\\python";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            projectDirectory = projectDirectory.Replace("\\", "/");
        }
        string interval = "daily";

        //act
        string actual = "";
        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            Program.Main(new string[] { "-d", projectDirectory, "-i", interval });
            actual = sw.ToString();
        }

        //assert
        string expected = @"version: 2
updates:
- package-ecosystem: pip
  directory: /
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
" + Environment.NewLine;

        //If it's a Linux runner, reverse the brackets
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = expected.Replace("\\", "/");
            actual = actual.Replace("\\", "/");
        }
        Assert.AreEqual(expected, actual);
    }

}
