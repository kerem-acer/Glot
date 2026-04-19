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
    public Text ToUpper() => ToUpper(CultureInfo.CurrentCulture);

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to uppercase using the specified culture.</summary>
    public Text ToUpper(CultureInfo culture)
    {
        if (IsEmpty)
        {
            return this;
        }

        return ToUpperCore(culture);
    }

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to lowercase using the current culture.</summary>
    public Text ToLower() => ToLower(CultureInfo.CurrentCulture);

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to lowercase using the specified culture.</summary>
    public Text ToLower(CultureInfo culture)
    {
        if (IsEmpty)
        {
            return this;
        }

        return ToLowerCore(culture);
    }

    /// <summary>Like <see cref="ToUpper(CultureInfo)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public OwnedText ToUpperPooled(CultureInfo culture)
    {
        if (IsEmpty)
        {
            return OwnedText.Empty;
        }

        return ToUpperPooledCore(culture);
    }

    /// <summary>Like <see cref="ToLower(CultureInfo)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
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

        return firstChangeOffset < 0 ?
            this :
            CaseCore(
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

        return firstChangeOffset < 0 ?
            this :
            CaseCore(
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

                    if (!bytes.ContainsAnyInRange((byte)'a', (byte)'z'))
                    {
                        result = this;
                        return true;
                    }

                    var dst = new byte[bytes.Length];
                    Ascii.ToUpper(bytes, dst, out _);
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

                    if (!chars.ContainsAnyInRange('a', 'z'))
                    {
                        result = this;
                        return true;
                    }

                    var dst = new char[chars.Length];
                    Ascii.ToUpper(chars, dst, out _);
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

                    if (!bytes.ContainsAnyInRange((byte)'A', (byte)'Z'))
                    {
                        result = this;
                        return true;
                    }

                    var dst = new byte[bytes.Length];
                    Ascii.ToLower(bytes, dst, out _);
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

                    if (!chars.ContainsAnyInRange('A', 'Z'))
                    {
                        result = this;
                        return true;
                    }

                    var dst = new char[chars.Length];
                    Ascii.ToLower(chars, dst, out _);
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
                    if (!Ascii.IsValid(bytes))
                    {
                        break;
                    }

                    if (!bytes.ContainsAnyInRange((byte)'a', (byte)'z'))
                    {
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf8, countRunes: false);
                        return true;
                    }

                    var buffer = ArrayPool<byte>.Shared.Rent(bytes.Length);
                    Ascii.ToUpper(bytes, buffer.AsSpan(0, bytes.Length), out _);
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
                    if (!Ascii.IsValid(chars))
                    {
                        break;
                    }

                    if (!chars.ContainsAnyInRange('a', 'z'))
                    {
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf16, countRunes: false);
                        return true;
                    }

                    var buffer = ArrayPool<char>.Shared.Rent(chars.Length);
                    Ascii.ToUpper(chars, buffer.AsSpan(0, chars.Length), out _);
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
                    if (!Ascii.IsValid(bytes))
                    {
                        break;
                    }

                    if (!bytes.ContainsAnyInRange((byte)'A', (byte)'Z'))
                    {
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf8, countRunes: false);
                        return true;
                    }

                    var buffer = ArrayPool<byte>.Shared.Rent(bytes.Length);
                    Ascii.ToLower(bytes, buffer.AsSpan(0, bytes.Length), out _);
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
                    if (!Ascii.IsValid(chars))
                    {
                        break;
                    }

                    if (!chars.ContainsAnyInRange('A', 'Z'))
                    {
                        result = OwnedText.FromBytes(bytes, TextEncoding.Utf16, countRunes: false);
                        return true;
                    }

                    var buffer = ArrayPool<char>.Shared.Rent(chars.Length);
                    Ascii.ToLower(chars, buffer.AsSpan(0, chars.Length), out _);
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
