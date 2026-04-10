using System.Text;

namespace Glot;

public readonly partial struct Text
{
    // Replace

    /// <summary>Returns a new <see cref="Text"/> with all occurrences of <paramref name="oldValue"/> replaced by <paramref name="newValue"/>. Returns <c>this</c> if no match found.</summary>
    public Text Replace(Text oldValue, Text newValue)
    {
        var builder = new TextBuilder(Encoding);
        if (!ReplaceCore(ref builder, oldValue.AsSpan(), newValue.AsSpan()))
        {
            builder.Dispose();
            return this;
        }

        var result = builder.ToText();
        builder.Dispose();
        return result;
    }

    /// <inheritdoc cref="Replace(Text, Text)"/>
    public Text Replace(string oldValue, string newValue)
        => Replace(From(oldValue), From(newValue));

    /// <summary>Like <see cref="Replace(Text, Text)"/> but returns a pooled <see cref="OwnedText"/>. Returns default if no match found.</summary>
    public OwnedText ReplacePooled(Text oldValue, Text newValue)
    {
        var builder = new TextBuilder(Encoding);
        if (!ReplaceCore(ref builder, oldValue.AsSpan(), newValue.AsSpan()))
        {
            builder.Dispose();
            return default;
        }

        var result = builder.ToOwnedText();
        builder.Dispose();
        return result;
    }

    /// <inheritdoc cref="ReplacePooled(Text, Text)"/>
    public OwnedText ReplacePooled(string oldValue, string newValue)
        => ReplacePooled(From(oldValue), From(newValue));

    bool ReplaceCore(ref TextBuilder builder, TextSpan oldValue, TextSpan newValue)
    {
        var remaining = AsSpan();
        var found = false;

        while (true)
        {
            var runePos = remaining.RuneIndexOf(oldValue);
            if (runePos < 0)
            {
                if (found)
                {
                    builder.Append(remaining);
                }

                return found;
            }

            found = true;
            builder.Append(remaining.RuneSlice(0, runePos));
            builder.Append(newValue);
            remaining = remaining.RuneSlice(runePos + oldValue.RuneLength);
        }
    }

    // Insert

    /// <summary>Returns a new <see cref="Text"/> with <paramref name="value"/> inserted at the given rune index.</summary>
    public Text Insert(int runeIndex, Text value)
    {
        if (value.IsEmpty)
        {
            return this;
        }

        var builder = new TextBuilder(Encoding);
        InsertCore(ref builder, runeIndex, value.AsSpan());
        var result = builder.ToText();
        builder.Dispose();
        return result;
    }

    /// <inheritdoc cref="Insert(int, Text)"/>
    public Text Insert(int runeIndex, string value)
        => Insert(runeIndex, From(value));

    /// <summary>Like <see cref="Insert(int, Text)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public OwnedText InsertPooled(int runeIndex, Text value)
    {
        if (value.IsEmpty)
        {
            return default;
        }

        var builder = new TextBuilder(Encoding);
        InsertCore(ref builder, runeIndex, value.AsSpan());
        var result = builder.ToOwnedText();
        builder.Dispose();
        return result;
    }

    /// <inheritdoc cref="InsertPooled(int, Text)"/>
    public OwnedText InsertPooled(int runeIndex, string value)
        => InsertPooled(runeIndex, From(value));

    void InsertCore(ref TextBuilder builder, int runeIndex, TextSpan value)
    {
        var span = AsSpan();
        builder.Append(span.RuneSlice(0, runeIndex));
        builder.Append(value);
        builder.Append(span.RuneSlice(runeIndex));
    }

    // Remove

    /// <summary>Returns a new <see cref="Text"/> with <paramref name="runeCount"/> runes removed starting at <paramref name="runeIndex"/>.</summary>
    public Text Remove(int runeIndex, int runeCount)
    {
        if (runeCount == 0)
        {
            return this;
        }

        if (runeIndex == 0)
        {
            return RuneSlice(runeCount);
        }

        if (runeIndex + runeCount >= RuneLength)
        {
            return RuneSlice(0, runeIndex);
        }

        var builder = new TextBuilder(Encoding);
        RemoveCore(ref builder, runeIndex, runeCount);
        var result = builder.ToText();
        builder.Dispose();
        return result;
    }

    /// <summary>Like <see cref="Remove(int, int)"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public OwnedText RemovePooled(int runeIndex, int runeCount)
    {
        if (runeCount == 0)
        {
            return default;
        }

        var builder = new TextBuilder(Encoding);
        if (runeIndex == 0)
        {
            builder.Append(AsSpan().RuneSlice(runeCount));
        }
        else if (runeIndex + runeCount >= RuneLength)
        {
            builder.Append(AsSpan().RuneSlice(0, runeIndex));
        }
        else
        {
            RemoveCore(ref builder, runeIndex, runeCount);
        }

        var result = builder.ToOwnedText();
        builder.Dispose();
        return result;
    }

    void RemoveCore(ref TextBuilder builder, int runeIndex, int runeCount)
    {
        var span = AsSpan();
        builder.Append(span.RuneSlice(0, runeIndex));
        builder.Append(span.RuneSlice(runeIndex + runeCount));
    }

    // ToUpperInvariant / ToLowerInvariant

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to uppercase (invariant). Returns <c>this</c> if already uppercase.</summary>
    public Text ToUpperInvariant()
    {
        var builder = new TextBuilder(Encoding);
        if (!CaseCore(ref builder, upper: true))
        {
            builder.Dispose();
            return this;
        }

        var result = builder.ToText();
        builder.Dispose();
        return result;
    }

    /// <summary>Returns a new <see cref="Text"/> with all runes converted to lowercase (invariant). Returns <c>this</c> if already lowercase.</summary>
    public Text ToLowerInvariant()
    {
        var builder = new TextBuilder(Encoding);
        if (!CaseCore(ref builder, upper: false))
        {
            builder.Dispose();
            return this;
        }

        var result = builder.ToText();
        builder.Dispose();
        return result;
    }

    /// <summary>Like <see cref="ToUpperInvariant()"/> but returns a pooled <see cref="OwnedText"/>. Returns default if already uppercase.</summary>
    public OwnedText ToUpperInvariantPooled()
    {
        var builder = new TextBuilder(Encoding);
        if (!CaseCore(ref builder, upper: true))
        {
            builder.Dispose();
            return default;
        }

        var result = builder.ToOwnedText();
        builder.Dispose();
        return result;
    }

    /// <summary>Like <see cref="ToLowerInvariant()"/> but returns a pooled <see cref="OwnedText"/>. Returns default if already lowercase.</summary>
    public OwnedText ToLowerInvariantPooled()
    {
        var builder = new TextBuilder(Encoding);
        if (!CaseCore(ref builder, upper: false))
        {
            builder.Dispose();
            return default;
        }

        var result = builder.ToOwnedText();
        builder.Dispose();
        return result;
    }

    // Concat

    /// <summary>Concatenates <see cref="Text"/> values into a new <see cref="Text"/>.</summary>
    public static Text Concat(params ReadOnlySpan<Text> values)
    {
        if (values.IsEmpty) return Empty;
        if (values.Length == 1) return values[0];

        var encoding = TextEncoding.Utf8;
        for (var i = 0; i < values.Length; i++)
        {
            if (!values[i].IsEmpty)
            {
                encoding = values[i].Encoding;
                break;
            }
        }

        var builder = new TextBuilder(encoding);
        for (var i = 0; i < values.Length; i++)
        {
            builder.Append(values[i]);
        }

        var result = builder.ToText();
        builder.Dispose();
        return result;
    }

    /// <summary>Like <see cref="Concat(ReadOnlySpan{Text})"/> but returns a pooled <see cref="OwnedText"/>.</summary>
    public static OwnedText ConcatPooled(params ReadOnlySpan<Text> values)
    {
        if (values.IsEmpty) return default;

        var encoding = TextEncoding.Utf8;
        for (var i = 0; i < values.Length; i++)
        {
            if (!values[i].IsEmpty)
            {
                encoding = values[i].Encoding;
                break;
            }
        }

        var builder = new TextBuilder(encoding);
        for (var i = 0; i < values.Length; i++)
        {
            builder.Append(values[i]);
        }

        var result = builder.ToOwnedText();
        builder.Dispose();
        return result;
    }

    /// <summary>Concatenates this text with another.</summary>
    public static Text operator +(Text left, Text right) => Concat(left, right);

    bool CaseCore(ref TextBuilder builder, bool upper)
    {
        var changed = false;

        foreach (var rune in AsSpan().EnumerateRunes())
        {
            var converted = upper ? Rune.ToUpperInvariant(rune) : Rune.ToLowerInvariant(rune);
            if (converted != rune)
            {
                changed = true;
            }

            builder.AppendRune(converted);
        }

        return changed;
    }
}
