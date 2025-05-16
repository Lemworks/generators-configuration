using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AppSettingsGenerator.SnapshotTests;

[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
internal sealed class SnapshotTests
{
    [Test]
    public Task AppSettingsGeneratorTest()
    {
        var additionalText = new InMemoryAdditionalText(
            Path.DirectorySeparatorChar + "appsettings.json",
            """
            {
                "Test": "",
                "Object": {
                    "Value": 1,
                    "Object": {
                        "Something": null
                    }
                },
                "Last": ""
            }
            """);

        var driver = CSharpGeneratorDriver.Create(new AppSettingsGenerator())
            .AddAdditionalTexts([additionalText])
            .RunGenerators(CSharpCompilation.Create("Testing"));

        return Verify(driver)
            .UseDirectory("Snapshots");
    }
}
