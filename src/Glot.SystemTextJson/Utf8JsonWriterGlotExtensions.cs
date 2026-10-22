using System.Buffers;
using System.Text.Json;

namespace Glot.SystemTextJson;

/// <summary>
/// Extends <see cref="Utf8JsonWriter"/> with writing a <see cref="Text"/>. No <see cref="JsonSerializerOptions"/> are needed.
/// </summary>
public static class Utf8JsonWriterGlotExtensions
{
    const int StackAllocThreshold = 512;

    /// <summary>
    /// Writes <paramref name="value"/> as a JSON string value, from the text's own encoding.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="value">The text to write.</param>
    /// <remarks>
    /// Like <see cref="Utf8JsonWriter.WriteStringValue(ReadOnlySpan{byte})"/>. UTF-8 and UTF-16 texts are written directly.
    /// Other encodings are transcoded through a stack- or pool-allocated buffer.
    /// </remarks>
    /// <example>
    /// <code>
    /// writer.WriteTextValue(text);
    /// </code>
    /// </example>
    public static void WriteTextValue(this Utf8JsonWriter writer, Text value)
    {
        switch (value.Encoding)
        {
            case TextEncoding.Utf8:
                writer.WriteStringValue(value.Bytes);
                break;
            case TextEncoding.Utf16:
                writer.WriteStringValue(value.Chars);
                break;
            default:
                WriteTranscoded(writer, value, asPropertyName: false);
                break;
        }
    }

    /// <summary>
    /// Writes <paramref name="value"/> as a JSON property name, from the text's own encoding.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="value">The property name to write.</param>
    /// <remarks>
    /// Like <see cref="Utf8JsonWriter.WritePropertyName(ReadOnlySpan{byte})"/>. UTF-8 and UTF-16 texts are written directly.
    /// Other encodings are transcoded through a stack- or pool-allocated buffer.
    /// </remarks>
    /// <example>
    /// <code>
    /// writer.WriteTextPropertyName(name);
    /// </code>
    /// </example>
    public static void WriteTextPropertyName(this Utf8JsonWriter writer, Text value)
    {
        switch (value.Encoding)
        {
            case TextEncoding.Utf8:
                writer.WritePropertyName(value.Bytes);
                break;
            case TextEncoding.Utf16:
                writer.WritePropertyName(value.Chars);
                break;
            default:
                WriteTranscoded(writer, value, asPropertyName: true);
                break;
        }
    }

    static void WriteTranscoded(Utf8JsonWriter writer, Text value, bool asPropertyName)
    {
        var maxBytes = value.RuneLength * 4;
        byte[]? rented = null;
        var buffer = maxBytes <= StackAllocThreshold
            ? stackalloc byte[StackAllocThreshold]
            : (rented = ArrayPool<byte>.Shared.Rent(maxBytes));
        try
        {
            var written = value.EncodeToUtf8(buffer);
            if (asPropertyName)
            {
                writer.WritePropertyName(buffer.Slice(0, written));
            }
            else
            {
                writer.WriteStringValue(buffer.Slice(0, written));
            }
        }
        finally
        {
            if (rented is not null)
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }
    }
}
