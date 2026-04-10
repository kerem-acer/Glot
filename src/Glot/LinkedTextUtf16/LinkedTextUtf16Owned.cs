using System.Buffers;

namespace Glot;

/// <summary>
/// A disposable handle that returns the <see cref="LinkedTextUtf16"/> and all its
/// rented resources (overflow arrays, sequence nodes) to their pools on dispose.
/// Also serves as an interpolated string handler for direct assignment.
/// </summary>
#if NET6_0_OR_GREATER
[System.Runtime.CompilerServices.InterpolatedStringHandler]
#endif
public struct LinkedTextUtf16Owned : IDisposable
{
    internal LinkedTextUtf16Owned(LinkedTextUtf16 data)
    {
        Data = data;
    }

#if NET6_0_OR_GREATER
    /// <summary>Handler constructor for interpolated string support. Rents from pool.</summary>
    public LinkedTextUtf16Owned(int literalLength, int formattedCount)
    {
        Data = LinkedTextUtf16.Pool.Get();
        Data.EnsureCapacity(formattedCount + 1);
    }

    /// <summary>Appends a literal string segment.</summary>
    public readonly void AppendLiteral(string value) => Data!.AppendLiteral(value);

    /// <summary>Appends a string value.</summary>
    public readonly void AppendFormatted(string? value) => Data!.AppendFormatted(value);

    /// <summary>Appends a <see cref="ReadOnlyMemory{T}"/> segment.</summary>
    public readonly void AppendFormatted(ReadOnlyMemory<char> value) => Data!.AppendFormatted(value);

    /// <summary>Appends a <see cref="Text"/> value.</summary>
    public readonly void AppendFormatted(Text value) => Data!.AppendFormatted(value);

    /// <summary>Appends a <see cref="TextSpan"/> value.</summary>
    public readonly void AppendFormatted(TextSpan value) => Data!.AppendFormatted(value);

    /// <summary>Appends any formattable value.</summary>
    public readonly void AppendFormatted<T>(T value) => Data!.AppendFormatted(value);

    /// <summary>Appends a formattable value with format specifier.</summary>
    public readonly void AppendFormatted<T>(T value, string? format) => Data!.AppendFormatted(value, format);
#endif

    /// <summary>Returns <c>true</c> if this instance has been disposed or is default.</summary>
    public readonly bool IsDisposed => Data is null;

    /// <summary>The total char count.</summary>
    public readonly int Length => Data?.Length ?? 0;

    /// <summary>Returns <c>true</c> if empty or disposed.</summary>
    public readonly bool IsEmpty => Data is null || Data.IsEmpty;

    /// <summary>The underlying <see cref="LinkedTextUtf16"/>. Valid only while not disposed.</summary>
    public LinkedTextUtf16? Data { get; private set; }

    /// <summary>Returns a <see cref="LinkedTextUtf16Span"/> over the full content.</summary>
    public readonly LinkedTextUtf16Span AsSpan()
    {
        if (Data is null)
        {
            return default;
        }

        return Data.AsSpan();
    }

    /// <summary>
    /// Returns overflow array to <see cref="ArrayPool{T}"/>, sequence nodes to their pool,
    /// and the <see cref="LinkedTextUtf16"/> instance to the object pool.
    /// </summary>
    public void Dispose()
    {
        if (Data is null)
        {
            return;
        }

        var data = Data;
        Data = null;
        LinkedTextUtf16.Pool.Return(data);
    }
}
