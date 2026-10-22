using System.Runtime.CompilerServices;

namespace Glot.SystemTextJson.Analyzers.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize() => UseProjectRelativeDirectory("snapshots");
}
