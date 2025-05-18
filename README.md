# Lemworks.Generators.Configuration
[![Latest version](https://img.shields.io/nuget/v/Lemworks.Generators.Configuration)](https://www.nuget.org/packages/Lemworks.Generators.Configuration/)
![License: MIT](https://img.shields.io/github/license/Lemworks/generators-configuration)
![NuGet Downloads](https://img.shields.io/nuget/dt/Lemworks.Generators.Configuration)


Source-generated configuration keys from your appsettings.json

## Setup
Install the package
```shell
dotnet add package Lemworks.Generators.Configuration
```

Add the following to your .csproj file
```xml
<ItemGroup>
    <AdditionalFiles Include="appsettings.json" />
    <Content Include="appsettings.json" CopyToOutputDirectory="Always" /> <!-- Not needed in Web projects -->
</ItemGroup>
```

Create an `appsettings.json` file with some content
```json
{
  "ConnectionStrings": {
    "SqlServer": "Server=(localdb)\\mssqllocaldb;Database=DatabaseName;Trusted_Connection=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

The keys will be generated as const strings in a `AppSettings` class
```csharp
using System;

namespace Generated.Configuration
{
    public static class AppSettings
    {
        public static class ConnectionStrings
        {
            public const string SqlServer = "ConnectionStrings:SqlServer";
        }
        
        public static class Logging
        {
            public static class LogLevel
            {
                public const string Default = "Logging:LogLevel:Default";
                public const string Microsoft = "Logging:LogLevel:Microsoft";
            }
        }
    }
}
```

Use the keys in your code like this
```csharp
using Generated.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
        
configuration.GetConnectionString(AppSettings.ConnectionStrings.SqlServer) // "Server=(localdb)\\mssqllocaldb;Database=DatabaseName;Trusted_Connection=True"
configuration[AppSettings.Logging.LogLevel.Default] // "Information"
configuration[AppSettings.Logging.LogLevel.Microsoft] // "Warning"
```