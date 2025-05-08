namespace AppSettingsGenerator;

public readonly struct AppSettingsFile
{
    public readonly string FileName;
    
    public readonly string Text;

    public AppSettingsFile(string fileName, string? text)
    {
        FileName = fileName;
        Text = string.IsNullOrWhiteSpace(text) ? "{}" : text!;
    }
}