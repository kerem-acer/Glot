using System.Runtime.InteropServices;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Returns <c>true</c> if this text contains the specified value, compared rune-by-rune across encodings.</summary>
    public bool Contains(Text value)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (Encoding == value.Encoding)
        {
            return RawBytes.IndexOf(value.RawBytes) >= 0;
        }

        return AsSpan().Contains(value.AsSpan());
    }

    public bool Contains(string value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());
        if (valueBytes.IsEmpty)
        {
            return true;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.IndexOf(valueBytes) >= 0;
        }

        return AsSpan().Contains(value.AsSpan());
    }

    public bool Contains(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (Encoding == encoding)
        {
            return RawBytes.IndexOf(value) >= 0;
        }

        return AsSpan().Contains(value, encoding);
    }

    public bool Contains(ReadOnlySpan<char> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return true;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.IndexOf(valueBytes) >= 0;
        }

        return AsSpan().Contains(value);
    }

    public bool Contains(ReadOnlySpan<int> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return true;
        }

        if (Encoding == TextEncoding.Utf32)
        {
            return RawBytes.IndexOf(valueBytes) >= 0;
        }

        return AsSpan().Contains(value);
    }

    /// <summary>Returns <c>true</c> if this text begins with <paramref name="value"/>, compared rune-by-rune across encodings.</summary>
    public bool StartsWith(Text value)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (Encoding == value.Encoding)
        {
            return RawBytes.StartsWith(value.RawBytes);
        }

        return AsSpan().StartsWith(value.AsSpan());
    }

    public bool StartsWith(string value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());
        if (valueBytes.IsEmpty)
        {
            return true;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.StartsWith(valueBytes);
        }

        return AsSpan().StartsWith(value.AsSpan());
    }

    public bool StartsWith(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (Encoding == encoding)
        {
            return RawBytes.StartsWith(value);
        }

        return AsSpan().StartsWith(value, encoding);
    }

    public bool StartsWith(ReadOnlySpan<char> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return true;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.StartsWith(valueBytes);
        }

        return AsSpan().StartsWith(value);
    }

    public bool StartsWith(ReadOnlySpan<int> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return true;
        }

        if (Encoding == TextEncoding.Utf32)
        {
            return RawBytes.StartsWith(valueBytes);
        }

        return AsSpan().StartsWith(value);
    }

    /// <summary>Returns <c>true</c> if this text ends with <paramref name="value"/>, compared rune-by-rune across encodings.</summary>
    public bool EndsWith(Text value)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (Encoding == value.Encoding)
        {
            return RawBytes.EndsWith(value.RawBytes);
        }

        return AsSpan().EndsWith(value.AsSpan());
    }

    public bool EndsWith(string value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());
        if (valueBytes.IsEmpty)
        {
            return true;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.EndsWith(valueBytes);
        }

        return AsSpan().EndsWith(value.AsSpan());
    }

    public bool EndsWith(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (Encoding == encoding)
        {
            return RawBytes.EndsWith(value);
        }

        return AsSpan().EndsWith(value, encoding);
    }

    public bool EndsWith(ReadOnlySpan<char> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return true;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.EndsWith(valueBytes);
        }

        return AsSpan().EndsWith(value);
    }

    public bool EndsWith(ReadOnlySpan<int> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return true;
        }

        if (Encoding == TextEncoding.Utf32)
        {
            return RawBytes.EndsWith(valueBytes);
        }

        return AsSpan().EndsWith(value);
    }

    /// <summary>Returns the zero-based rune index of the first occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int RuneIndexOf(Text value)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        if (Encoding == value.Encoding)
        {
            var bytePos = RawBytes.IndexOf(value.RawBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().RuneIndexOf(value.AsSpan());
    }

    public int RuneIndexOf(string value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());
        if (valueBytes.IsEmpty)
        {
            return 0;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            var bytePos = RawBytes.IndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().RuneIndexOf(value.AsSpan());
    }

    public int RuneIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        if (Encoding == encoding)
        {
            var bytePos = RawBytes.IndexOf(value);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().RuneIndexOf(value, encoding);
    }

    public int RuneIndexOf(ReadOnlySpan<char> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return 0;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            var bytePos = RawBytes.IndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().RuneIndexOf(value);
    }

    public int RuneIndexOf(ReadOnlySpan<int> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return 0;
        }

        if (Encoding == TextEncoding.Utf32)
        {
            var bytePos = RawBytes.IndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().RuneIndexOf(value);
    }

    /// <summary>Returns the zero-based rune index of the last occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int LastRuneIndexOf(Text value)
    {
        if (value.IsEmpty)
        {
            return RuneLength;
        }

        if (Encoding == value.Encoding)
        {
            var bytePos = RawBytes.LastIndexOf(value.RawBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().LastRuneIndexOf(value.AsSpan());
    }

    public int LastRuneIndexOf(string value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());
        if (valueBytes.IsEmpty)
        {
            return RuneLength;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            var bytePos = RawBytes.LastIndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().LastRuneIndexOf(value.AsSpan());
    }

    public int LastRuneIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return RuneLength;
        }

        if (Encoding == encoding)
        {
            var bytePos = RawBytes.LastIndexOf(value);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().LastRuneIndexOf(value, encoding);
    }

    public int LastRuneIndexOf(ReadOnlySpan<char> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return RuneLength;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            var bytePos = RawBytes.LastIndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().LastRuneIndexOf(value);
    }

    public int LastRuneIndexOf(ReadOnlySpan<int> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return RuneLength;
        }

        if (Encoding == TextEncoding.Utf32)
        {
            var bytePos = RawBytes.LastIndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.Count(RawBytes[..bytePos], Encoding);
        }

        return AsSpan().LastRuneIndexOf(value);
    }

    /// <summary>Returns the zero-based byte offset of the first occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int ByteIndexOf(Text value)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        if (Encoding == value.Encoding)
        {
            return RawBytes.IndexOf(value.RawBytes);
        }

        return AsSpan().ByteIndexOf(value.AsSpan());
    }

    public int ByteIndexOf(string value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());
        if (valueBytes.IsEmpty)
        {
            return 0;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.IndexOf(valueBytes);
        }

        return AsSpan().ByteIndexOf(value.AsSpan());
    }

    public int ByteIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        if (Encoding == encoding)
        {
            return RawBytes.IndexOf(value);
        }

        return AsSpan().ByteIndexOf(value, encoding);
    }

    public int ByteIndexOf(ReadOnlySpan<char> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return 0;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.IndexOf(valueBytes);
        }

        return AsSpan().ByteIndexOf(value);
    }

    public int ByteIndexOf(ReadOnlySpan<int> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return 0;
        }

        if (Encoding == TextEncoding.Utf32)
        {
            return RawBytes.IndexOf(valueBytes);
        }

        return AsSpan().ByteIndexOf(value);
    }

    /// <summary>Returns the zero-based byte offset of the last occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    public int LastByteIndexOf(Text value)
    {
        if (value.IsEmpty)
        {
            return ByteLength;
        }

        if (Encoding == value.Encoding)
        {
            return RawBytes.LastIndexOf(value.RawBytes);
        }

        return AsSpan().LastByteIndexOf(value.AsSpan());
    }

    public int LastByteIndexOf(string value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());
        if (valueBytes.IsEmpty)
        {
            return ByteLength;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.LastIndexOf(valueBytes);
        }

        return AsSpan().LastByteIndexOf(value.AsSpan());
    }

    public int LastByteIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return ByteLength;
        }

        if (Encoding == encoding)
        {
            return RawBytes.LastIndexOf(value);
        }

        return AsSpan().LastByteIndexOf(value, encoding);
    }

    public int LastByteIndexOf(ReadOnlySpan<char> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return ByteLength;
        }

        if (Encoding == TextEncoding.Utf16)
        {
            return RawBytes.LastIndexOf(valueBytes);
        }

        return AsSpan().LastByteIndexOf(value);
    }

    public int LastByteIndexOf(ReadOnlySpan<int> value)
    {
        var valueBytes = MemoryMarshal.AsBytes(value);
        if (valueBytes.IsEmpty)
        {
            return ByteLength;
        }

        if (Encoding == TextEncoding.Utf32)
        {
            return RawBytes.LastIndexOf(valueBytes);
        }

        return AsSpan().LastByteIndexOf(value);
    }

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
