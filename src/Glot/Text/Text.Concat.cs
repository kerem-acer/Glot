using System.Buffers;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Concatenates two <see cref="Text"/> values.</summary>
    /// <param name="a">The first text.</param>
    /// <param name="b">The second text.</param>
    /// <returns>A new <see cref="Text"/> containing the concatenation of <paramref name="a"/> and <paramref name="b"/>.</returns>
    /// <remarks>Allocates a new <c>byte[]</c> to hold the combined content. When both texts share the same encoding, the bytes are copied directly. Otherwise, a <see cref="TextBuilder"/> transcodes on the fly.</remarks>
    /// <example>
    /// <code>
    /// var greeting = Text.Concat(Text.From("hello"), Text.From(" world"));
    /// </code>
    /// </example>
    public static Text Concat(Text a, Text b)
    {
        if (a.IsEmpty)
        {
            return b;
        }

        if (b.IsEmpty)
        {
            return a;
        }

        var encoding = a.Encoding;
        if (encoding == b.Encoding)
        {
            var totalBytes = a.ByteLength + b.ByteLength;
            var totalRunes = a.RuneLength + b.RuneLength;
            var buffer = new byte[totalBytes];
            a.UnsafeBytes.CopyTo(buffer);
            b.UnsafeBytes.CopyTo(buffer.AsSpan(a.ByteLength));
            return new Text(
                buffer,
                0,
                totalBytes,
                encoding,
                totalRunes,
                BackingType.ByteArray);
        }

        return ConcatCore(
            [
                a,
                b
            ],
            encoding,
            FinishAsText);
    }

    /// <summary>
    /// Concatenates <see cref="Text"/> values into a new <see cref="Text"/>.
    /// The result uses the encoding of the first non-empty value (or UTF-8 if all are empty).
    /// </summary>
    /// <param name="values">The values to concatenate.</param>
    /// <returns>A new <see cref="Text"/> containing the concatenation of <paramref name="values"/>.</returns>
    /// <remarks>The result uses the encoding of the first non-empty value. Allocates a single <c>byte[]</c> when all values share the same encoding.</remarks>
    /// <example>
    /// <code>
    /// Text result = Text.Concat(text1, text2, text3);
    /// </code>
    /// </example>
    public static Text Concat(params ReadOnlySpan<Text> values)
        => Concat(values, ResolveEncoding(values));

    /// <summary>Concatenates <see cref="Text"/> values into a new <see cref="Text"/> with the specified target encoding.</summary>
    /// <param name="values">The values to concatenate.</param>
    /// <param name="encoding">The target encoding for the result.</param>
    /// <returns>A new <see cref="Text"/> containing the concatenation of <paramref name="values"/> in the specified <paramref name="encoding"/>.</returns>
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

        if (TrySumSameEncoding(
            values,
            encoding,
            out var totalBytes,
            out var totalRunes))
        {
            if (totalBytes == 0)
            {
                return Empty;
            }

            var buffer = new byte[totalBytes];
            CopyAll(values, buffer);
            return new Text(
                buffer,
                0,
                totalBytes,
                encoding,
                totalRunes,
                BackingType.ByteArray);
        }

        return ConcatCore(values, encoding, FinishAsText);
    }

    /// <summary>
    /// Like <see cref="Concat(ReadOnlySpan{Text})"/> but returns a pooled <see cref="OwnedText"/>.
    /// The result uses the encoding of the first non-empty value (or UTF-8 if all are empty).
    /// </summary>
    /// <param name="values">The values to concatenate.</param>
    /// <returns>A pooled <see cref="OwnedText"/> containing the concatenation, or <c>null</c> if all values are empty.</returns>
    /// <remarks>Returns a pooled <see cref="OwnedText"/> backed by an <see cref="System.Buffers.ArrayPool{T}"/> buffer. The caller must dispose the result. Returns <c>null</c> when all values are empty.</remarks>
    public static OwnedText? ConcatPooled(params ReadOnlySpan<Text> values)
        => ConcatPooled(values, ResolveEncoding(values));

    /// <summary>Like <see cref="Concat(ReadOnlySpan{Text}, TextEncoding)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    /// <param name="values">The values to concatenate.</param>
    /// <param name="encoding">The target encoding for the result.</param>
    /// <returns>A pooled <see cref="OwnedText"/> containing the concatenation, or <c>null</c> if all values are empty.</returns>
    public static OwnedText? ConcatPooled(ReadOnlySpan<Text> values, TextEncoding encoding)
    {
        if (values.IsEmpty)
        {
            return null;
        }

        if (TrySumSameEncoding(
            values,
            encoding,
            out var totalBytes,
            out var totalRunes))
        {
            if (totalBytes == 0)
            {
                return null;
            }

            var buffer = ArrayPool<byte>.Shared.Rent(totalBytes);
            CopyAll(values, buffer);
            return OwnedText.Create(
                buffer,
                totalBytes,
                encoding,
                totalRunes);
        }

        return ConcatCore(values, encoding, FinishAsOwnedText);
    }

    static bool TrySumSameEncoding(
        ReadOnlySpan<Text> values,
        TextEncoding encoding,
        out int totalBytes,
        out int totalRunes)
    {
        totalBytes = 0;
        totalRunes = 0;
        foreach (var t in values)
        {
            if (t.IsEmpty)
            {
                continue;
            }

            if (t.Encoding != encoding)
            {
                return false;
            }

            totalBytes += t.ByteLength;
            totalRunes += t.RuneLength;
        }

        return true;
    }

    static void CopyAll(ReadOnlySpan<Text> values, byte[] buffer)
    {
        var offset = 0;
        foreach (var t in values)
        {
            if (t.IsEmpty)
            {
                continue;
            }

            var bytes = t.UnsafeBytes;
            bytes.CopyTo(buffer.AsSpan(offset));
            offset += bytes.Length;
        }
    }

    static TResult ConcatCore<TResult>(ReadOnlySpan<Text> values, TextEncoding encoding, BuilderFinisher<TResult> finish)
    {
        var builder = new TextBuilder(encoding);
        try
        {
            foreach (var t in values)
            {
                builder.Append(t);
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
        foreach (var t in values)
        {
            if (!t.IsEmpty)
            {
                return t.Encoding;
            }
        }

        return TextEncoding.Utf8;
    }

    /// <summary>Concatenates this text with another.</summary>
    /// <param name="left">The first text.</param>
    /// <param name="right">The second text.</param>
    /// <returns>A new <see cref="Text"/> containing the concatenation of <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <example>
    /// <code>
    /// Text result = text1 + text2;
    /// </code>
    /// </example>
    public static Text operator +(Text left, Text right) => Concat(left, right);
}
