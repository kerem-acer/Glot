using BenchmarkDotNet.Attributes;

namespace Glot.Benchmarks;

public class ScriptParamsAttribute() : ParamsAttribute(
    Script.Ascii,
    Script.Cjk,
    Script.Emoji,
    Script.Mixed);

public class SearchSizeParamsAttribute() : ParamsAttribute(64, 4096, 65536);

public class EqualitySizeParamsAttribute() : ParamsAttribute(8, 256, 65536);

public class PartSizeParamsAttribute() : ParamsAttribute(1, 64, 1024);
