Remove-Item -Path ./artifacts/*.0.0.1-integration-test.nupkg -ErrorAction SilentlyContinue

Remove-Item -Path ./packages/ -Recurse -ErrorAction SilentlyContinue

dotnet pack -c Release -o ./artifacts -p:Version=0.0.1-integration-test

dotnet restore ./tests/AppSettingsGenerator.NugetIntegrationTests --packages ./packages --configfile "nuget.integration-tests.config"

dotnet build ./tests/AppSettingsGenerator.NugetIntegrationTests -c Release --packages ./packages --no-restore

dotnet test ./tests/AppSettingsGenerator.NugetIntegrationTests -c Release --no-build --no-restore