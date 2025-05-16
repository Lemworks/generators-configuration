using System.Runtime.CompilerServices;

namespace AppSettingsGenerator.SnapshotTests;

internal static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}