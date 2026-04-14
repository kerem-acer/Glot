using System.Buffers;

namespace Glot.SystemTextJson;

/// <summary>
/// An <see cref="IBufferWriter{T}"/> backed by <see cref="ArrayPool{T}"/>.
/// Ownership of the buffer can be transferred to <see cref="OwnedText"/> via <see cref="ToOwnedText"/>.
/// Supports thread-local cache reuse via <see cref="PrepareForReuse"/>.
/// </summary>
sealed class PooledBufferWriter : IBufferWriter<byte>, IDisposable
{
    byte[] _buffer;
    int _written;
    bool _transferred;

    public PooledBufferWriter(int initialCapacity)
    {
        _buffer = initialCapacity > 0
            ? ArrayPool<byte>.Shared.Rent(initialCapacity)
            : [];
    }

    /// <summary>Creates an empty instance for thread-local caching.</summary>
    internal static PooledBufferWriter CreateForCaching() => new(0);

    /// <summary>
    /// Transfers ownership of the pooled buffer to a new <see cref="OwnedText"/>.
    /// After this call, <see cref="Dispose"/> becomes a no-op for the transferred buffer.
    /// </summary>
    public OwnedText ToOwnedText()
    {
        var result = OwnedText.Create(_buffer, _written, TextEncoding.Utf8);
        _transferred = true;
        return result;
    }

    /// <summary>
    /// Resets state for thread-local cache reuse. Returns the buffer to the pool
    /// if it wasn't transferred to <see cref="OwnedText"/>.
    /// </summary>
    internal void PrepareForReuse()
    {
        if (!_transferred && _buffer.Length > 0)
        {
            ArrayPool<byte>.Shared.Return(_buffer);
        }

        _buffer = [];
        _written = 0;
        _transferred = false;
    }

    public void Advance(int count) => _written += count;

    public Memory<byte> GetMemory(int sizeHint = 0)
    {
        EnsureCapacity(sizeHint);
        return _buffer.AsMemory(_written);
    }

    public Span<byte> GetSpan(int sizeHint = 0)
    {
        EnsureCapacity(sizeHint);
        return _buffer.AsSpan(_written);
    }

    void EnsureCapacity(int sizeHint)
    {
        var needed = Math.Max(sizeHint, 1);
        if (needed <= _buffer.Length - _written)
        {
            return;
        }

        var newSize = Math.Max(Math.Max(_buffer.Length, 256) * 2, _written + needed);
        var larger = ArrayPool<byte>.Shared.Rent(newSize);

        if (_written > 0)
        {
            _buffer.AsSpan(0, _written).CopyTo(larger);
        }

        if (_buffer.Length > 0)
        {
            ArrayPool<byte>.Shared.Return(_buffer);
        }

        _buffer = larger;
    }

    public void Dispose()
    {
        if (!_transferred && _buffer.Length > 0)
        {
            ArrayPool<byte>.Shared.Return(_buffer);
        }
    }
}
