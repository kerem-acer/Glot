namespace Glot.Benchmarks;

public record GlotEvent(Text Source, Text Level, Text Message, Text Tags);
public record StringEvent(string Source, string Level, string Message, string Tags);
