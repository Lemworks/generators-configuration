using Microsoft.CodeAnalysis.CSharp;
using VerifyTUnit;

namespace AppSettingsGenerator.SnapshotTests;

public static class TestHelper
{
    public static Task Verify(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source, CSharpParseOptions.Default);
        
        var compilation = CSharpCompilation.Create(
            assemblyName: "Test",
            syntaxTrees: [syntaxTree]);

        var driver = CSharpGeneratorDriver.Create(new AppSettingsGenerator())
            .AddAdditionalTexts([new InMemoryAdditionalText(Path.DirectorySeparatorChar + "appsettings.json", source)])
            .RunGenerators(compilation);

        return Verifier.Verify(driver)
            .UseDirectory("Snapshots");
    }
}
