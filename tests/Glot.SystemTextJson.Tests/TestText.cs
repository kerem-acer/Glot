using System.Text;

namespace Glot.SystemTextJson.Tests;

static class TestText
{
    /// <summary>Creates a <see cref="Text"/> holding <paramref name="value"/> in <paramref name="encoding"/>.</summary>
    public static Text Encode(string value, TextEncoding encoding) => encoding switch
    {
        TextEncoding.Utf8 => Text.FromUtf8(Encoding.UTF8.GetBytes(value)),
        TextEncoding.Utf16 => Text.From(value),
        _ => Text.FromBytes(Encoding.UTF32.GetBytes(value), TextEncoding.Utf32),
    };
}
