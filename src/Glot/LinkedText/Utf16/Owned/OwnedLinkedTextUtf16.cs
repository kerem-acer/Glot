using Microsoft.Extensions.ObjectPool;

namespace Glot;

/// <summary>
/// A disposable handle that returns the <see cref="LinkedTextUtf16"/> and all its
/// rented resources to their pools on dispose. The wrapper itself is pooled.
/// </summary>
public sealed partial class OwnedLinkedTextUtf16 : IDisposable
{
    static readonly ObjectPool<OwnedLinkedTextUtf16> Pool =
        new DefaultObjectPool<OwnedLinkedTextUtf16>(new Policy(), 32);

    OwnedLinkedTextUtf16() { }

    static OwnedLinkedTextUtf16 GetFromPool(LinkedTextUtf16 data)
    {
        var owned = Pool.Get();
        owned.Data = data;
        return owned;
    }

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf16"/> from strings.</summary>
    public static OwnedLinkedTextUtf16 Create(params ReadOnlySpan<string> segments)
    {
        var linked = LinkedTextUtf16.Pool.Get();
        linked.PopulateStrings(segments);
        return GetFromPool(linked);
    }

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf16"/> from memory segments.</summary>
    public static OwnedLinkedTextUtf16 Create(params ReadOnlySpan<ReadOnlyMemory<char>> segments)
    {
        var linked = LinkedTextUtf16.Pool.Get();
        linked.Populate(segments);
        return GetFromPool(linked);
    }

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf16"/> from <see cref="Text"/> values.</summary>
    public static OwnedLinkedTextUtf16 Create(params ReadOnlySpan<Text> segments)
    {
        var linked = LinkedTextUtf16.Pool.Get();
        linked.PopulateTexts(segments);
        return GetFromPool(linked);
    }

#if NET6_0_OR_GREATER
    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf16"/> from an interpolated string. Default: <see cref="OwnedTextHandling.Copy"/>.</summary>
    public static OwnedLinkedTextUtf16 Create(LinkedTextUtf16InterpolatedStringHandler handler)
        => GetFromPool(handler.Take());

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf16"/> from an interpolated string with the specified <see cref="OwnedTextHandling"/> mode.</summary>
    public static OwnedLinkedTextUtf16 Create(
        OwnedTextHandling handling,
        [System.Runtime.CompilerServices.InterpolatedStringHandlerArgument("handling")]
        LinkedTextUtf16InterpolatedStringHandler handler)
        => GetFromPool(handler.Take());
#endif

    /// <summary>Returns <c>true</c> if this instance has been disposed or is default.</summary>
    public bool IsDisposed => Data is null;

    /// <summary>The total char count.</summary>
    public int Length => Data?.Length ?? 0;

    /// <summary>Returns <c>true</c> if empty or disposed.</summary>
    public bool IsEmpty => Data is null || Data.IsEmpty;

    /// <summary>The underlying <see cref="LinkedTextUtf16"/>. Valid only while not disposed.</summary>
    public LinkedTextUtf16? Data { get; private set; }

    /// <summary>Returns a <see cref="LinkedTextUtf16Span"/> over the full content.</summary>
    public LinkedTextUtf16Span AsSpan()
    {
        if (Data is null)
        {
            return default;
        }

        return Data.AsSpan();
    }

    /// <summary>Finalizer — returns the linked text data if Dispose was not called.</summary>
    ~OwnedLinkedTextUtf16() => ReturnData();

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
        LinkedTextUtf16.Pool.Return(data);
    }

    sealed class Policy : PooledObjectPolicy<OwnedLinkedTextUtf16>
    {
        public override OwnedLinkedTextUtf16 Create() => new();
        public override bool Return(OwnedLinkedTextUtf16 obj) => true;
    }
}
