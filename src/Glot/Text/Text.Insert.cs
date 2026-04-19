namespace Glot;

public readonly partial struct Text
{
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
    public OwnedText? InsertPooled(int runeIndex, Text value)
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
    public OwnedText? InsertPooled(int runeIndex, string value)
        => InsertPooled(runeIndex, From(value));

    TResult InsertCore<TResult>(int runeIndex, TextSpan value, BuilderFinisher<TResult> finish)
    {
        var builder = new TextBuilder(Encoding);
        try
        {
            var bytes = UnsafeBytes;
            var encoding = Encoding;
            var byteOffset = RuneIndex.ToByteOffset(bytes, encoding, runeIndex);
            builder.Append(new TextSpan(bytes[..byteOffset], encoding, runeIndex));
            builder.Append(value);
            builder.Append(new TextSpan(bytes[byteOffset..], encoding, RuneLength - runeIndex));
            return finish(ref builder);
        }
        finally
        {
            builder.Dispose();
        }
    }
}
