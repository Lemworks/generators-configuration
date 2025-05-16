using System.Text;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Lemworks.Generators.Configuration;

[Generator]
internal sealed class AppSettingsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var filename = Path.DirectorySeparatorChar + "appsettings.json";

        var appsettings = context.AdditionalTextsProvider
            .Where(text => text.Path.EndsWith(filename))
            .Select(static (text, cancellationToken) =>
                new AppSettingsFile(
                    fileName: Path.GetFileName(text.Path),
                    text: text.GetText(cancellationToken)?.ToString()));

        context.RegisterSourceOutput(appsettings, Execute);
    }

    private static void Execute(SourceProductionContext source, AppSettingsFile appSettingsFile)
    {
        StringBuilder builder = new();

        builder.AppendLine(
            """
            using System;

            namespace Lemworks.Configuration
            {
                public static class AppSettings
            """);

        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(appSettingsFile.Text), new JsonReaderOptions
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip
        });

        var depth = 1;
        Stack<string> stack = new();
        while (reader.Read())
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.PropertyName:
                {
                    stack.Push(reader.GetString()!);
                    break;
                }
                case JsonTokenType.StartObject:
                {
                    if (stack.Count > 0)
                    {
                        builder.Append(Indent(depth));
                        builder.Append("public static class ");
                        builder.AppendLine(stack.First());
                    }

                    builder.Append(Indent(depth));
                    builder.Append('{');
                    builder.AppendLine();
                    depth++;
                    break;
                }
                case JsonTokenType.EndObject:
                {
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                    }

                    depth--;
                    builder.Append(Indent(depth));
                    builder.Append('}');
                    builder.AppendLine();
                    break;
                }
                case JsonTokenType.StartArray:
                case JsonTokenType.EndArray:
                case JsonTokenType.String:
                case JsonTokenType.Number:
                case JsonTokenType.True:
                case JsonTokenType.False:
                case JsonTokenType.Null:
                {
                    if (stack.Count > 0)
                    {
                        builder.Append(Indent(depth));
                        builder.Append("public const string ");
                        builder.Append(stack.First());
                        builder.Append(" = \"");
                        builder.Append(string.Join(":", stack.Reverse()));
                        builder.AppendLine("\";");
                        stack.Pop();
                    }

                    break;
                }
                default:
                {
                    continue;
                }
            }

        }

        builder.Append('}');

        source.AddSource(
            $"{appSettingsFile.FileName}.g.cs",
            SourceText.From(builder.ToString(), Encoding.UTF8));
    }

    private static string Indent(int depth) => new(' ', depth * 4);
}
