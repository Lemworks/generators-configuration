using Microsoft.CodeAnalysis.CSharp;
using VerifyTUnit;

namespace AppSettingsGenerator.SnapshotTests;

public static class TestHelper
{
    public static Task Verify(string source)
    {
        var driver = CSharpGeneratorDriver.Create(new AppSettingsGenerator())
            .AddAdditionalTexts([new InMemoryAdditionalText(Path.DirectorySeparatorChar + "appsettings.json", source)])
            .RunGenerators(CSharpCompilation.Create("Testing"));

        return Verifier.Verify(driver)
            .UseDirectory("Snapshots");
    }
}
