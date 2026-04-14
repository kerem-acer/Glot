using System.Text.Json;

namespace Glot.SystemTextJson;

/// <summary>
/// Extends <see cref="JsonSerializer"/> with methods that serialize directly into pooled <see cref="OwnedText"/>
/// or stream to a destination.
/// </summary>
public static class JsonSerializerGlotExtensions
{
    extension(JsonSerializer)
    {
        /// <summary>
        /// Serializes <paramref name="value"/> as UTF-8 JSON into a pooled <see cref="OwnedText"/>.
        /// Uses thread-local cached writer and buffer — zero allocations on the hot path.
        /// Caller must dispose the returned value to return the buffer to the pool.
        /// </summary>
        public static OwnedText SerializeToUtf8OwnedText<T>(T value, JsonSerializerOptions? options = null)
        {
            var writer = WriterCache.Rent(options, out var bufferWriter);
            try
            {
                JsonSerializer.Serialize(writer, value, options);
                writer.Flush();
                return bufferWriter.ToOwnedText();
            }
            finally
            {
                WriterCache.Return(writer, bufferWriter);
            }
        }

        /// <summary>
        /// Serializes <paramref name="value"/> as UTF-8 JSON into a <see cref="Text"/>.
        /// One allocation for the exact-size <c>byte[]</c>; equivalent to
        /// <c>Text.FromUtf8(JsonSerializer.SerializeToUtf8Bytes(value, options))</c> but avoids the internal copy.
        /// </summary>
        public static Text SerializeToUtf8Text<T>(T value, JsonSerializerOptions? options = null)
            => Text.FromUtf8(JsonSerializer.SerializeToUtf8Bytes(value, options));
    }
}
