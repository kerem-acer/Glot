namespace Glot;

public readonly partial struct Text
{
    /// <summary>Returns <c>true</c> if this text contains the specified value, compared rune-by-rune across encodings.</summary>
    public bool Contains(Text value) => AsSpan().Contains(value.AsSpan());

    public bool Contains(string value) => AsSpan().Contains(value.AsSpan());

    public bool Contains(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().Contains(value, encoding);

    public bool Contains(ReadOnlySpan<char> value)
        => AsSpan().Contains(value);

    public bool Contains(ReadOnlySpan<int> value)
        => AsSpan().Contains(value);

    /// <summary>Returns <c>true</c> if this text begins with <paramref name="value"/>, compared rune-by-rune across encodings.</summary>
    public bool StartsWith(Text value) => AsSpan().StartsWith(value.AsSpan());

    public bool StartsWith(string value) => AsSpan().StartsWith(value.AsSpan());

    public bool StartsWith(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().StartsWith(value, encoding);

    public bool StartsWith(ReadOnlySpan<char> value)
        => AsSpan().StartsWith(value);

    public bool StartsWith(ReadOnlySpan<int> value)
        => AsSpan().StartsWith(value);

    /// <summary>Returns <c>true</c> if this text ends with <paramref name="value"/>, compared rune-by-rune across encodings.</summary>
    public bool EndsWith(Text value) => AsSpan().EndsWith(value.AsSpan());

    public bool EndsWith(string value) => AsSpan().EndsWith(value.AsSpan());

    public bool EndsWith(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().EndsWith(value, encoding);

    public bool EndsWith(ReadOnlySpan<char> value)
        => AsSpan().EndsWith(value);

    public bool EndsWith(ReadOnlySpan<int> value)
        => AsSpan().EndsWith(value);

    /// <summary>Returns the zero-based rune index of the first occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int RuneIndexOf(Text value) => AsSpan().RuneIndexOf(value.AsSpan());

    public int RuneIndexOf(string value) => AsSpan().RuneIndexOf(value.AsSpan());

    public int RuneIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().RuneIndexOf(value, encoding);

    public int RuneIndexOf(ReadOnlySpan<char> value)
        => AsSpan().RuneIndexOf(value);

    public int RuneIndexOf(ReadOnlySpan<int> value)
        => AsSpan().RuneIndexOf(value);

    /// <summary>Returns the zero-based rune index of the last occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int LastRuneIndexOf(Text value) => AsSpan().LastRuneIndexOf(value.AsSpan());

    public int LastRuneIndexOf(string value) => AsSpan().LastRuneIndexOf(value.AsSpan());

    public int LastRuneIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().LastRuneIndexOf(value, encoding);

    public int LastRuneIndexOf(ReadOnlySpan<char> value)
        => AsSpan().LastRuneIndexOf(value);

    public int LastRuneIndexOf(ReadOnlySpan<int> value)
        => AsSpan().LastRuneIndexOf(value);

    /// <summary>Returns the zero-based byte offset of the first occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int ByteIndexOf(Text value) => AsSpan().ByteIndexOf(value.AsSpan());

    public int ByteIndexOf(string value) => AsSpan().ByteIndexOf(value.AsSpan());

    public int ByteIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().ByteIndexOf(value, encoding);

    public int ByteIndexOf(ReadOnlySpan<char> value)
        => AsSpan().ByteIndexOf(value);

    public int ByteIndexOf(ReadOnlySpan<int> value)
        => AsSpan().ByteIndexOf(value);

    /// <summary>Returns the zero-based byte offset of the last occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int LastByteIndexOf(Text value) => AsSpan().LastByteIndexOf(value.AsSpan());

    public int LastByteIndexOf(string value) => AsSpan().LastByteIndexOf(value.AsSpan());

    public int LastByteIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().LastByteIndexOf(value, encoding);

    public int LastByteIndexOf(ReadOnlySpan<char> value)
        => AsSpan().LastByteIndexOf(value);

    public int LastByteIndexOf(ReadOnlySpan<int> value)
        => AsSpan().LastByteIndexOf(value);

    /// <summary>Removes leading and trailing whitespace. Returns a non-copying sub-view of the same backing data.</summary>
    public Text Trim() => TrimStart().TrimEnd();

    public Text TrimStart()
    {
        var original = AsSpan();
        var trimmed = original.TrimStart();
        var bytesRemoved = original.ByteLength - trimmed.ByteLength;
        return new Text(_data, _start + bytesRemoved, trimmed.ByteLength, trimmed.Encoding, trimmed.RuneLength);
    }

    public Text TrimEnd()
    {
        var trimmed = AsSpan().TrimEnd();
        return new Text(_data, _start, trimmed.ByteLength, trimmed.Encoding, trimmed.RuneLength);
    }

    /// <summary>Returns a non-copying sub-view starting at <paramref name="runeOffset"/>.</summary>
    public Text RuneSlice(int runeOffset)
    {
        if ((uint)runeOffset > (uint)RuneLength)
        {
            throw new ArgumentOutOfRangeException(nameof(runeOffset));
        }

        var span = AsSpan();
        var byteOffset = RuneIndex.ToByteOffset(span.Bytes, Encoding, runeOffset);
        return new Text(_data, _start + byteOffset, ByteLength - byteOffset, Encoding, RuneLength - runeOffset);
    }

    public Text RuneSlice(int runeOffset, int runeCount)
    {
        if ((uint)runeOffset > (uint)RuneLength)
        {
            throw new ArgumentOutOfRangeException(nameof(runeOffset));
        }

        if ((uint)runeCount > (uint)(RuneLength - runeOffset))
        {
            throw new ArgumentOutOfRangeException(nameof(runeCount));
        }

        var span = AsSpan();
        var byteOffset = RuneIndex.ToByteOffset(span.Bytes, Encoding, runeOffset);
        var byteCount = RuneIndex.ToByteOffset(span.Bytes[byteOffset..], Encoding, runeCount);
        return new Text(_data, _start + byteOffset, byteCount, Encoding, runeCount);
    }

    public Text ByteSlice(int byteOffset)
    {
        if ((uint)byteOffset > (uint)ByteLength)
        {
            throw new ArgumentOutOfRangeException(nameof(byteOffset));
        }

        var sliced = AsSpan().ByteSlice(byteOffset);
        return new Text(_data, _start + byteOffset, sliced.ByteLength, sliced.Encoding, sliced.RuneLength);
    }

    public Text ByteSlice(int byteOffset, int byteCount)
    {
        if ((uint)byteOffset > (uint)ByteLength)
        {
            throw new ArgumentOutOfRangeException(nameof(byteOffset));
        }

        if ((uint)byteCount > (uint)(ByteLength - byteOffset))
        {
            throw new ArgumentOutOfRangeException(nameof(byteCount));
        }

        var sliced = AsSpan().ByteSlice(byteOffset, byteCount);
        return new Text(_data, _start + byteOffset, byteCount, sliced.Encoding, sliced.RuneLength);
    }

    /// <summary>Splits this text by the given separator, yielding sub-spans via a zero-allocation enumerator.</summary>
    public TextSpan.SplitEnumerator Split(Text separator)
        => AsSpan().Split(separator.AsSpan());

    public TextSpan.SplitEnumerator Split(string separator)
        => AsSpan().Split(separator.AsSpan());

    public TextSpan.SplitEnumerator Split(ReadOnlySpan<byte> separator, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().Split(separator, encoding);

    public TextSpan.SplitEnumerator Split(ReadOnlySpan<char> separator)
        => AsSpan().Split(separator);

    public TextSpan.SplitEnumerator Split(ReadOnlySpan<int> separator)
        => AsSpan().Split(separator);

    /// <summary>Returns a zero-allocation enumerator over the runes in this text.</summary>
    public TextSpan.RuneEnumerator EnumerateRunes()
        => AsSpan().EnumerateRunes();
}
