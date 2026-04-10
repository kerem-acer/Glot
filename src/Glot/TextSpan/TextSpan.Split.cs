using System.Runtime.InteropServices;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Splits this span by the given separator, yielding sub-spans via a zero-allocation enumerator.</summary>
    public SplitEnumerator Split(TextSpan separator)
        => new(this, separator);

    /// <inheritdoc cref="Split(TextSpan)"/>
    public SplitEnumerator Split(ReadOnlySpan<byte> separator, TextEncoding encoding = TextEncoding.Utf8)
        => Split(Uncounted(separator, encoding));

    /// <inheritdoc cref="Split(TextSpan)"/>
    public SplitEnumerator Split(ReadOnlySpan<char> separator)
        => Split(Uncounted(MemoryMarshal.AsBytes(separator), TextEncoding.Utf16));

    /// <inheritdoc cref="Split(TextSpan)"/>
    public SplitEnumerator Split(ReadOnlySpan<int> separator)
        => Split(Uncounted(MemoryMarshal.AsBytes(separator), TextEncoding.Utf32));
}
