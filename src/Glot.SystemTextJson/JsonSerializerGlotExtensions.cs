using System.Diagnostics.CodeAnalysis;
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
        /// The caller must dispose the returned value.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <param name="options">The serializer options, or <c>null</c> to use defaults.</param>
        /// <returns>An <see cref="OwnedText"/> containing the UTF-8 JSON representation.</returns>
        /// <remarks>Uses a thread-local cached writer and buffer to avoid allocation on the serialization path. The caller must dispose the returned <see cref="OwnedText"/>.</remarks>
        /// <example>
        /// <code>
        /// using var json = JsonSerializer.SerializeToUtf8OwnedText(new { Name = "test" });
        /// return GlotResults.Json(json);
        /// </code>
        /// </example>
        [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext.")]
        [RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
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
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <param name="options">The serializer options, or <c>null</c> to use defaults.</param>
        /// <returns>A <see cref="Text"/> containing the UTF-8 JSON representation.</returns>
        /// <remarks>Allocates a single exact-size <c>byte[]</c> for the result.</remarks>
        /// <example>
        /// <code>
        /// Text json = JsonSerializer.SerializeToUtf8Text(data);
        /// </code>
        /// </example>
        [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext.")]
        [RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
        public static Text SerializeToUtf8Text<T>(T value, JsonSerializerOptions? options = null)
            => Text.FromUtf8(JsonSerializer.SerializeToUtf8Bytes(value, options));
    }
}
