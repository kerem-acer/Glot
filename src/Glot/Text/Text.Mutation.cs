using System.Runtime.InteropServices;
using System.Text;

namespace Glot;

public readonly partial struct Text
{
    delegate TResult BuilderFinisher<out TResult>(ref TextBuilder builder);

    static Text FinishAsText(ref TextBuilder b) => b.ToText();
    static OwnedText FinishAsOwnedText(ref TextBuilder b) => b.ToOwnedText();

    // Replace

    /// <summary>Returns a new <see cref="Text"/> with all occurrences of <paramref name="oldValue"/> replaced by <paramref name="newValue"/>. Returns <c>this</c> if no match found.</summary>
    public Text Replace(Text oldValue, Text newValue)
    {
        if (oldValue.IsEmpty)
        {
            throw new ArgumentException("Value cannot be empty.", nameof(oldValue));
        }

        if (IsEmpty)
        {
            return this;
        }

        var firstBytePos = AsSpan().ByteIndexOf(oldValue.AsSpan());
        if (firstBytePos < 0)
        {
            return this;
        }

        return ReplaceCore(oldValue.AsSpan(), newValue.AsSpan(), firstBytePos, FinishAsText);
    }

    /// <inheritdoc cref="Replace(Text, Text)"/>
    public Text Replace(string oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(oldValue))
        {
            throw new ArgumentException("Value cannot be empty.", nameof(oldValue));
        }

        if (IsEmpty)
        {
            return this;
        }

        var oldSpan = new TextSpan(
            MemoryMarshal.AsBytes(oldValue.AsSpan()), TextEncoding.Utf16, 0);
        var firstBytePos = AsSpan().ByteIndexOf(oldSpan);
        if (firstBytePos < 0)
        {
            return this;
        }

        var newSpan = new TextSpan(
            MemoryMarshal.AsBytes(newValue.AsSpan()), TextEncoding.Utf16);
        return ReplaceCore(oldSpan, newSpan, firstBytePos, FinishAsText);
    }

    /// <summary>Like <see cref="Replace(Text, Text)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public OwnedText ReplacePooled(Text oldValue, Text newValue)
    {
        if (oldValue.IsEmpty)
        {
            throw new ArgumentException("Value cannot be empty.", nameof(oldValue));
        }

        if (IsEmpty)
        {
            return default;
        }

        var firstBytePos = AsSpan().ByteIndexOf(oldValue.AsSpan());
        if (firstBytePos < 0)
        {
            return OwnedText.FromBytes(AsSpan().Bytes, Encoding);
        }

        return ReplaceCore(oldValue.AsSpan(), newValue.AsSpan(), firstBytePos, FinishAsOwnedText);
    }

    /// <inheritdoc cref="ReplacePooled(Text, Text)"/>
    public OwnedText ReplacePooled(string oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(oldValue))
        {
            throw new ArgumentException("Value cannot be empty.", nameof(oldValue));
        }

        if (IsEmpty)
        {
            return default;
        }

        var oldSpan = new TextSpan(
            MemoryMarshal.AsBytes(oldValue.AsSpan()), TextEncoding.Utf16, 0);
        var firstBytePos = AsSpan().ByteIndexOf(oldSpan);
        if (firstBytePos < 0)
        {
            return OwnedText.FromBytes(AsSpan().Bytes, Encoding);
        }

        var newSpan = new TextSpan(
            MemoryMarshal.AsBytes(newValue.AsSpan()), TextEncoding.Utf16);
        return ReplaceCore(oldSpan, newSpan, firstBytePos, FinishAsOwnedText);
    }

    TResult ReplaceCore<TResult>(TextSpan oldValue, TextSpan newValue, int firstBytePos, BuilderFinisher<TResult> finish)
    {
        var builder = new TextBuilder(Encoding);
        try
        {
            var remaining = AsSpan();
            var sameEncoding = remaining.Encoding == oldValue.Encoding;
            var bytePos = firstBytePos;

            while (true)
            {
                if (bytePos > 0)
                {
                    builder.Append(remaining.ByteSlice(0, bytePos));
                }

                builder.Append(newValue);

                int matchByteLen;
                if (sameEncoding)
                {
                    matchByteLen = oldValue.ByteLength;
                }
                else
                {
                    var matched = RunePrefix.TryMatch(
                        remaining.Bytes[bytePos..],
                        remaining.Encoding,
                        oldValue.Bytes,
                        oldValue.Encoding,
                        out matchByteLen);

                    if (!matched || matchByteLen == 0)
                    {
                        throw new InvalidOperationException(
                            "Cross-encoding ByteIndexOf reported a match that RunePrefix.TryMatch could not confirm.");
                    }
                }

                remaining = remaining.ByteSlice(bytePos + matchByteLen);

                bytePos = remaining.ByteIndexOf(oldValue);
                if (bytePos < 0)
                {
                    builder.Append(remaining);
                    return finish(ref builder);
                }
            }
        }
        finally
        {
            builder.Dispose();
        }
    }

    // Insert

    /// <summary>Returns a new <see cref="Text"/> with <paramref name="value"/> inserted at the given rune index.</summary>
    public Text Insert(int runeIndex, Text value)
    {
        if ((uint)runeIndex > (uint)RuneLength)
        {
            throw new ArgumentOutOfRangeException(nameof(runeIndex));
        }

        if (value.IsEmpty)
        {
            return this;
        }

        return InsertCore(runeIndex, value.AsSpan(), FinishAsText);
    }

    /// <inheritdoc cref="Insert(int, Text)"/>
    public Text Insert(int runeIndex, string value)
        => Insert(runeIndex, From(value));

    /// <summary>Like <see cref="Insert(int, Text)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public OwnedText InsertPooled(int runeIndex, Text value)
    {
        if ((uint)runeIndex > (uint)RuneLength)
        {
            throw new ArgumentOutOfRangeException(nameof(runeIndex));
        }

        if (value.IsEmpty)
        {
            return OwnedText.FromBytes(AsSpan().Bytes, Encoding);
        }

        return InsertCore(runeIndex, value.AsSpan(), FinishAsOwnedText);
    }

    /// <inheritdoc cref="InsertPooled(int, Text)"/>
    public OwnedText InsertPooled(int runeIndex, string value)
        => InsertPooled(runeIndex, From(value));

    TResult InsertCore<TResult>(int runeIndex, TextSpan value, BuilderFinisher<TResult> finish)
    {
        var builder = new TextBuilder(Encoding);
        try
        {
            var span = AsSpan();
            var byteOffset = RuneIndex.ToByteOffset(span.Bytes, span.Encoding, runeIndex);
            builder.Append(new TextSpan(span.Bytes[..byteOffset], span.Encoding, runeIndex));
            builder.Append(value);
            builder.Append(new TextSpan(span.Bytes[byteOffset..], span.Encoding, span.RuneLength - runeIndex));
            return finish(ref builder);
        }
        finally
        {
            builder.Dispose();
        }
    }

    // Remove

    /// <summary>Returns a new <see cref="Text"/> with <paramref name="runeCount"/> runes removed starting at <paramref name="runeIndex"/>.</summary>
    public Text Remove(int runeIndex, int runeCount)
    {
        ValidateRemoveRange(runeIndex, runeCount);

        if (runeCount == 0)
        {
            return this;
        }

        if (runeIndex == 0)
        {
            return RuneSlice(runeCount);
        }

        if (runeIndex + runeCount == RuneLength)
        {
            return RuneSlice(0, runeIndex);
        }

        return RemoveCore(runeIndex, runeCount, FinishAsText);
    }

    /// <summary>Like <see cref="Remove(int, int)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public OwnedText RemovePooled(int runeIndex, int runeCount)
    {
        ValidateRemoveRange(runeIndex, runeCount);

        if (runeCount == 0)
        {
            return OwnedText.FromBytes(AsSpan().Bytes, Encoding);
        }

        return RemoveCore(runeIndex, runeCount, FinishAsOwnedText);
    }

    void ValidateRemoveRange(int runeIndex, int runeCount)
    {
        if ((uint)runeIndex > (uint)RuneLength)
        {
            throw new ArgumentOutOfRangeException(nameof(runeIndex));
        }

        if ((uint)runeCount > (uint)(RuneLength - runeIndex))
        {
            throw new ArgumentOutOfRangeException(nameof(runeCount));
        }
    }

    TResult RemoveCore<TResult>(int runeIndex, int runeCount, BuilderFinisher<TResult> finish)
    {
        var builder = new TextBuilder(Encoding);
        try
        {
            var span = AsSpan();
            var startByte = RuneIndex.ToByteOffset(span.Bytes, span.Encoding, runeIndex);
            var removedBytes = RuneIndex.ToByteOffset(span.Bytes[startByte..], span.Encoding, runeCount);
            builder.Append(new TextSpan(span.Bytes[..startByte], span.Encoding, runeIndex));
            builder.Append(new TextSpan(span.Bytes[(startByte + removedBytes)..], span.Encoding, span.RuneLength - runeIndex - runeCount));
            return finish(ref builder);
        }
        finally
        {
            builder.Dispose();
        }
    }

    // ToUpperInvariant / ToLowerInvariant

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to uppercase (invariant). Returns <c>this</c> if already uppercase.</summary>
    public Text ToUpperInvariant()
    {
        var firstChangeOffset = FindFirstCaseChange(upper: true, out var prefixRuneCount);
        if (firstChangeOffset < 0)
        {
            return this;
        }

        return CaseCore(upper: true, firstChangeOffset, prefixRuneCount, FinishAsText);
    }

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to lowercase (invariant). Returns <c>this</c> if already lowercase.</summary>
    public Text ToLowerInvariant()
    {
        var firstChangeOffset = FindFirstCaseChange(upper: false, out var prefixRuneCount);
        if (firstChangeOffset < 0)
        {
            return this;
        }

        return CaseCore(upper: false, firstChangeOffset, prefixRuneCount, FinishAsText);
    }

    /// <summary>Like <see cref="ToUpperInvariant()"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public OwnedText ToUpperInvariantPooled()
    {
        var firstChangeOffset = FindFirstCaseChange(upper: true, out var prefixRuneCount);
        if (firstChangeOffset < 0)
        {
            return OwnedText.FromBytes(AsSpan().Bytes, Encoding);
        }

        return CaseCore(upper: true, firstChangeOffset, prefixRuneCount, FinishAsOwnedText);
    }

    /// <summary>Like <see cref="ToLowerInvariant()"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public OwnedText ToLowerInvariantPooled()
    {
        var firstChangeOffset = FindFirstCaseChange(upper: false, out var prefixRuneCount);
        if (firstChangeOffset < 0)
        {
            return OwnedText.FromBytes(AsSpan().Bytes, Encoding);
        }

        return CaseCore(upper: false, firstChangeOffset, prefixRuneCount, FinishAsOwnedText);
    }

    // Concat

    /// <summary>
    /// Concatenates <see cref="Text"/> values into a new <see cref="Text"/>.
    /// The result uses the encoding of the first non-empty value (or UTF-8 if all are empty).
    /// Use the overload with an explicit encoding parameter to control the output encoding.
    /// </summary>
    public static Text Concat(params ReadOnlySpan<Text> values)
        => Concat(values, ResolveEncoding(values));

    /// <summary>Concatenates <see cref="Text"/> values into a new <see cref="Text"/> with the specified target encoding.</summary>
    public static Text Concat(ReadOnlySpan<Text> values, TextEncoding encoding)
    {
        if (values.IsEmpty)
        {
            return Empty;
        }

        if (values.Length == 1 && values[0].Encoding == encoding)
        {
            return values[0];
        }

        return ConcatCore(values, encoding, FinishAsText);
    }

    /// <summary>
    /// Like <see cref="Concat(ReadOnlySpan{Text})"/> but returns a pooled <see cref="OwnedText"/>.
    /// The result uses the encoding of the first non-empty value (or UTF-8 if all are empty).
    /// Use the overload with an explicit encoding parameter to control the output encoding.
    /// </summary>
    public static OwnedText ConcatPooled(params ReadOnlySpan<Text> values)
        => ConcatPooled(values, ResolveEncoding(values));

    /// <summary>Like <see cref="Concat(ReadOnlySpan{Text}, TextEncoding)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public static OwnedText ConcatPooled(ReadOnlySpan<Text> values, TextEncoding encoding)
    {
        if (values.IsEmpty)
        {
            return default;
        }

        return ConcatCore(values, encoding, FinishAsOwnedText);
    }

    static TResult ConcatCore<TResult>(ReadOnlySpan<Text> values, TextEncoding encoding, BuilderFinisher<TResult> finish)
    {
        var builder = new TextBuilder(encoding);
        try
        {
            for (var i = 0; i < values.Length; i++)
            {
                builder.Append(values[i]);
            }

            return finish(ref builder);
        }
        finally
        {
            builder.Dispose();
        }
    }

    static TextEncoding ResolveEncoding(ReadOnlySpan<Text> values)
    {
        for (var i = 0; i < values.Length; i++)
        {
            if (!values[i].IsEmpty)
            {
                return values[i].Encoding;
            }
        }

        return TextEncoding.Utf8;
    }

    /// <summary>Concatenates this text with another.</summary>
    public static Text operator +(Text left, Text right) => Concat(left, right);

    int FindFirstCaseChange(bool upper, out int prefixRuneCount)
    {
        var all = AsSpan().Bytes;
        var encoding = Encoding;
        var offset = 0;
        prefixRuneCount = 0;

        while (offset < all.Length)
        {
            Rune.TryDecodeFirst(all[offset..], encoding, out var rune, out var consumed);
            var converted = upper ? Rune.ToUpperInvariant(rune) : Rune.ToLowerInvariant(rune);

            if (converted != rune)
            {
                return offset;
            }

            offset += consumed;
            prefixRuneCount++;
        }

        return -1;
    }

    TResult CaseCore<TResult>(bool upper, int firstChangeOffset, int prefixRuneCount, BuilderFinisher<TResult> finish)
    {
        var builder = new TextBuilder(Encoding);
        try
        {
            var all = AsSpan().Bytes;
            var encoding = Encoding;

            if (firstChangeOffset > 0)
            {
                builder.AppendCounted(all[..firstChangeOffset], prefixRuneCount);
            }

            var offset = firstChangeOffset;
            var runStart = offset;
            var runRuneCount = 0;

            while (offset < all.Length)
            {
                Rune.TryDecodeFirst(all[offset..], encoding, out var rune, out var consumed);
                var converted = upper ? Rune.ToUpperInvariant(rune) : Rune.ToLowerInvariant(rune);

                if (converted != rune)
                {
                    if (offset > runStart)
                    {
                        builder.AppendCounted(all[runStart..offset], runRuneCount);
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
                builder.AppendCounted(all[runStart..offset], runRuneCount);
            }

            return finish(ref builder);
        }
        finally
        {
            builder.Dispose();
        }
    }
}
