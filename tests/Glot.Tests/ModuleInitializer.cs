using System.Runtime.CompilerServices;

namespace Glot.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize() => UseProjectRelativeDirectory("snapshots");
}
