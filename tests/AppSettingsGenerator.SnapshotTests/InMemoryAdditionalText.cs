using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace AppSettingsGenerator.SnapshotTests;

internal sealed class InMemoryAdditionalText : AdditionalText
{
    private readonly string _text;
    
    public override string Path { get; }

    public InMemoryAdditionalText(string path, string text)
    {
        Path = path;
        _text = text;
    }

    public override SourceText GetText(CancellationToken cancellationToken = default) =>
        SourceText.From(_text);
}