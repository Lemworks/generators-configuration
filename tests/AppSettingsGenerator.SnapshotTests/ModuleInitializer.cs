using System.Runtime.CompilerServices;

namespace AppSettingsGenerator.SnapshotTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init() => VerifySourceGenerators.Initialize();
}