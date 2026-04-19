using System.Runtime.CompilerServices;

namespace Glot;

public readonly partial struct Text
{
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
            return OwnedText.FromBytes(Bytes, Encoding);
        }

        return RemoveCore(runeIndex, runeCount, FinishAsOwnedText);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            var bytes = UnsafeBytes;
            var encoding = Encoding;
            var startByte = RuneIndex.ToByteOffset(bytes, encoding, runeIndex);
            var removedBytes = RuneIndex.ToByteOffset(bytes[startByte..], encoding, runeCount);
            builder.Append(new TextSpan(bytes[..startByte], encoding, runeIndex));
            builder.Append(new TextSpan(bytes[(startByte + removedBytes)..], encoding, RuneLength - runeIndex - runeCount));
            return finish(ref builder);
        }
        finally
        {
            builder.Dispose();
        }
    }
}
