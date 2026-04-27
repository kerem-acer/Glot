using System.Buffers;
using Microsoft.Extensions.ObjectPool;

namespace Glot;

/// <summary>
/// A disposable text value that owns a pooled buffer.
/// Use <see cref="Text"/> to read the content; use <c>using</c> to manage lifetime.
/// </summary>
/// <remarks>
/// <para>The backing buffer is rented from <see cref="System.Buffers.ArrayPool{T}"/> and the wrapper
/// object is pooled via <see cref="Microsoft.Extensions.ObjectPool.ObjectPool{T}"/>.</para>
/// <para>Read content through <see cref="Text"/>. The <see cref="Text"/> view is valid only while this
/// instance has not been disposed.</para>
/// </remarks>
public sealed partial class OwnedText :
    IDisposable,
    IEquatable<OwnedText>,
    IEquatable<Text>,
    IComparable<OwnedText>,
    IComparable<Text>
{
    /// <summary>A shared empty <see cref="OwnedText"/> instance.</summary>
    public static readonly OwnedText Empty = new();

    static readonly ObjectPool<OwnedText> Pool =
        new DefaultObjectPool<OwnedText>(new Policy(), 32);

    Text _text;
    bool _ownsBuffer;

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

    /// <summary>Returns a <see cref="Glot.Text"/> view over the pooled buffer.</summary>
    /// <remarks>The returned <see cref="Glot.Text"/> references the pooled buffer. Do not use it after this <see cref="OwnedText"/> is disposed.</remarks>
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
        _ownsBuffer = true;
    }

    internal void InitializeView(Text value)
    {
        _text = value;
        _ownsBuffer = false;
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
    ~OwnedText()
    {
        if (_ownsBuffer)
        {
            ReturnBuffer();
        }
    }

    /// <summary>Releases the pooled buffer and returns this instance to the pool.</summary>
    /// <remarks>Returns the backing array to <see cref="System.Buffers.ArrayPool{T}"/> and the wrapper to the object pool. Calling <see cref="Dispose"/> multiple times is safe.</remarks>
    public void Dispose()
    {
        if (this == Empty || IsDisposed)
        {
            return;
        }

        IsDisposed = true;
        if (_ownsBuffer)
        {
            ReturnBuffer();
        }
        else
        {
            _text = default;
        }
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
