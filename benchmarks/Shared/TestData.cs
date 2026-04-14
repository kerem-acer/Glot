using System.Collections.Frozen;

namespace Glot.Benchmarks;

static class TestData
{
    // Repeating patterns for Generate(). Each exercises a distinct UTF-8 byte-width profile.
    // Ascii:  1-byte chars, triggers SIMD/ASCII fast paths
    // Latin:  1–2 byte mix (ASCII + Latin Extended), multi-byte BMP boundary
    // Cjk:    3-byte chars (CJK Unified Ideographs), uniform long multi-byte BMP
    // Emoji:  4-byte chars (supplementary plane), surrogate pairs in UTF-16
    // Mixed:  all byte widths interleaved, stresses branch prediction
    static readonly FrozenDictionary<Script, string> Patterns = new Dictionary<Script, string>
    {
        [Script.Ascii] = "The quick brown fox jumps over the lazy dog and packs my box with dozens. ",
        [Script.Latin] = "Café résumé straße naïve über Ñoño piñata jalapeño crème brûlée. ",
        [Script.Cjk] = "快速的棕色狐狸跳过了懒惰的狗每天在公园里散步看风景。",
        [Script.Emoji] = "🎉🚀🌍💻🎯🔥🏆🎨🎵🌈🦊🐕💨🌟🏪🎭🎸🎺🎻🧩",
        [Script.Mixed] = "Hello café! 快速 order #42 🚀🌍 straße 计费 done. ",
    }.ToFrozenDictionary();

    // CSV segments for GenerateCsv(). Short, script-appropriate tokens joined by ", ".
    static readonly FrozenDictionary<Script, string> CsvSegments = new Dictionary<Script, string>
    {
        [Script.Ascii] = "billing",
        [Script.Latin] = "café",
        [Script.Cjk] = "计费",
        [Script.Emoji] = "💰📧",
        [Script.Mixed] = "café字🚀",
    }.ToFrozenDictionary();

    // Substrings guaranteed to exist in Generate() output.
    static readonly FrozenDictionary<Script, string> Needles = new Dictionary<Script, string>
    {
        [Script.Ascii] = "fox",
        [Script.Latin] = "café",
        [Script.Cjk] = "狐狸",
        [Script.Emoji] = "🚀🌍",
        [Script.Mixed] = "order",
    }.ToFrozenDictionary();

    // Substrings guaranteed NOT to exist in Generate() output.
    static readonly FrozenDictionary<Script, string> MissingNeedles = new Dictionary<Script, string>
    {
        [Script.Ascii] = "zxqj",
        [Script.Latin] = "züqj",
        [Script.Cjk] = "龙鹏",
        [Script.Emoji] = "🦄🦉",
        [Script.Mixed] = "鹏ü",
    }.ToFrozenDictionary();

    // Marker + replacement pairs for replace benchmarks. Markers are injected into source text.
    static readonly FrozenDictionary<Script, (string Marker, string Replacement)> MarkerPairs =
        new Dictionary<Script, (string, string)>
        {
            [Script.Ascii] = ("@", "[at]"),
            [Script.Latin] = ("@", "[at]"),
            [Script.Cjk] = ("@", "[at]"),
            [Script.Emoji] = ("🌟", "🧩🧩"),
            [Script.Mixed] = ("@", "[at]"),
        }.ToFrozenDictionary();

    /// <summary>
    /// Generates a string of at least <paramref name="targetLength"/> chars by repeating the pattern.
    /// Always cuts at valid char boundaries (never splits surrogate pairs).
    /// </summary>
    public static string Generate(int targetLength, Script script = Script.Ascii)
    {
        if (targetLength <= 0)
        {
            return "";
        }

        var pattern = Patterns[script];
        var sb = new System.Text.StringBuilder(targetLength + pattern.Length);
        while (sb.Length < targetLength)
        {
            sb.Append(pattern);
        }

        // Don't split surrogate pairs
        var len = targetLength;
        if (len < sb.Length && char.IsHighSurrogate(sb[len - 1]))
        {
            len++;
        }

        return sb.ToString(0, Math.Min(len, sb.Length));
    }

    public static string GenerateCsv(int targetLength, Script script = Script.Ascii)
    {
        var segment = CsvSegments[script];
        var sb = new System.Text.StringBuilder(targetLength + segment.Length);
        while (sb.Length < targetLength)
        {
            if (sb.Length > 0)
            {
                sb.Append(", ");
            }

            sb.Append(segment);
        }

        // Don't split surrogate pairs
        var len = Math.Min(sb.Length, targetLength);
        if (len > 0 && len < sb.Length && char.IsHighSurrogate(sb[len - 1]))
        {
            len++;
        }

        return sb.ToString(0, Math.Min(len, sb.Length));
    }

    /// <summary>
    /// Returns a short, script-appropriate needle string guaranteed to exist in
    /// <see cref="Generate"/> output and safe for all encodings.
    /// </summary>
    public static string Needle(Script script) => Needles[script];

    /// <summary>
    /// Returns a short, script-appropriate needle string guaranteed NOT to exist in
    /// <see cref="Generate"/> output. Used for "not found" search benchmarks.
    /// </summary>
    public static string MissingNeedle(Script script) => MissingNeedles[script];

    /// <summary>
    /// Returns a copy of <paramref name="value"/> with the last character changed,
    /// producing a string that differs only at the end. Safe for surrogate pairs.
    /// </summary>
    public static string Mutate(string value)
    {
        var chars = value.ToCharArray();
        var lastIdx = chars.Length - 1;
        if (lastIdx > 0 && char.IsLowSurrogate(chars[lastIdx]))
        {
            lastIdx--;
        }

        chars[lastIdx] = 'Z';
        if (lastIdx + 1 < chars.Length && char.IsLowSurrogate(chars[lastIdx + 1]))
        {
            chars[lastIdx + 1] = ' ';
        }

        return new string(chars);
    }

    /// <summary>
    /// Returns a marker+replacement pair that's safe for the given script (no surrogate splitting).
    /// </summary>
    public static (string Marker, string Replacement) MarkerPair(Script script) => MarkerPairs[script];
}
