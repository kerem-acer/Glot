using Microsoft.Extensions.ObjectPool;

namespace Glot;

/// <summary>
/// A disposable handle that returns the <see cref="LinkedTextUtf16"/> and its rented resources to their pools on dispose.
/// </summary>
/// <remarks>
/// <para>Both the <see cref="LinkedTextUtf16"/> and its overflow segment array are rented from pools.
/// This wrapper is itself pooled via <see cref="Microsoft.Extensions.ObjectPool.ObjectPool{T}"/>.</para>
/// </remarks>
public sealed partial class OwnedLinkedTextUtf16 :
    IDisposable,
    IEquatable<OwnedLinkedTextUtf16>,
    IEquatable<LinkedTextUtf16>,
    IEquatable<Text>,
    IComparable<OwnedLinkedTextUtf16>,
    IComparable<LinkedTextUtf16>,
    IComparable<Text>
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
    /// <param name="segments">The string values to compose.</param>
    /// <returns>A pooled <see cref="OwnedLinkedTextUtf16"/> containing the provided strings.</returns>
    /// <example>
    /// <code>
    /// using var owned = OwnedLinkedTextUtf16.Create("hello", " world");
    /// var span = owned.AsSpan(); // valid until disposed
    /// </code>
    /// </example>
    public static OwnedLinkedTextUtf16 Create(params ReadOnlySpan<string> segments)
    {
        var linked = LinkedTextUtf16.Pool.Get();
        linked.PopulateStrings(segments);
        return GetFromPool(linked);
    }

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf16"/> from memory segments.</summary>
    /// <param name="segments">The UTF-16 memory segments to compose.</param>
    /// <returns>A pooled <see cref="OwnedLinkedTextUtf16"/> containing the provided segments.</returns>
    public static OwnedLinkedTextUtf16 Create(params ReadOnlySpan<ReadOnlyMemory<char>> segments)
    {
        var linked = LinkedTextUtf16.Pool.Get();
        linked.Populate(segments);
        return GetFromPool(linked);
    }

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf16"/> from <see cref="Text"/> values.</summary>
    /// <param name="segments">The <see cref="Text"/> values to compose.</param>
    /// <returns>A pooled <see cref="OwnedLinkedTextUtf16"/> containing the provided values.</returns>
    public static OwnedLinkedTextUtf16 Create(params ReadOnlySpan<Text> segments)
    {
        var linked = LinkedTextUtf16.Pool.Get();
        linked.PopulateTexts(segments);
        return GetFromPool(linked);
    }

#if NET6_0_OR_GREATER
    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf16"/> from an interpolated string.</summary>
    /// <param name="handler">The interpolated string handler that provides the content.</param>
    /// <returns>A pooled <see cref="OwnedLinkedTextUtf16"/> containing the interpolated content.</returns>
    /// <remarks>The handler rents a <see cref="LinkedTextUtf16"/> from the pool. <see cref="OwnedText"/> holes use <see cref="OwnedTextHandling.Copy"/> by default.</remarks>
    /// <example>
    /// <code>
    /// using var owned = OwnedLinkedTextUtf16.Create($"count: {42}");
    /// </code>
    /// </example>
    public static OwnedLinkedTextUtf16 Create(LinkedTextUtf16InterpolatedStringHandler handler)
        => GetFromPool(handler.Take());

    /// <summary>Creates a pooled <see cref="OwnedLinkedTextUtf16"/> from an interpolated string with the specified <see cref="OwnedTextHandling"/> mode.</summary>
    /// <param name="handling">Controls how <see cref="OwnedText"/> values are handled during interpolation.</param>
    /// <param name="handler">The interpolated string handler that provides the content.</param>
    /// <returns>A pooled <see cref="OwnedLinkedTextUtf16"/> containing the interpolated content.</returns>
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
    /// <returns>A <see cref="LinkedTextUtf16Span"/> spanning the entire linked text, or <c>default</c> if disposed.</returns>
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

    /// <summary>Releases all rented resources and returns this instance to the pool.</summary>
    /// <remarks>Returns the <see cref="LinkedTextUtf16"/> (including its segment arrays and format buffers) to the pool, then returns this wrapper to the object pool.</remarks>
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
