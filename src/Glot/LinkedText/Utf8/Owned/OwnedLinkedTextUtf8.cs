using Microsoft.Extensions.ObjectPool;

namespace Glot;

/// <summary>
/// A disposable handle that returns the <see cref="LinkedTextUtf8"/> and its rented resources to their pools on dispose.
/// </summary>
/// <remarks>
/// <para>Both the <see cref="LinkedTextUtf8"/> and its overflow segment array are rented from pools.
/// This wrapper is itself pooled via <see cref="Microsoft.Extensions.ObjectPool.ObjectPool{T}"/>.</para>
/// </remarks>
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
    /// <param name="segments">The UTF-8 memory segments to compose.</param>
    /// <returns>A pooled <see cref="OwnedLinkedTextUtf8"/> containing the provided segments.</returns>
    /// <example>
    /// <code>
    /// using var owned = OwnedLinkedTextUtf8.Create("hello"u8.ToArray().AsMemory());
    /// var span = owned.AsSpan(); // valid until disposed
    /// </code>
    /// </example>
    public static OwnedLinkedTextUtf8 Create(params ReadOnlySpan<ReadOnlyMemory<byte>> segments)
    {
        var linked = LinkedTextUtf8.Pool.Get();
        linked.Populate(segments);
        return GetFromPool(linked);
    }

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf8"/> from <see cref="Text"/> values.</summary>
    /// <param name="segments">The <see cref="Text"/> values to compose.</param>
    /// <returns>A pooled <see cref="OwnedLinkedTextUtf8"/> containing the provided values.</returns>
    public static OwnedLinkedTextUtf8 Create(params ReadOnlySpan<Text> segments)
    {
        var linked = LinkedTextUtf8.Pool.Get();
        linked.PopulateTexts(segments);
        return GetFromPool(linked);
    }

#if NET8_0_OR_GREATER
    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf8"/> from an interpolated string.</summary>
    /// <param name="handler">The interpolated string handler that provides the content.</param>
    /// <returns>A pooled <see cref="OwnedLinkedTextUtf8"/> containing the interpolated content.</returns>
    /// <remarks>The handler rents a <see cref="LinkedTextUtf8"/> from the pool. <see cref="OwnedText"/> holes use <see cref="OwnedTextHandling.Copy"/> by default.</remarks>
    /// <example>
    /// <code>
    /// using var owned = OwnedLinkedTextUtf8.Create($"count: {42}");
    /// </code>
    /// </example>
    public static OwnedLinkedTextUtf8 Create(LinkedTextUtf8InterpolatedStringHandler handler)
        => GetFromPool(handler.Take());

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf8"/> from an interpolated string with the specified <see cref="OwnedTextHandling"/> mode.</summary>
    /// <param name="handling">Controls how <see cref="OwnedText"/> values are handled during interpolation.</param>
    /// <param name="handler">The interpolated string handler that provides the content.</param>
    /// <returns>A pooled <see cref="OwnedLinkedTextUtf8"/> containing the interpolated content.</returns>
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
    /// <returns>A <see cref="LinkedTextUtf8Span"/> spanning the entire linked text, or <c>default</c> if disposed.</returns>
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

    /// <summary>Releases all rented resources and returns this instance to the pool.</summary>
    /// <remarks>Returns the <see cref="LinkedTextUtf8"/> (including its segment arrays and format buffers) to the pool, then returns this wrapper to the object pool.</remarks>
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
