using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Returns <c>true</c> if this text contains the specified value, compared rune-by-rune across encodings.</summary>
    /// <param name="value">The text to search for.</param>
    /// <returns><c>true</c> if this text contains <paramref name="value"/>; otherwise <c>false</c>.</returns>
    /// <remarks>When both texts share the same encoding, uses a direct byte-sequence search. Cross-encoding searches transcode on the fly without allocating.</remarks>
    /// <example>
    /// <code>
    /// var text = Text.FromUtf8("hello world"u8);
    /// bool found = text.Contains(Text.From("world")); // true — cross-encoding
    /// </code>
    /// </example>
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

    /// <inheritdoc cref="Contains(Text)"/>
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

    /// <inheritdoc cref="Contains(Text)"/>
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

    /// <inheritdoc cref="Contains(Text)"/>
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

    /// <inheritdoc cref="Contains(Text)"/>
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
    /// <param name="value">The text to compare against the start of this text.</param>
    /// <returns><c>true</c> if this text begins with <paramref name="value"/>; otherwise <c>false</c>.</returns>
    /// <remarks>Same-encoding comparison uses direct byte prefix check. Cross-encoding transcodes on the fly.</remarks>
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

    /// <inheritdoc cref="StartsWith(Text)"/>
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

    /// <inheritdoc cref="StartsWith(Text)"/>
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

    /// <inheritdoc cref="StartsWith(Text)"/>
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

    /// <inheritdoc cref="StartsWith(Text)"/>
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
    /// <param name="value">The text to compare against the end of this text.</param>
    /// <returns><c>true</c> if this text ends with <paramref name="value"/>; otherwise <c>false</c>.</returns>
    /// <remarks>Same-encoding comparison uses direct byte suffix check. Cross-encoding transcodes on the fly.</remarks>
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

    /// <inheritdoc cref="EndsWith(Text)"/>
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

    /// <inheritdoc cref="EndsWith(Text)"/>
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

    /// <inheritdoc cref="EndsWith(Text)"/>
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

    /// <inheritdoc cref="EndsWith(Text)"/>
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
    /// <param name="value">The text to locate.</param>
    /// <returns>The zero-based rune index of the first occurrence of <paramref name="value"/>, or -1 if not found.</returns>
    /// <remarks>The returned index is in runes (Unicode scalar values), not bytes or chars. Same-encoding uses direct byte search then counts the prefix runes. Cross-encoding transcodes on the fly.</remarks>
    /// <example>
    /// <code>
    /// var text = Text.FromUtf8("caf\u00e9"u8);
    /// int index = text.RuneIndexOf(Text.FromUtf8("\u00e9"u8)); // 3
    /// </code>
    /// </example>
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

    /// <inheritdoc cref="RuneIndexOf(Text)"/>
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

    /// <inheritdoc cref="RuneIndexOf(Text)"/>
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

    /// <inheritdoc cref="RuneIndexOf(Text)"/>
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

    /// <inheritdoc cref="RuneIndexOf(Text)"/>
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
    /// <param name="value">The text to locate.</param>
    /// <returns>The zero-based rune index of the last occurrence of <paramref name="value"/>, or -1 if not found.</returns>
    /// <remarks>Returns <see cref="RuneLength"/> for an empty <paramref name="value"/>.</remarks>
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

    /// <inheritdoc cref="LastRuneIndexOf(Text)"/>
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

    /// <inheritdoc cref="LastRuneIndexOf(Text)"/>
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

    /// <inheritdoc cref="LastRuneIndexOf(Text)"/>
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

    /// <inheritdoc cref="LastRuneIndexOf(Text)"/>
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
    /// <param name="value">The text to locate.</param>
    /// <returns>The zero-based byte offset of the first occurrence of <paramref name="value"/>, or -1 if not found.</returns>
    /// <remarks>The returned index is in bytes of the text's encoding, not runes or chars. Same-encoding uses direct byte search. Cross-encoding transcodes on the fly.</remarks>
    /// <example>
    /// <code>
    /// var text = Text.FromUtf8("caf\u00e9"u8);
    /// int offset = text.ByteIndexOf(Text.FromUtf8("\u00e9"u8)); // 3 in UTF-8 (c=1, a=1, f=1)
    /// </code>
    /// </example>
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

    /// <inheritdoc cref="ByteIndexOf(Text)"/>
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

    /// <inheritdoc cref="ByteIndexOf(Text)"/>
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

    /// <inheritdoc cref="ByteIndexOf(Text)"/>
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

    /// <inheritdoc cref="ByteIndexOf(Text)"/>
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
    /// <param name="value">The text to locate.</param>
    /// <returns>The zero-based byte offset of the last occurrence of <paramref name="value"/>, or -1 if not found.</returns>
    /// <remarks>Returns <see cref="ByteLength"/> for an empty <paramref name="value"/>.</remarks>
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

    /// <inheritdoc cref="LastByteIndexOf(Text)"/>
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

    /// <inheritdoc cref="LastByteIndexOf(Text)"/>
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

    /// <inheritdoc cref="LastByteIndexOf(Text)"/>
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

    /// <inheritdoc cref="LastByteIndexOf(Text)"/>
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
    /// <returns>A <see cref="Text"/> with leading and trailing whitespace removed.</returns>
    /// <remarks>Does not allocate — returns a sub-view over the same backing data with adjusted offset and length.</remarks>
    /// <example>
    /// <code>
    /// var text = Text.From("  hello  ");
    /// Text trimmed = text.Trim(); // "hello"
    /// </code>
    /// </example>
    public Text Trim() => TrimStart().TrimEnd();

    /// <summary>Removes leading whitespace. Returns a non-copying sub-view of the same backing data.</summary>
    /// <returns>A <see cref="Text"/> with leading whitespace removed.</returns>
    /// <remarks>Does not allocate — returns a sub-view over the same backing data.</remarks>
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

    /// <summary>Removes trailing whitespace. Returns a non-copying sub-view of the same backing data.</summary>
    /// <returns>A <see cref="Text"/> with trailing whitespace removed.</returns>
    /// <remarks>Does not allocate — returns a sub-view over the same backing data.</remarks>
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
    /// <param name="runeOffset">The zero-based rune index at which the slice begins.</param>
    /// <returns>A <see cref="Text"/> representing the sub-view from <paramref name="runeOffset"/> to the end.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="runeOffset"/> is negative or greater than <see cref="RuneLength"/>.</exception>
    /// <remarks>Does not allocate — returns a sub-view over the same backing data. The rune count of the result is computed from the original.</remarks>
    /// <example>
    /// <code>
    /// var text = Text.From("hello");
    /// Text slice = text.RuneSlice(2); // "llo"
    /// </code>
    /// </example>
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

    /// <summary>Returns a non-copying sub-view of <paramref name="runeCount"/> runes starting at <paramref name="runeOffset"/>.</summary>
    /// <param name="runeOffset">The zero-based rune index at which the slice begins.</param>
    /// <param name="runeCount">The number of runes to include in the slice.</param>
    /// <returns>A <see cref="Text"/> representing the specified sub-view.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="runeOffset"/> or <paramref name="runeCount"/> is out of range.</exception>
    /// <remarks>Does not allocate — returns a sub-view over the same backing data.</remarks>
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

    /// <summary>Returns a non-copying sub-view starting at <paramref name="byteOffset"/>. Rune count is deferred.</summary>
    /// <param name="byteOffset">The zero-based byte offset at which the slice begins.</param>
    /// <returns>A <see cref="Text"/> representing the sub-view from <paramref name="byteOffset"/> to the end.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="byteOffset"/> is negative or greater than <see cref="ByteLength"/>.</exception>
    /// <remarks>Does not allocate — returns a sub-view over the same backing data. The rune count is deferred (computed on first access).</remarks>
    /// <example>
    /// <code>
    /// var text = Text.FromUtf8("hello"u8);
    /// Text slice = text.ByteSlice(2); // "llo" (bytes 2..4)
    /// </code>
    /// </example>
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

    /// <summary>Returns a non-copying sub-view of <paramref name="byteCount"/> bytes starting at <paramref name="byteOffset"/>. Rune count is deferred.</summary>
    /// <param name="byteOffset">The zero-based byte offset at which the slice begins.</param>
    /// <param name="byteCount">The number of bytes to include in the slice.</param>
    /// <returns>A <see cref="Text"/> representing the specified sub-view.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="byteOffset"/> or <paramref name="byteCount"/> is out of range.</exception>
    /// <remarks>Does not allocate — returns a sub-view over the same backing data. The rune count is deferred.</remarks>
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

    /// <summary>Splits this text by the given separator, yielding sub-spans via an enumerator.</summary>
    /// <param name="separator">The text to use as a delimiter.</param>
    /// <returns>A <see cref="TextSpan.SplitEnumerator"/> that iterates over the segments.</returns>
    /// <remarks>Does not allocate. The enumerator yields <see cref="TextSpan"/> sub-views over the original data.</remarks>
    /// <example>
    /// <code>
    /// var csv = Text.FromUtf8("a,b,c"u8);
    /// foreach (var part in csv.Split(Text.FromUtf8(","u8)))
    /// {
    ///     // part is a TextSpan: "a", "b", "c"
    /// }
    /// </code>
    /// </example>
    public TextSpan.SplitEnumerator Split(Text separator)
        => AsSpan().Split(separator.AsSpan());

    /// <inheritdoc cref="Split(Text)"/>
    public TextSpan.SplitEnumerator Split(string separator)
        => AsSpan().Split(separator.AsSpan());

    /// <inheritdoc cref="Split(Text)"/>
    public TextSpan.SplitEnumerator Split(ReadOnlySpan<byte> separator, TextEncoding encoding = TextEncoding.Utf8)
        => AsSpan().Split(separator, encoding);

    /// <inheritdoc cref="Split(Text)"/>
    public TextSpan.SplitEnumerator Split(ReadOnlySpan<char> separator)
        => AsSpan().Split(separator);

    /// <inheritdoc cref="Split(Text)"/>
    public TextSpan.SplitEnumerator Split(ReadOnlySpan<int> separator)
        => AsSpan().Split(separator);

    /// <summary>Returns an enumerator over the runes in this text.</summary>
    /// <returns>A <see cref="TextSpan.RuneEnumerator"/> that iterates over each rune.</returns>
    /// <remarks>Does not allocate. The enumerator decodes runes from the underlying bytes on each iteration.</remarks>
    /// <example>
    /// <code>
    /// var text = Text.From("caf\u00e9");
    /// foreach (var rune in text.EnumerateRunes())
    /// {
    ///     // rune: 'c', 'a', 'f', '\u00e9'
    /// }
    /// </code>
    /// </example>
    public TextSpan.RuneEnumerator EnumerateRunes()
        => AsSpan().EnumerateRunes();
}
