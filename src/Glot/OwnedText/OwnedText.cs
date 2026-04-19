using System.Buffers;
using Microsoft.Extensions.ObjectPool;

namespace Glot;

/// <summary>
/// A disposable text value that owns a pool-backed buffer.
/// Returns the buffer to <see cref="ArrayPool{T}"/> and the wrapper to <see cref="ObjectPool{T}"/> on dispose.
/// Use <see cref="Text"/> to read the content; use <c>using</c> to manage lifetime.
/// </summary>
public sealed partial class OwnedText : IDisposable
{
    /// <summary>A shared empty <see cref="OwnedText"/> instance. Dispose is a no-op.</summary>
    public static readonly OwnedText Empty = new();

    static readonly ObjectPool<OwnedText> Pool =
        new DefaultObjectPool<OwnedText>(new Policy(), 32);

    Text _text;

    OwnedText() { }

    /// <summary>The Unicode encoding of the text.</summary>
    public TextEncoding Encoding => _text.Encoding;

    /// <summary>The number of Unicode runes (scalar values).</summary>
    public int RuneLength => _text.RuneLength;

    /// <summary>The number of bytes in the encoded representation.</summary>
    public int ByteLength => _text.ByteLength;

    /// <summary>Returns <c>true</c> if this instance has been disposed.</summary>
    public bool IsDisposed { get; private set; }

    /// <summary>Returns <c>true</c> if this value contains no text.</summary>
    public bool IsEmpty => _text.IsEmpty;

    /// <summary>
    /// Returns a <see cref="Glot.Text"/> view over the pooled buffer. O(1).
    /// The returned value is valid only while this <see cref="OwnedText"/> has not been disposed.
    /// </summary>
    public Text Text => _text;

    internal void Initialize(object buffer,
        int byteLength,
        TextEncoding encoding,
        int runeLength,
        BackingType backingType)
    {
        _text = new Text(
            buffer,
            0,
            byteLength,
            encoding,
            runeLength,
            backingType);
    }

    /// <summary>
    /// Detaches the pooled buffer from this instance, transferring ownership to the caller.
    /// After detach, <see cref="Dispose"/> will not return the data buffer to <see cref="ArrayPool{T}"/>.
    /// </summary>
    internal object? Detach()
    {
        var data = _text.Data;
        _text = default;
        return data;
    }

    internal static OwnedText GetFromPool()
    {
        var owned = Pool.Get();
        owned.IsDisposed = false;
        return owned;
    }

    /// <summary>Finalizer — returns the data buffer if Dispose was not called. Does not return the wrapper to the pool.</summary>
    ~OwnedText() => ReturnBuffer();

    /// <summary>Returns the pooled buffer to the appropriate <see cref="ArrayPool{T}"/> and the wrapper to the object pool.</summary>
    public void Dispose()
    {
        if (this == Empty || IsDisposed)
        {
            return;
        }

        IsDisposed = true;
        ReturnBuffer();
        GC.SuppressFinalize(this);
        Pool.Return(this);
    }

    void ReturnBuffer()
    {
        var data = _text.Data;
        if (data is null)
        {
            return;
        }

        switch (_text.DataBackingType)
        {
            case BackingType.ByteArray:
                ArrayPool<byte>.Shared.Return((byte[])data);
                break;

            case BackingType.CharArray:
                ArrayPool<char>.Shared.Return((char[])data);
                break;

            case BackingType.IntArray:
                ArrayPool<int>.Shared.Return((int[])data);
                break;
        }

        _text = default;
    }

    sealed class Policy : PooledObjectPolicy<OwnedText>
    {
        public override OwnedText Create() => new();

        public override bool Return(OwnedText obj) => true;
    }
}
