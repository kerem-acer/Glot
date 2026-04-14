using Microsoft.Extensions.ObjectPool;

namespace Glot;

/// <summary>
/// A disposable handle that returns the <see cref="LinkedTextUtf8"/> and all its
/// rented resources to their pools on dispose. The wrapper itself is pooled.
/// </summary>
public sealed partial class OwnedLinkedTextUtf8 : IDisposable
{
    static readonly ObjectPool<OwnedLinkedTextUtf8> Pool =
        new DefaultObjectPool<OwnedLinkedTextUtf8>(new Policy(), 32);

    OwnedLinkedTextUtf8() { }

    static OwnedLinkedTextUtf8 GetFromPool(LinkedTextUtf8 data)
    {
        var owned = Pool.Get();
        owned.Data = data;
        return owned;
    }

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf8"/> from memory segments.</summary>
    public static OwnedLinkedTextUtf8 Create(params ReadOnlySpan<ReadOnlyMemory<byte>> segments)
    {
        var linked = LinkedTextUtf8.Pool.Get();
        linked.Populate(segments);
        return GetFromPool(linked);
    }

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf8"/> from <see cref="Text"/> values.</summary>
    public static OwnedLinkedTextUtf8 Create(params ReadOnlySpan<Text> segments)
    {
        var linked = LinkedTextUtf8.Pool.Get();
        linked.PopulateTexts(segments);
        return GetFromPool(linked);
    }

#if NET8_0_OR_GREATER
    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf8"/> from an interpolated string. Default: <see cref="OwnedTextHandling.Copy"/>.</summary>
    public static OwnedLinkedTextUtf8 Create(LinkedTextUtf8InterpolatedStringHandler handler)
        => GetFromPool(handler.Take());

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf8"/> from an interpolated string with the specified <see cref="OwnedTextHandling"/> mode.</summary>
    public static OwnedLinkedTextUtf8 Create(
        OwnedTextHandling handling,
        [System.Runtime.CompilerServices.InterpolatedStringHandlerArgument("handling")]
        LinkedTextUtf8InterpolatedStringHandler handler)
        => GetFromPool(handler.Take());
#endif

    /// <summary>Returns <c>true</c> if this instance has been disposed or is default.</summary>
    public bool IsDisposed => Data is null;

    /// <summary>The total byte count.</summary>
    public int Length => Data?.Length ?? 0;

    /// <summary>Returns <c>true</c> if empty or disposed.</summary>
    public bool IsEmpty => Data is null || Data.IsEmpty;

    /// <summary>The underlying <see cref="LinkedTextUtf8"/>. Valid only while not disposed.</summary>
    public LinkedTextUtf8? Data { get; private set; }

    /// <summary>Returns a <see cref="LinkedTextUtf8Span"/> over the full content.</summary>
    public LinkedTextUtf8Span AsSpan()
    {
        if (Data is null)
        {
            return default;
        }

        return Data.AsSpan();
    }

    /// <summary>Finalizer — returns the linked text data if Dispose was not called.</summary>
    ~OwnedLinkedTextUtf8() => ReturnData();

    /// <summary>Returns all rented resources to their pools, including the wrapper itself.</summary>
    public void Dispose()
    {
        if (Data is null)
        {
            return;
        }

        ReturnData();
        GC.SuppressFinalize(this);
        Pool.Return(this);
    }

    void ReturnData()
    {
        if (Data is null)
        {
            return;
        }

        var data = Data;
        Data = null;
        LinkedTextUtf8.Pool.Return(data);
    }

    sealed class Policy : PooledObjectPolicy<OwnedLinkedTextUtf8>
    {
        public override OwnedLinkedTextUtf8 Create() => new();
        public override bool Return(OwnedLinkedTextUtf8 obj) => true;
    }
}
