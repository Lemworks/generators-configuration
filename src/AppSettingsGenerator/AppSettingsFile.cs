namespace AppSettingsGenerator;

internal sealed record AppSettingsFile
{
    public string FileName { get; }
    
    public string Text  { get; }

    public AppSettingsFile(string fileName, string? text)
    {
        FileName = fileName;
        Text = !string.IsNullOrWhiteSpace(text) ? text! : "{}";
    }
}