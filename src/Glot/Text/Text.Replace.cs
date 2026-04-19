using System.Buffers;
using System.Runtime.InteropServices;

namespace Glot;

public readonly partial struct Text
{
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

        var firstBytePos = ByteIndexOf(oldValue);
        if (firstBytePos < 0)
        {
            return this;
        }

        return ReplaceCore(
            oldValue.AsSpan(),
            newValue.AsSpan(),
            firstBytePos,
            FinishAsText);
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
            MemoryMarshal.AsBytes(oldValue.AsUnsafeSpan()),
            TextEncoding.Utf16,
            0);

        var firstBytePos = ByteIndexOf(oldSpan);
        if (firstBytePos < 0)
        {
            return this;
        }

        var newSpan = new TextSpan(
            MemoryMarshal.AsBytes(newValue.AsUnsafeSpan()),
            TextEncoding.Utf16);

        return ReplaceCore(
            oldSpan,
            newSpan,
            firstBytePos,
            FinishAsText);
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
            return OwnedText.Empty;
        }

        var firstBytePos = ByteIndexOf(oldValue);
        if (firstBytePos < 0)
        {
            return OwnedText.FromBytes(UnsafeBytes, Encoding);
        }

        return ReplaceCore(
            oldValue.AsSpan(),
            newValue.AsSpan(),
            firstBytePos,
            FinishAsOwnedText);
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
            return OwnedText.Empty;
        }

        var oldSpan = new TextSpan(
            MemoryMarshal.AsBytes(oldValue.AsUnsafeSpan()),
            TextEncoding.Utf16,
            0);

        var firstBytePos = ByteIndexOf(oldSpan);
        if (firstBytePos < 0)
        {
            return OwnedText.FromBytes(UnsafeBytes, Encoding);
        }

        var newSpan = new TextSpan(
            MemoryMarshal.AsBytes(newValue.AsUnsafeSpan()),
            TextEncoding.Utf16);

        return ReplaceCore(
            oldSpan,
            newSpan,
            firstBytePos,
            FinishAsOwnedText);
    }

    TResult ReplaceCore<TResult>(TextSpan oldValue,
        TextSpan newValue,
        int firstBytePos,
        BuilderFinisher<TResult> finish)
    {
        if (Encoding == oldValue.Encoding && Encoding == newValue.Encoding)
        {
            return ReplaceSameEncoding(
                oldValue.Bytes,
                newValue.Bytes,
                RuneLength,
                oldValue.RuneLength,
                newValue.RuneLength,
                firstBytePos,
                finish);
        }

        return ReplaceCrossEncoding(
            oldValue,
            newValue,
            firstBytePos,
            finish);
    }

    TResult ReplaceCrossEncoding<TResult>(TextSpan oldValue,
        TextSpan newValue,
        int firstBytePos,
        BuilderFinisher<TResult> finish)
    {
        // Transcode needle and replacement to this Text's encoding, then use the fast path.
        var encoding = Encoding;
        var maxOld = TranscodeSize.Estimate(oldValue.Bytes.Length, oldValue.Encoding, encoding);
        var maxNew = TranscodeSize.Estimate(newValue.Bytes.Length, newValue.Encoding, encoding);

        byte[]? rentedOld = null, rentedNew = null;
        Span<byte> oldBuf = maxOld <= 256 ? stackalloc byte[maxOld] : (rentedOld = ArrayPool<byte>.Shared.Rent(maxOld));
        Span<byte> newBuf = maxNew <= 256 ? stackalloc byte[maxNew] : (rentedNew = ArrayPool<byte>.Shared.Rent(maxNew));

        try
        {
            var oldWritten = TextSpan.TranscodeToEncoding(oldValue, oldBuf, encoding);
            var newWritten = TextSpan.TranscodeToEncoding(newValue, newBuf, encoding);

            return ReplaceSameEncoding(
                oldBuf[..oldWritten],
                newBuf[..newWritten],
                RuneLength,
                oldValue.RuneLength,
                newValue.RuneLength,
                firstBytePos,
                finish);
        }
        finally
        {
            if (rentedOld is not null)
            {
                ArrayPool<byte>.Shared.Return(rentedOld);
            }

            if (rentedNew is not null)
            {
                ArrayPool<byte>.Shared.Return(rentedNew);
            }
        }
    }

    TResult ReplaceSameEncoding<TResult>(
        ReadOnlySpan<byte> needleBytes,
        ReadOnlySpan<byte> replacementBytes,
        int sourceRuneLength,
        int needleRuneLength,
        int replacementRuneLength,
        int firstBytePos,
        BuilderFinisher<TResult> finish)
    {
        var builder = new TextBuilder(Encoding);
        try
        {
            var source = UnsafeBytes;
            var cursor = 0;
            var bytePos = firstBytePos;
            var matchCount = 0;

            while (true)
            {
                // Append bytes before the match (rune count deferred).
                var segEnd = cursor + bytePos;
                if (segEnd > cursor)
                {
                    builder.AppendCounted(source[cursor..segEnd], 0);
                }

                // Append replacement (rune count deferred).
                builder.AppendCounted(replacementBytes, 0);
                matchCount++;

                cursor = segEnd + needleBytes.Length;

                var next = source[cursor..].IndexOf(needleBytes);
                if (next < 0)
                {
                    // Append tail.
                    if (cursor < source.Length)
                    {
                        builder.AppendCounted(source[cursor..], 0);
                    }

                    break;
                }

                bytePos = next;
            }

            // Fix up rune length via O(1) arithmetic.
            builder.RuneLength = sourceRuneLength - (matchCount * needleRuneLength) + (matchCount * replacementRuneLength);

            return finish(ref builder);
        }
        finally
        {
            builder.Dispose();
        }
    }

}
