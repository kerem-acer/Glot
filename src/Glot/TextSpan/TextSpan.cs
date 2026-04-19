namespace Glot;

/// <summary>
/// A stack-only view over encoded text bytes. Cannot escape the stack.
/// </summary>
/// <remarks>
/// <para><see cref="TextSpan"/> normalizes all backing types (<see cref="string"/>, <c>char[]</c>, <c>int[]</c>)
/// into a <see cref="ReadOnlySpan{T}"/> of bytes at construction time. This enables a single code path
/// for all encodings.</para>
/// <para>Because this is a <c>ref struct</c>, it cannot be stored on the heap, used in async methods,
/// or captured in closures. Use <see cref="Text"/> for heap-safe scenarios.</para>
/// </remarks>
public readonly ref partial struct TextSpan
#if NET6_0_OR_GREATER
    : ISpanFormattable
#endif
#if NET8_0_OR_GREATER
    , IUtf8SpanFormattable
#endif
#if NET9_0_OR_GREATER
    , IEquatable<TextSpan>
    , IComparable<TextSpan>
#endif
{
    readonly EncodedLength _encodedLength;

    /// <summary>
    /// Creates a <see cref="TextSpan"/> over the given bytes with the specified encoding.
    /// </summary>
    /// <param name="bytes">The raw encoded bytes.</param>
    /// <param name="encoding">The Unicode encoding of the bytes.</param>
    /// <remarks>Counts runes during construction. Use <see cref="Text.AsSpan"/> to avoid recounting when the rune count is already known.</remarks>
    public TextSpan(ReadOnlySpan<byte> bytes, TextEncoding encoding)
    {
        Bytes = bytes;
        _encodedLength = new EncodedLength(encoding, RuneCount.Count(bytes, encoding));
    }

    internal TextSpan(ReadOnlySpan<byte> bytes, TextEncoding encoding, int runeLength)
    {
        Bytes = bytes;
        _encodedLength = new EncodedLength(encoding, runeLength);
    }

    /// <summary>
    /// Creates a <see cref="TextSpan"/> without counting runes.
    /// The resulting span has <see cref="RuneLength"/> == 0.
    /// Use only when the span is an intermediate value whose <see cref="RuneLength"/> is never read.
    /// </summary>
    static TextSpan Uncounted(ReadOnlySpan<byte> bytes, TextEncoding encoding)
        => new(bytes, encoding, 0);

    /// <summary>The raw underlying bytes, regardless of encoding.</summary>
    public ReadOnlySpan<byte> Bytes { get; }

    /// <summary>The number of bytes in this span.</summary>
    public int ByteLength => Bytes.Length;

    /// <summary>The Unicode encoding of the text in this span.</summary>
    public TextEncoding Encoding => _encodedLength.Encoding;

    /// <summary>Returns <c>true</c> if this span contains no bytes.</summary>
    public bool IsEmpty => Bytes.IsEmpty;

    /// <summary>The number of Unicode scalar values in this span.</summary>
    /// <remarks>Cached when constructed from a <see cref="Text"/> with a known rune count. Computed on first access otherwise.</remarks>
    public int RuneLength
    {
        get
        {
            var cached = _encodedLength.RuneLength;
            return cached != 0 || Bytes.IsEmpty ? cached : RuneCount.Count(Bytes, Encoding);
        }
    }
}
