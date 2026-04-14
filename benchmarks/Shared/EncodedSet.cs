using U8;

namespace Glot.Benchmarks;

public record struct EncodedSet(string Str,
    Text Utf8,
    Text Utf16,
    Text Utf32,
    U8String U8)
{
    public static EncodedSet From(string value)
    {
        var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(value);
        return new EncodedSet(
            value,
            Text.FromUtf8(utf8Bytes),
            Text.From(value),
            Text.FromUtf32(ToCodePoints(value)),
            new U8String(utf8Bytes));
    }

    static int[] ToCodePoints(string s)
    {
        var runes = new List<int>();
        foreach (var rune in s.EnumerateRunes())
        {
            runes.Add(rune.Value);
        }

        return runes.ToArray();
    }
}
