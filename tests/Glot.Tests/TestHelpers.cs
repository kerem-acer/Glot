using System.Runtime.InteropServices;

namespace Glot.Tests;

static class TestHelpers
{
    public static byte[] Encode(string s, TextEncoding encoding) => encoding switch
    {
        TextEncoding.Utf8 => System.Text.Encoding.UTF8.GetBytes(s),
        TextEncoding.Utf16 => System.Text.Encoding.Unicode.GetBytes(s),
        TextEncoding.Utf32 => System.Text.Encoding.UTF32.GetBytes(s),
        _ => throw new ArgumentOutOfRangeException(nameof(encoding)),
    };

    public static int CollectSplitCount(TextSpan source, TextSpan separator)
    {
        var count = 0;
        foreach (var _ in source.Split(separator))
        {
            count++;
        }

        return count;
    }

    public static string[] CollectSplitParts(TextSpan source, TextSpan separator)
    {
        var parts = new List<string>();
        foreach (var part in source.Split(separator))
        {
            var chars = MemoryMarshal.Cast<byte, char>(part.Bytes);
            parts.Add(new string(chars));
        }

        return parts.ToArray();
    }
}
