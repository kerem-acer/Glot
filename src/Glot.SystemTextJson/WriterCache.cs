using System.Text.Json;

namespace Glot.SystemTextJson;

/// <summary>
/// Thread-local cache for <see cref="Utf8JsonWriter"/> and <see cref="PooledBufferWriter"/> instances.
/// Same pattern as the runtime's internal Utf8JsonWriterCache — zero contention, handles recursion.
/// </summary>
static class WriterCache
{
    [ThreadStatic]
    static ThreadLocalState? t_state;

    public static Utf8JsonWriter Rent(JsonSerializerOptions? options, out PooledBufferWriter bufferWriter)
    {
        var state = t_state ??= new ThreadLocalState();

        if (state.RentCount++ == 0)
        {
            // First call on this thread — reuse cached instances.
            bufferWriter = state.BufferWriter;
            // PrepareForReuse already called on last Return; buffer rents on first GetSpan/GetMemory.

#if NET6_0_OR_GREATER
            if (ReferenceEquals(state.CachedOptions, options) && state.Writer is { } cachedWriter)
            {
                cachedWriter.Reset(bufferWriter);
                return cachedWriter;
            }
#endif

            var writer = CreateWriter(bufferWriter, options);
#if NET6_0_OR_GREATER
            state.Writer = writer;
            state.CachedOptions = options;
#endif
            return writer;
        }

        // Recursive call — fresh instances to avoid conflicts.
        bufferWriter = new PooledBufferWriter(256);
        return CreateWriter(bufferWriter, options);
    }

    public static void Return(Utf8JsonWriter writer, PooledBufferWriter bufferWriter)
    {
        var state = t_state!;

        bufferWriter.PrepareForReuse();

        var rentCount = --state.RentCount;
        if (rentCount != 0)
        {
            // Recursive instance — dispose, don't cache.
            writer.Dispose();
        }
    }

    static Utf8JsonWriter CreateWriter(PooledBufferWriter bufferWriter, JsonSerializerOptions? options)
    {
        var writerOptions = new JsonWriterOptions
        {
            Encoder = options?.Encoder,
            Indented = options?.WriteIndented ?? false,
            MaxDepth = options?.MaxDepth ?? 0,
            SkipValidation = true,
#if NET9_0_OR_GREATER
            NewLine = options?.NewLine ?? "\n",
#endif
        };

        return new Utf8JsonWriter(bufferWriter, writerOptions);
    }

    sealed class ThreadLocalState
    {
        public readonly PooledBufferWriter BufferWriter = PooledBufferWriter.CreateForCaching();
#if NET6_0_OR_GREATER
        public Utf8JsonWriter? Writer;
        public JsonSerializerOptions? CachedOptions;
#endif
        public int RentCount;
    }
}
