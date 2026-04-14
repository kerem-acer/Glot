#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace Glot;

/// <summary>
/// A stack-allocated interpolated string handler that builds a <see cref="LinkedTextUtf8"/>
/// from pooled resources. Used via <see cref="OwnedLinkedTextUtf8.Create(LinkedTextUtf8InterpolatedStringHandler)"/>.
/// </summary>
[InterpolatedStringHandler]
public struct LinkedTextUtf8InterpolatedStringHandler : IDisposable
{
    LinkedTextUtf8? _data;

    /// <summary>Handler constructor. Rents from pool. Default: <see cref="OwnedTextHandling.Copy"/>.</summary>
    public LinkedTextUtf8InterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _data = LinkedTextUtf8.Pool.Get();
        _data.OwnedTextHandling = OwnedTextHandling.Copy;
        _data.EnsureCapacity(formattedCount + 1);
    }

    /// <summary>Handler constructor with explicit <see cref="OwnedTextHandling"/> control.</summary>
    public LinkedTextUtf8InterpolatedStringHandler(int literalLength, int formattedCount, OwnedTextHandling handling)
    {
        _data = LinkedTextUtf8.Pool.Get();
        _data.OwnedTextHandling = handling;
        _data.EnsureCapacity(formattedCount + 1);
    }

    /// <summary>Takes the built <see cref="LinkedTextUtf8"/> and nulls the internal reference. Caller owns the result.</summary>
    internal LinkedTextUtf8 Take()
    {
        var data = _data ?? throw new ObjectDisposedException(nameof(LinkedTextUtf8InterpolatedStringHandler));
        _data = null;
        return data;
    }

    /// <summary>Returns the pooled <see cref="LinkedTextUtf8"/> if not already taken.</summary>
    public void Dispose()
    {
        if (_data is null)
        {
            return;
        }

        var data = _data;
        _data = null;
        LinkedTextUtf8.Pool.Return(data);
    }

    /// <summary>Appends a literal string segment.</summary>
    public readonly void AppendLiteral(string value) => _data!.AppendLiteral(value);

    /// <summary>Appends a string value.</summary>
    public readonly void AppendFormatted(string? value) => _data!.AppendFormatted(value);

    /// <summary>Appends a <see cref="ReadOnlyMemory{T}"/> segment.</summary>
    public readonly void AppendFormatted(ReadOnlyMemory<byte> value) => _data!.AppendFormatted(value);

    /// <summary>Appends a <see cref="Text"/> value.</summary>
    public readonly void AppendFormatted(Text value) => _data!.AppendFormatted(value);

    /// <summary>Appends an <see cref="OwnedText"/> value.</summary>
    public readonly void AppendFormatted(OwnedText? value) => _data!.AppendFormatted(value);

    /// <summary>Appends a <see cref="TextSpan"/> value.</summary>
    public readonly void AppendFormatted(TextSpan value) => _data!.AppendFormatted(value);

    /// <summary>Appends any formattable value.</summary>
    public readonly void AppendFormatted<T>(T value) => _data!.AppendFormatted(value);

    /// <summary>Appends a formattable value with format specifier.</summary>
    public readonly void AppendFormatted<T>(T value, string? format) => _data!.AppendFormatted(value, format);
}
#endif
