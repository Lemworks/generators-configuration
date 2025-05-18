Remove-Item -Path $PSScriptRoot\artifacts\*.0.0.1-integration-test.nupkg -ErrorAction SilentlyContinue

Remove-Item -Path $PSScriptRoot\packages\ -Recurse -ErrorAction SilentlyContinue

dotnet pack -c Release -o $PSScriptRoot\artifacts -p:Version=0.0.1-integration-test

dotnet restore $PSScriptRoot\tests\AppSettingsGenerator.NugetIntegrationTests --packages $PSScriptRoot\packages --configfile $PSScriptRoot\nuget.integration-tests.config

dotnet build $PSScriptRoot\tests\AppSettingsGenerator.NugetIntegrationTests -c Release --packages $PSScriptRoot\packages --no-restore

dotnet test $PSScriptRoot\tests\AppSettingsGenerator.NugetIntegrationTests -c Release --no-build --no-restore