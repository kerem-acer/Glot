#if NET8_0_OR_GREATER
using System.Buffers;
#endif
using System.Globalization;
#if !NETSTANDARD2_0
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#endif
using System.Text;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Returns a new <see cref="Text"/> with all runes converted to uppercase (invariant). Returns <c>this</c> if already uppercase.</summary>
    /// <returns>A new <see cref="Text"/> with all runes converted to uppercase, or the original if no changes were needed.</returns>
    /// <remarks>Returns <c>this</c> when no runes need changing (no allocation). Otherwise allocates a new backing array.</remarks>
    /// <example>
    /// <code>
    /// var text = Text.From("hello");
    /// Text upper = text.ToUpperInvariant(); // "HELLO"
    /// </code>
    /// </example>
    public Text ToUpperInvariant()
    {
        if (IsEmpty)
        {
            return this;
        }

#if NET8_0_OR_GREATER
        if (TryUpperAscii(out var ascii))
        {
            return ascii;
        }
#endif

        return ToUpperCore(CultureInfo.InvariantCulture);
    }

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to lowercase (invariant). Returns <c>this</c> if already lowercase.</summary>
    /// <returns>A new <see cref="Text"/> with all runes converted to lowercase, or the original if no changes were needed.</returns>
    /// <remarks>Returns <c>this</c> when no runes need changing (no allocation). Otherwise allocates a new backing array.</remarks>
    public Text ToLowerInvariant()
    {
        if (IsEmpty)
        {
            return this;
        }

#if NET8_0_OR_GREATER
        if (TryLowerAscii(out var ascii))
        {
            return ascii;
        }
#endif

        return ToLowerCore(CultureInfo.InvariantCulture);
    }

    /// <summary>Like <see cref="ToUpperInvariant()"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    /// <returns>An <see cref="OwnedText"/> with all runes converted to uppercase.</returns>
    /// <remarks>The caller must dispose the returned <see cref="OwnedText"/>. Uses pooled buffers from <see cref="System.Buffers.ArrayPool{T}"/>.</remarks>
    public OwnedText ToUpperInvariantPooled()
    {
        if (IsEmpty)
        {
            return OwnedText.Empty;
        }

#if NET8_0_OR_GREATER
        if (TryUpperAsciiPooled(out var ascii))
        {
            return ascii;
        }
#endif

        return ToUpperPooledCore(CultureInfo.InvariantCulture);
    }

    /// <summary>Like <see cref="ToLowerInvariant()"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    /// <returns>An <see cref="OwnedText"/> with all runes converted to lowercase.</returns>
    /// <remarks>The caller must dispose the returned <see cref="OwnedText"/>. Uses pooled buffers from <see cref="System.Buffers.ArrayPool{T}"/>.</remarks>
    public OwnedText ToLowerInvariantPooled()
    {
        if (IsEmpty)
        {
            return OwnedText.Empty;
        }

#if NET8_0_OR_GREATER
        if (TryLowerAsciiPooled(out var ascii))
        {
            return ascii;
        }
#endif

        return ToLowerPooledCore(CultureInfo.InvariantCulture);
    }

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to uppercase using the current culture.</summary>
    /// <returns>A new <see cref="Text"/> with all runes converted to uppercase, or the original if no changes were needed.</returns>
    public Text ToUpper() => ToUpper(CultureInfo.CurrentCulture);

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to uppercase using the specified culture.</summary>
    /// <param name="culture">The culture whose casing rules are used.</param>
    /// <returns>A new <see cref="Text"/> with all runes converted to uppercase, or the original if no changes were needed.</returns>
    /// <remarks>Returns <c>this</c> when no runes need changing. Otherwise allocates a new backing array with the culture-specific casing applied.</remarks>
    public Text ToUpper(CultureInfo culture)
    {
        if (IsEmpty)
        {
            return this;
        }

        return ToUpperCore(culture);
    }

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to lowercase using the current culture.</summary>
    /// <returns>A new <see cref="Text"/> with all runes converted to lowercase, or the original if no changes were needed.</returns>
    public Text ToLower() => ToLower(CultureInfo.CurrentCulture);

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to lowercase using the specified culture.</summary>
    /// <param name="culture">The culture whose casing rules are used.</param>
    /// <returns>A new <see cref="Text"/> with all runes converted to lowercase, or the original if no changes were needed.</returns>
    /// <remarks>Returns <c>this</c> when no runes need changing. Otherwise allocates a new backing array with the culture-specific casing applied.</remarks>
    public Text ToLower(CultureInfo culture)
    {
        if (IsEmpty)
        {
            return this;
        }

        return ToLowerCore(culture);
    }

    /// <summary>Like <see cref="ToUpper(CultureInfo)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    /// <param name="culture">The culture whose casing rules are used.</param>
    /// <returns>An <see cref="OwnedText"/> with all runes converted to uppercase.</returns>
    /// <remarks>The caller must dispose the returned <see cref="OwnedText"/>. Uses pooled buffers.</remarks>
    public OwnedText ToUpperPooled(CultureInfo culture)
    {
        if (IsEmpty)
        {
            return OwnedText.Empty;
        }

        return ToUpperPooledCore(culture);
    }

    /// <summary>Like <see cref="ToLower(CultureInfo)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    /// <param name="culture">The culture whose casing rules are used.</param>
    /// <returns>An <see cref="OwnedText"/> with all runes converted to lowercase.</returns>
    /// <remarks>The caller must dispose the returned <see cref="OwnedText"/>. Uses pooled buffers.</remarks>
    public OwnedText ToLowerPooled(CultureInfo culture)
    {
        if (IsEmpty)
        {
            return OwnedText.Empty;
        }

        return ToLowerPooledCore(culture);
    }

    Text ToUpperCore(CultureInfo culture)
    {
        var bytes = UnsafeBytes;
        var firstChangeOffset = FindFirstCaseChange(
            upper: true,
            culture,
            bytes,
            out var prefixRuneCount);

        if (firstChangeOffset < 0)
        {
            return this;
        }

#if !NETSTANDARD2_0
        if (Encoding == TextEncoding.Utf16)
        {
            return CaseVectorizedUtf16(upper: true, culture, bytes);
        }
#endif

        return CaseCore(
            upper: true,
            culture,
            bytes,
            firstChangeOffset,
            prefixRuneCount,
            FinishAsText);
    }

    Text ToLowerCore(CultureInfo culture)
    {
        var bytes = UnsafeBytes;
        var firstChangeOffset = FindFirstCaseChange(
            upper: false,
            culture,
            bytes,
            out var prefixRuneCount);

        if (firstChangeOffset < 0)
        {
            return this;
        }

#if !NETSTANDARD2_0
        if (Encoding == TextEncoding.Utf16)
        {
            return CaseVectorizedUtf16(upper: false, culture, bytes);
        }
#endif

        return CaseCore(
            upper: false,
            culture,
            bytes,
            firstChangeOffset,
            prefixRuneCount,
            FinishAsText);
    }

    OwnedText ToUpperPooledCore(CultureInfo culture)
    {
        var bytes = UnsafeBytes;
        var firstChangeOffset = FindFirstCaseChange(
            upper: true,
            culture,
            bytes,
            out var prefixRuneCount);

        return firstChangeOffset < 0 ?
            OwnedText.FromBytes(bytes, Encoding) :
            CaseCore(
                upper: true,
                culture,
                bytes,
                firstChangeOffset,
                prefixRuneCount,
                FinishAsOwnedText);
    }

    OwnedText ToLowerPooledCore(CultureInfo culture)
    {
        var bytes = UnsafeBytes;
        var firstChangeOffset = FindFirstCaseChange(
            upper: false,
            culture,
            bytes,
            out var prefixRuneCount);

        return firstChangeOffset < 0 ?
            OwnedText.FromBytes(bytes, Encoding) :
            CaseCore(
                upper: false,
                culture,
                bytes,
                firstChangeOffset,
                prefixRuneCount,
                FinishAsOwnedText);
    }

#if !NETSTANDARD2_0
    // UTF-16 backing only. Delegates the full case conversion to the BCL's vectorized
    // MemoryExtensions.ToUpper / ToLower, which uses ICU tables under the hood.
    // Caller must have already confirmed at least one rune changes case (firstChangeOffset >= 0).
    Text CaseVectorizedUtf16(bool upper, CultureInfo culture, ReadOnlySpan<byte> sourceBytes)
    {
        var srcChars = MemoryMarshal.Cast<byte, char>(sourceBytes);
        var dstArray = new char[srcChars.Length];
        Span<char> dst = dstArray;

        var written = upper
            ? MemoryExtensions.ToUpper(srcChars, dst, culture)
            : MemoryExtensions.ToLower(srcChars, dst, culture);

        // BCL returns -1 only when destination is too small; sizes match, so this is
        // the number of chars written and must equal srcChars.Length.
        _ = written;

        return new Text(
            dstArray,
            0,
            dstArray.Length * 2,
            TextEncoding.Utf16,
            _runeLength,
            BackingType.CharArray);
    }
#endif

    // Shared helpers

    int FindFirstCaseChange(bool upper,
        CultureInfo culture,
        ReadOnlySpan<byte> all,
        out int prefixRuneCount)
    {
        var encoding = Encoding;
        var length = all.Length;
        var offset = 0;
        prefixRuneCount = 0;

#if NETSTANDARD2_0
        while (offset < length)
        {
            Rune.TryDecodeFirst(all.Slice(offset), encoding, out var rune, out var consumed);
#else
        ref byte origin = ref MemoryMarshal.GetReference(all);
        while (offset < length)
        {
            Rune.TryDecodeFirst(
                MemoryMarshal.CreateReadOnlySpan(ref Unsafe.Add(ref origin, offset), length - offset),
                encoding,
                out var rune,
                out var consumed);
#endif

            if (rune.Value <= 0xFFFF)
            {
                var c = (char)rune.Value;
                var converted = upper ? char.ToUpper(c, culture) : char.ToLower(c, culture);
                if (converted != c && Rune.TryCreate(converted, out _))
                {
                    return offset;
                }
            }

            offset += consumed;
            prefixRuneCount++;
        }

        return -1;
    }

    TResult CaseCore<TResult>(bool upper,
        CultureInfo culture,
        ReadOnlySpan<byte> all,
        int firstChangeOffset,
        int prefixRuneCount,
        BuilderFinisher<TResult> finish)
    {
        var builder = new TextBuilder(Encoding);
        try
        {
            var encoding = Encoding;
            var length = all.Length;

            if (firstChangeOffset > 0)
            {
                builder.AppendCounted(all[..firstChangeOffset], prefixRuneCount);
            }

            var offset = firstChangeOffset;
            var runStart = offset;
            var runRuneCount = 0;

#if !NETSTANDARD2_0
            ref byte origin = ref MemoryMarshal.GetReference(all);
#endif

            while (offset < length)
            {
#if NETSTANDARD2_0
                Rune.TryDecodeFirst(all.Slice(offset), encoding, out var rune, out var consumed);
#else
                Rune.TryDecodeFirst(
                    MemoryMarshal.CreateReadOnlySpan(
                        ref Unsafe.Add(ref origin, offset),
                        length - offset),
                    encoding,
                    out var rune,
                    out var consumed);
#endif

                Rune converted = rune;
                if (rune.Value <= 0xFFFF)
                {
                    var c = (char)rune.Value;
                    var cc = upper ? char.ToUpper(c, culture) : char.ToLower(c, culture);
                    if (cc != c && Rune.TryCreate(cc, out var r))
                    {
                        converted = r;
                    }
                }

                if (converted != rune)
                {
                    if (offset > runStart)
                    {
#if NETSTANDARD2_0
                        builder.AppendCounted(all.Slice(runStart, offset - runStart), runRuneCount);
#else
                        builder.AppendCounted(
                            MemoryMarshal.CreateReadOnlySpan(
                                ref Unsafe.Add(ref origin, runStart),
                                offset - runStart),
                            runRuneCount);
#endif
                    }

                    builder.AppendRune(converted);
                    offset += consumed;
                    runStart = offset;
                    runRuneCount = 0;
                }
                else
                {
                    offset += consumed;
                    runRuneCount++;
                }
            }

            if (offset > runStart)
            {
#if NETSTANDARD2_0
                builder.AppendCounted(all.Slice(runStart, offset - runStart), runRuneCount);
#else
                builder.AppendCounted(
                    MemoryMarshal.CreateReadOnlySpan(
                        ref Unsafe.Add(ref origin, runStart),
                        offset - runStart),
                    runRuneCount);
#endif
            }

            return finish(ref builder);
        }
        finally
        {
            builder.Dispose();
        }
    }

#if NET8_0_OR_GREATER

    bool TryUpperAscii(out Text result)
    {
        var bytes = UnsafeBytes;

        switch (Encoding)
        {
            case TextEncoding.Utf8:
                {
                    if (!Ascii.IsValid(bytes))
                    {
                        break;
                    }

                    var dst = new byte[bytes.Length];
                    Ascii.ToUpper(bytes, dst, out _);

                    if (bytes.SequenceEqual(dst))
                    {
                        result = this;
                        return true;
                    }

                    result = new Text(
                        dst,
                        0,
                        dst.Length,
                        TextEncoding.Utf8,
                        dst.Length,
                        BackingType.ByteArray);

                    return true;
                }

            case TextEncoding.Utf16:
                {
                    var chars = MemoryMarshal.Cast<byte, char>(bytes);
                    if (!Ascii.IsValid(chars))
                    {
                        break;
                    }

                    var dst = new char[chars.Length];
                    Ascii.ToUpper(chars, dst, out _);

                    if (chars.SequenceEqual(dst))
                    {
                        result = this;
                        return true;
                    }

                    result = new Text(
                        dst,
                        0,
                        dst.Length * 2,
                        TextEncoding.Utf16,
                        dst.Length,
                        BackingType.CharArray);

                    return true;
                }

            case TextEncoding.Utf32:
                {
                    var ints = MemoryMarshal.Cast<byte, int>(bytes);
                    if (ints.ContainsAnyExceptInRange(0, 127))
                    {
                        break;
                    }

                    if (!ints.ContainsAnyInRange('a', 'z'))
                    {
                        result = this;
                        return true;
                    }

                    var dst = new int[ints.Length];
                    ToggleCaseAsciiUtf32(
                        ints,
                        dst,
                        -32,
                        'a',
                        'z');

                    result = new Text(
                        dst,
                        0,
                        dst.Length * 4,
                        TextEncoding.Utf32,
                        dst.Length,
                        BackingType.IntArray);

                    return true;
                }
        }

        result = default;
        return false;
    }

    bool TryLowerAscii(out Text result)
    {
        var bytes = UnsafeBytes;

        switch (Encoding)
        {
            case TextEncoding.Utf8:
                {
                    if (!Ascii.IsValid(bytes))
                    {
                        break;
                    }

                    var dst = new byte[bytes.Length];
                    Ascii.ToLower(bytes, dst, out _);

                    if (bytes.SequenceEqual(dst))
                    {
                        result = this;
                        return true;
                    }

                    result = new Text(
                        dst,
                        0,
                        dst.Length,
                        TextEncoding.Utf8,
                        dst.Length,
                        BackingType.ByteArray);

                    return true;
                }

            case TextEncoding.Utf16:
                {
                    var chars = MemoryMarshal.Cast<byte, char>(bytes);
                    if (!Ascii.IsValid(chars))
                    {
                        break;
                    }

                    var dst = new char[chars.Length];
                    Ascii.ToLower(chars, dst, out _);

                    if (chars.SequenceEqual(dst))
                    {
                        result = this;
                        return true;
                    }

                    result = new Text(
                        dst,
                        0,
                        dst.Length * 2,
                        TextEncoding.Utf16,
                        dst.Length,
                        BackingType.CharArray);

                    return true;
                }

            case TextEncoding.Utf32:
                {
                    var ints = MemoryMarshal.Cast<byte, int>(bytes);
                    if (ints.ContainsAnyExceptInRange(0, 127))
                    {
                        break;
                    }

                    if (!ints.ContainsAnyInRange('A', 'Z'))
                    {
                        result = this;
                        return true;
                    }

                    var dst = new int[ints.Length];
                    ToggleCaseAsciiUtf32(
                        ints,
                        dst,
                        32,
                        'A',
                        'Z');

                    result = new Text(
                        dst,
                        0,
                        dst.Length * 4,
                        TextEncoding.Utf32,
                        dst.Length,
                        BackingType.IntArray);

                    return true;
                }
        }

        result = default;
        return false;
    }

    bool TryUpperAsciiPooled(out OwnedText result)
    {
        var bytes = UnsafeBytes;

        switch (Encoding)
        {
            case TextEncoding.Utf8:
                {
                    var buffer = ArrayPool<byte>.Shared.Rent(bytes.Length);
                    var span = buffer.AsSpan(0, bytes.Length);
                    if (Ascii.ToUpper(bytes, span, out _) != OperationStatus.Done)
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
                        break;
                    }

                    if (bytes.SequenceEqual(span))
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf8, countRunes: false);
                        return true;
                    }

                    result = OwnedText.Create(
                        buffer,
                        bytes.Length,
                        TextEncoding.Utf8,
                        bytes.Length);

                    return true;
                }

            case TextEncoding.Utf16:
                {
                    var chars = MemoryMarshal.Cast<byte, char>(bytes);
                    var buffer = ArrayPool<char>.Shared.Rent(chars.Length);
                    var span = buffer.AsSpan(0, chars.Length);
                    if (Ascii.ToUpper(chars, span, out _) != OperationStatus.Done)
                    {
                        ArrayPool<char>.Shared.Return(buffer);
                        break;
                    }

                    if (chars.SequenceEqual(span))
                    {
                        ArrayPool<char>.Shared.Return(buffer);
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf16, countRunes: false);
                        return true;
                    }

                    result = OwnedText.Create(buffer, chars.Length);
                    return true;
                }

            case TextEncoding.Utf32:
                {
                    var ints = MemoryMarshal.Cast<byte, int>(bytes);
                    if (ints.ContainsAnyExceptInRange(0, 127))
                    {
                        break;
                    }

                    if (!ints.ContainsAnyInRange('a', 'z'))
                    {
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf32, countRunes: false);
                        return true;
                    }

                    var buffer = ArrayPool<int>.Shared.Rent(ints.Length);
                    ToggleCaseAsciiUtf32(
                        ints,
                        buffer.AsSpan(0, ints.Length),
                        -32,
                        'a',
                        'z');

                    result = OwnedText.Create(buffer, ints.Length);
                    return true;
                }
        }

        result = default!;
        return false;
    }

    bool TryLowerAsciiPooled(out OwnedText result)
    {
        var bytes = UnsafeBytes;

        switch (Encoding)
        {
            case TextEncoding.Utf8:
                {
                    var buffer = ArrayPool<byte>.Shared.Rent(bytes.Length);
                    var span = buffer.AsSpan(0, bytes.Length);
                    if (Ascii.ToLower(bytes, span, out _) != OperationStatus.Done)
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
                        break;
                    }

                    if (bytes.SequenceEqual(span))
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf8, countRunes: false);
                        return true;
                    }

                    result = OwnedText.Create(
                        buffer,
                        bytes.Length,
                        TextEncoding.Utf8,
                        bytes.Length);

                    return true;
                }

            case TextEncoding.Utf16:
                {
                    var chars = MemoryMarshal.Cast<byte, char>(bytes);
                    var buffer = ArrayPool<char>.Shared.Rent(chars.Length);
                    var span = buffer.AsSpan(0, chars.Length);
                    if (Ascii.ToLower(chars, span, out _) != OperationStatus.Done)
                    {
                        ArrayPool<char>.Shared.Return(buffer);
                        break;
                    }

                    if (chars.SequenceEqual(span))
                    {
                        ArrayPool<char>.Shared.Return(buffer);
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf16, countRunes: false);
                        return true;
                    }

                    result = OwnedText.Create(buffer, chars.Length);
                    return true;
                }

            case TextEncoding.Utf32:
                {
                    var ints = MemoryMarshal.Cast<byte, int>(bytes);
                    if (ints.ContainsAnyExceptInRange(0, 127))
                    {
                        break;
                    }

                    if (!ints.ContainsAnyInRange('A', 'Z'))
                    {
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf32, countRunes: false);
                        return true;
                    }

                    var buffer = ArrayPool<int>.Shared.Rent(ints.Length);
                    ToggleCaseAsciiUtf32(
                        ints,
                        buffer.AsSpan(0, ints.Length),
                        32,
                        'A',
                        'Z');

                    result = OwnedText.Create(buffer, ints.Length);
                    return true;
                }
        }

        result = default!;
        return false;
    }

    static void ToggleCaseAsciiUtf32(ReadOnlySpan<int> src,
        Span<int> dst,
        int delta,
        int low,
        int high)
    {
        for (var i = 0; i < src.Length; i++)
        {
            var v = src[i];
            dst[i] = v >= low && v <= high ? v + delta : v;
        }
    }

#endif
}
