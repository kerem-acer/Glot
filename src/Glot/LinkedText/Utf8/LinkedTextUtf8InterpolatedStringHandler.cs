#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace Glot;

/// <summary>
/// An interpolation handler that builds a <see cref="LinkedTextUtf8"/> from pooled resources.
/// </summary>
/// <remarks>Rents a <see cref="LinkedTextUtf8"/> from the pool. String literals are transcoded to UTF-8 segments. Formatted values use a shared format buffer.</remarks>
[InterpolatedStringHandler]
public struct LinkedTextUtf8InterpolatedStringHandler : IDisposable
{
    LinkedTextUtf8? _data;

    /// <summary>Creates a handler for building UTF-8 linked text.</summary>
    /// <param name="literalLength">The total length of all literal string segments in the interpolated string.</param>
    /// <param name="formattedCount">The number of formatted holes in the interpolated string.</param>
    public LinkedTextUtf8InterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _data = LinkedTextUtf8.Pool.Get();
        _data.OwnedTextHandling = OwnedTextHandling.Copy;
        _data.EnsureCapacity(formattedCount + 1);
    }

    /// <summary>Creates a handler with the specified <see cref="OwnedTextHandling"/> mode.</summary>
    /// <param name="literalLength">The total length of all literal string segments in the interpolated string.</param>
    /// <param name="formattedCount">The number of formatted holes in the interpolated string.</param>
    /// <param name="handling">Controls how <see cref="OwnedText"/> values are handled during interpolation.</param>
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

    /// <summary>Releases pooled resources if not already consumed.</summary>
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
    /// <param name="value">The literal string to append.</param>
    public readonly void AppendLiteral(string value) => _data!.AppendLiteral(value);

    /// <summary>Appends a string value.</summary>
    /// <param name="value">The string to append.</param>
    public readonly void AppendFormatted(string? value) => _data!.AppendFormatted(value);

    /// <summary>Appends a <see cref="ReadOnlyMemory{T}"/> segment.</summary>
    /// <param name="value">The UTF-8 memory to append.</param>
    public readonly void AppendFormatted(ReadOnlyMemory<byte> value) => _data!.AppendFormatted(value);

    /// <summary>Appends a <see cref="Text"/> value.</summary>
    /// <param name="value">The <see cref="Text"/> to append.</param>
    public readonly void AppendFormatted(Text value) => _data!.AppendFormatted(value);

    /// <summary>Appends an <see cref="OwnedText"/> value.</summary>
    /// <param name="value">The <see cref="OwnedText"/> to append.</param>
    public readonly void AppendFormatted(OwnedText? value) => _data!.AppendFormatted(value);

    /// <summary>Appends a <see cref="TextSpan"/> value.</summary>
    /// <param name="value">The <see cref="TextSpan"/> to append.</param>
    public readonly void AppendFormatted(TextSpan value) => _data!.AppendFormatted(value);

    /// <summary>Appends any formattable value.</summary>
    /// <param name="value">The value to format and append.</param>
    public readonly void AppendFormatted<T>(T value) => _data!.AppendFormatted(value);

    /// <summary>Appends a formattable value with format specifier.</summary>
    /// <param name="value">The value to format and append.</param>
    /// <param name="format">The format specifier.</param>
    public readonly void AppendFormatted<T>(T value, string? format) => _data!.AppendFormatted(value, format);
}
#endif
