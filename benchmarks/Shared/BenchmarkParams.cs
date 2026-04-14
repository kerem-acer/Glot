using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

public class ScriptParamsAttribute() : ParamsAttribute(
    Script.Ascii,
    Script.Latin,
    Script.Cjk,
    Script.Emoji,
    Script.Mixed);

public class SearchSizeParamsAttribute() : ParamsAttribute(64, 256, 4096, 65536);

public class EqualitySizeParamsAttribute() : ParamsAttribute(8, 64, 256, 4096, 65536);

public class PartSizeParamsAttribute() : ParamsAttribute(1, 8, 64, 1024);
