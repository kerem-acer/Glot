namespace Glot;

/// <summary>
/// A disposable handle that returns the <see cref="LinkedTextUtf8"/> and all its
/// rented resources to their pools on dispose.
/// </summary>
public struct LinkedTextUtf8Owned : IDisposable
{
    internal LinkedTextUtf8Owned(LinkedTextUtf8 data)
    {
        Data = data;
    }

    /// <summary>Returns <c>true</c> if this instance has been disposed or is default.</summary>
    public readonly bool IsDisposed => Data is null;

    /// <summary>The total byte count.</summary>
    public readonly int Length => Data?.Length ?? 0;

    /// <summary>Returns <c>true</c> if empty or disposed.</summary>
    public readonly bool IsEmpty => Data is null || Data.IsEmpty;

    /// <summary>The underlying <see cref="LinkedTextUtf8"/>. Valid only while not disposed.</summary>
    public LinkedTextUtf8? Data { get; private set; }

    /// <summary>Returns a <see cref="LinkedTextUtf8Span"/> over the full content.</summary>
    public readonly LinkedTextUtf8Span AsSpan()
    {
        if (Data is null)
        {
            return default;
        }

        return Data.AsSpan();
    }

    /// <summary>
    /// Returns all rented resources to their pools.
    /// </summary>
    public void Dispose()
    {
        if (Data is null)
        {
            return;
        }

        var data = Data;
        Data = null;
        LinkedTextUtf8.Pool.Return(data);
    }
}
