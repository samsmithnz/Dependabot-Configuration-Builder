# Dependabot-Configuration-Builder
Given a repository, scan all files for packages and generate the Dependabot configuration file

[![.NET](https://github.com/samsmithnz/Dependabot-Configuration-Builder/actions/workflows/dotnet.yml/badge.svg)](https://github.com/samsmithnz/Dependabot-Configuration-Builder/actions/workflows/dotnet.yml)

Currently scans for

| File | System | Package |
|--|--|--|
| nuget.config, *.csproj, *.vbproj | .NET | NuGet |
| pom.xml | Java | Maven |
| package.json | JavaScript | NPM |
| Gemfile, Gemfile.lock | Ruby | bundler |
| requirements.txt | Python | pip |
| *.yml | GitHub Actions | GitHub Actions |

See issues for details.

See the [official Dependabot docs](https://docs.github.com/en/code-security/supply-chain-security/keeping-your-dependencies-updated-automatically/configuration-options-for-dependency-updates) for more options

## Usage

```
DCB [-d|--directory <directory to scan>] [-a|--assignees <comma delimited list of assignees>]
```

## Example

Given this projects root directory + samples, produces the output:
```
version: 2
updates:
- package-ecosystem: nuget
  directory: /samples/dotnet/
  schedule:
    interval: daily
- package-ecosystem: maven
  directory: /samples/java/
  schedule:
    interval: daily
- package-ecosystem: npm
  directory: /samples/javascript/
  schedule:
    interval: daily
- package-ecosystem: pip
  directory: /samples/python/
  schedule:
    interval: daily
- package-ecosystem: bundler
  directory: /samples/ruby/
  schedule:
    interval: daily
- package-ecosystem: nuget
  directory: /src/DCB.Tests/
  schedule:
    interval: daily
- package-ecosystem: nuget
  directory: /src/DCB/
  schedule:
    interval: daily
- package-ecosystem: github-actions
  directory: /
  schedule:
    interval: daily
```
