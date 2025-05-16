dotnet restore ./tests/AppSettingsGenerator.NugetIntegrationTests --packages ./packages --configfile "nuget.integration-tests.config"

dotnet build ./tests/AppSettingsGenerator.NugetIntegrationTests -c Release --packages ./packages --no-restore

dotnet test ./tests/AppSettingsGenerator.NugetIntegrationTests -c Release --no-build --no-restore