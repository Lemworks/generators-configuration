namespace AppSettingsGenerator;

public record AppSettingsFile
{
    public readonly string FileName;
    
    public readonly string Text;

    public AppSettingsFile(string fileName, string? text)
    {
        FileName = fileName;
        Text = string.IsNullOrWhiteSpace(text) ? "{}" : text!;
    }
}