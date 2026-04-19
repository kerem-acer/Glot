using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Returns <c>true</c> if this text contains the specified value, compared rune-by-rune across encodings.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Text value)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (_encoding == value._encoding)
        {
            return Bytes.IndexOf(value.UnsafeBytes) >= 0;
        }

        return ContainsCrossEncoding(value);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    bool ContainsCrossEncoding(Text value) => AsSpan().Contains(value.AsSpan());

    public bool Contains(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        var valueBytes = MemoryMarshal.AsBytes(value.AsUnsafeSpan());

        if (Encoding == TextEncoding.Utf16)
        {
            return Bytes.IndexOf(valueBytes) >= 0;
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
            return Bytes.IndexOf(value) >= 0;
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
            return Bytes.IndexOf(valueBytes) >= 0;
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
            return Bytes.IndexOf(valueBytes) >= 0;
        }

        return AsSpan().Contains(value);
    }

    /// <summary>Returns <c>true</c> if this text begins with <paramref name="value"/>, compared rune-by-rune across encodings.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool StartsWith(Text value)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (_encoding == value._encoding)
        {
            return Bytes.StartsWith(value.UnsafeBytes);
        }

        return StartsWithCrossEncoding(value);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    bool StartsWithCrossEncoding(Text value) => AsSpan().StartsWith(value.AsSpan());

    public bool StartsWith(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());

        if (Encoding == TextEncoding.Utf16)
        {
            return Bytes.StartsWith(valueBytes);
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
            return Bytes.StartsWith(value);
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
            return Bytes.StartsWith(valueBytes);
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
            return Bytes.StartsWith(valueBytes);
        }

        return AsSpan().StartsWith(value);
    }

    /// <summary>Returns <c>true</c> if this text ends with <paramref name="value"/>, compared rune-by-rune across encodings.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool EndsWith(Text value)
    {
        if (value.IsEmpty)
        {
            return true;
        }

        if (_encoding == value._encoding)
        {
            return Bytes.EndsWith(value.UnsafeBytes);
        }

        return EndsWithCrossEncoding(value);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    bool EndsWithCrossEncoding(Text value) => AsSpan().EndsWith(value.AsSpan());

    public bool EndsWith(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        var valueBytes = MemoryMarshal.AsBytes(value.AsSpan());

        if (Encoding == TextEncoding.Utf16)
        {
            return Bytes.EndsWith(valueBytes);
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
            return Bytes.EndsWith(value);
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
            return Bytes.EndsWith(valueBytes);
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
            return Bytes.EndsWith(valueBytes);
        }

        return AsSpan().EndsWith(value);
    }

    /// <summary>Returns the zero-based rune index of the first occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int RuneIndexOf(Text value)
    {
        if (_encoding == value._encoding)
        {
            if (value.IsEmpty)
            {
                return 0;
            }

            var haystack = Bytes;
            var bytePos = haystack.IndexOf(value.UnsafeBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(
                haystack,
                Encoding,
                bytePos,
                _runeLength);
        }

        if (value.IsEmpty)
        {
            return 0;
        }

        return RuneIndexOfCrossEncoding(value);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    int RuneIndexOfCrossEncoding(Text value) => AsSpan().RuneIndexOf(value.AsSpan());

    public int RuneIndexOf(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return 0;
        }

        var valueBytes = MemoryMarshal.AsBytes(value.AsUnsafeSpan());

        if (Encoding == TextEncoding.Utf16)
        {
            var haystack = Bytes;
            var bytePos = haystack.IndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(haystack, Encoding, bytePos, _runeLength);
        }

        return AsSpan().RuneIndexOf(value.AsUnsafeSpan());
    }

    public int RuneIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        if (Encoding == encoding)
        {
            var haystack = Bytes;
            var bytePos = haystack.IndexOf(value);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(haystack, Encoding, bytePos, _runeLength);
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
            var haystack = Bytes;
            var bytePos = haystack.IndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(haystack, Encoding, bytePos, _runeLength);
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
            var haystack = Bytes;
            var bytePos = haystack.IndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(haystack, Encoding, bytePos, _runeLength);
        }

        return AsSpan().RuneIndexOf(value);
    }

    /// <summary>Returns the zero-based rune index of the last occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int LastRuneIndexOf(Text value)
    {
        if (_encoding == value._encoding)
        {
            if (value.IsEmpty)
            {
                return RuneLength;
            }

            var haystack = Bytes;
            var bytePos = haystack.LastIndexOf(value.UnsafeBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(
                haystack,
                Encoding,
                bytePos,
                _runeLength);
        }

        if (value.IsEmpty)
        {
            return RuneLength;
        }

        return LastRuneIndexOfCrossEncoding(value);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    int LastRuneIndexOfCrossEncoding(Text value) => AsSpan().LastRuneIndexOf(value.AsSpan());

    public int LastRuneIndexOf(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return RuneLength;
        }

        var valueBytes = MemoryMarshal.AsBytes(value.AsUnsafeSpan());

        if (Encoding == TextEncoding.Utf16)
        {
            var haystack = Bytes;
            var bytePos = haystack.LastIndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(haystack, Encoding, bytePos, _runeLength);
        }

        return AsSpan().LastRuneIndexOf(value.AsUnsafeSpan());
    }

    public int LastRuneIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return RuneLength;
        }

        if (Encoding == encoding)
        {
            var haystack = Bytes;
            var bytePos = haystack.LastIndexOf(value);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(haystack, Encoding, bytePos, _runeLength);
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
            var haystack = Bytes;
            var bytePos = haystack.LastIndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(haystack, Encoding, bytePos, _runeLength);
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
            var haystack = Bytes;
            var bytePos = haystack.LastIndexOf(valueBytes);
            if (bytePos < 0)
            {
                return -1;
            }

            return RuneCount.CountPrefix(haystack, Encoding, bytePos, _runeLength);
        }

        return AsSpan().LastRuneIndexOf(value);
    }

    /// <summary>Returns the zero-based byte offset of the first occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int ByteIndexOf(Text value)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        if (_encoding == value._encoding)
        {
            return Bytes.IndexOf(value.UnsafeBytes);
        }

        return ByteIndexOfCrossEncoding(value);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    int ByteIndexOfCrossEncoding(Text value) => AsSpan().ByteIndexOf(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int ByteIndexOf(TextSpan value)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        if (_encoding == value.Encoding)
        {
            return Bytes.IndexOf(value.Bytes);
        }

        return AsSpan().ByteIndexOf(value);
    }

    public int ByteIndexOf(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return 0;
        }

        var valueBytes = MemoryMarshal.AsBytes(value.AsUnsafeSpan());

        if (Encoding == TextEncoding.Utf16)
        {
            return Bytes.IndexOf(valueBytes);
        }

        return AsSpan().ByteIndexOf(value.AsUnsafeSpan());
    }

    public int ByteIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        if (Encoding == encoding)
        {
            return Bytes.IndexOf(value);
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
            return Bytes.IndexOf(valueBytes);
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
            return Bytes.IndexOf(valueBytes);
        }

        return AsSpan().ByteIndexOf(value);
    }

    /// <summary>Returns the zero-based byte offset of the last occurrence of <paramref name="value"/>, or -1 if not found.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int LastByteIndexOf(Text value)
    {
        if (value.IsEmpty)
        {
            return ByteLength;
        }

        if (_encoding == value._encoding)
        {
            return Bytes.LastIndexOf(value.UnsafeBytes);
        }

        return LastByteIndexOfCrossEncoding(value);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    int LastByteIndexOfCrossEncoding(Text value) => AsSpan().LastByteIndexOf(value.AsSpan());

    public int LastByteIndexOf(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return ByteLength;
        }

        var valueBytes = MemoryMarshal.AsBytes(value.AsUnsafeSpan());

        if (Encoding == TextEncoding.Utf16)
        {
            return Bytes.LastIndexOf(valueBytes);
        }

        return AsSpan().LastByteIndexOf(value.AsUnsafeSpan());
    }

    public int LastByteIndexOf(ReadOnlySpan<byte> value, TextEncoding encoding = TextEncoding.Utf8)
    {
        if (value.IsEmpty)
        {
            return ByteLength;
        }

        if (Encoding == encoding)
        {
            return Bytes.LastIndexOf(value);
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
            return Bytes.LastIndexOf(valueBytes);
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
            return Bytes.LastIndexOf(valueBytes);
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
        return new Text(
            _data,
            _start + bytesRemoved,
            trimmed.ByteLength,
            trimmed.Encoding,
            trimmed.RuneLength,
            _backingType);
    }

    public Text TrimEnd()
    {
        var trimmed = AsSpan().TrimEnd();
        return new Text(
            _data,
            _start,
            trimmed.ByteLength,
            trimmed.Encoding,
            trimmed.RuneLength,
            _backingType);
    }

    /// <summary>Returns a non-copying sub-view starting at <paramref name="runeOffset"/>.</summary>
    public Text RuneSlice(int runeOffset)
    {
        if ((uint)runeOffset > (uint)RuneLength)
        {
            throw new ArgumentOutOfRangeException(nameof(runeOffset));
        }

        var bytes = Bytes;
        var byteOffset = RuneIndex.ToByteOffset(bytes, Encoding, runeOffset);
        return new Text(
            _data,
            _start + byteOffset,
            ByteLength - byteOffset,
            Encoding,
            RuneLength - runeOffset,
            _backingType);
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

        var bytes = Bytes;
        var byteOffset = RuneIndex.ToByteOffset(bytes, Encoding, runeOffset);
        var byteCount = RuneIndex.ToByteOffset(bytes[byteOffset..], Encoding, runeCount);
        return new Text(
            _data,
            _start + byteOffset,
            byteCount,
            Encoding,
            runeCount,
            _backingType);
    }

    public Text ByteSlice(int byteOffset)
    {
        if ((uint)byteOffset > (uint)ByteLength)
        {
            throw new ArgumentOutOfRangeException(nameof(byteOffset));
        }

        return new Text(
            _data,
            _start + byteOffset,
            ByteLength - byteOffset,
            Encoding,
            0,
            _backingType);
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

        return new Text(
            _data,
            _start + byteOffset,
            byteCount,
            Encoding,
            0,
            _backingType);
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
