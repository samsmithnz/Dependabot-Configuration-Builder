# Dependabot-Configuration-Builder
Given a repository, scan all files for packages and generate the Dependabot configuration file

[![.NET](https://github.com/samsmithnz/Dependabot-Configuration-Builder/actions/workflows/dotnet.yml/badge.svg)](https://github.com/samsmithnz/Dependabot-Configuration-Builder/actions/workflows/dotnet.yml)

Currently scans for
- csproj (.NET C# project files), defaulting to nuget
- pom.xml (Java), defaulting to maven

See issues for details. Current implementation is an evenings work to support .NET/NuGet and Java/Pom/Maven, but this could potentially solve scale issues with Log4j/pom.xml.

See the [official docs](https://docs.github.com/en/code-security/supply-chain-security/keeping-your-dependencies-updated-automatically/configuration-options-for-dependency-updates) for more options

## Example

Given this projects root directory + samples, produces the output:
```
version: 2
updates:
- package-ecosystem: nuget
  directory: samples\dotnet\Dotnet.csproj
  schedule:
    interval: daily
- package-ecosystem: nuget
  directory: src\DCB.Tests\DCB.Tests.csproj
  schedule:
    interval: daily
- package-ecosystem: nuget
  directory: src\DCB\DCB.csproj
  schedule:
    interval: daily
```

And:

```
version: 2
updates:
- package-ecosystem: maven
  directory: pom.xml
  schedule:
    interval: daily
```
