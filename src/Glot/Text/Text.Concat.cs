using System.Buffers;

namespace Glot;

public readonly partial struct Text
{
    /// <summary>Concatenates two <see cref="Text"/> values.</summary>
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
    /// Use the overload with an explicit encoding parameter to control the output encoding.
    /// </summary>
    public static OwnedText? ConcatPooled(params ReadOnlySpan<Text> values)
        => ConcatPooled(values, ResolveEncoding(values));

    /// <summary>Like <see cref="Concat(ReadOnlySpan{Text}, TextEncoding)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
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
    public static Text operator +(Text left, Text right) => Concat(left, right);
}
