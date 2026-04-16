using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Glot.SystemTextJson;

/// <summary>
/// Converts <see cref="Text"/> to and from a JSON string value.
/// Zero-alloc for UTF-8 and UTF-16 encoded text on write.
/// Zero-string on read — decodes JSON UTF-8 bytes directly.
/// </summary>
public sealed class TextJsonConverter : JsonConverter<Text>
{
    const int StackAllocThreshold = 512;

    /// <inheritdoc/>
    public override Text Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return default;
        }

        if (!reader.ValueIsEscaped)
        {
            return reader.HasValueSequence
                ? Text.FromUtf8(reader.ValueSequence)
                : Text.FromUtf8(reader.ValueSpan);
        }

        long length = reader.HasValueSequence
            ? reader.ValueSequence.Length
            : reader.ValueSpan.Length;
        byte[]? rented = null;
        var buffer = length <= StackAllocThreshold
            ? stackalloc byte[StackAllocThreshold]
            : (rented = ArrayPool<byte>.Shared.Rent((int)length));
        try
        {
            var written = reader.CopyString(buffer);
            return Text.FromUtf8(buffer[..written]);
        }
        finally
        {
            if (rented is not null)
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Text value, JsonSerializerOptions options)
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
                WriteTranscoded(writer, value);
                break;
        }
    }

    static void WriteTranscoded(Utf8JsonWriter writer, Text value)
    {
        var maxBytes = value.RuneLength * 4;
        byte[]? rented = null;
        var buffer = maxBytes <= StackAllocThreshold
            ? stackalloc byte[StackAllocThreshold]
            : (rented = ArrayPool<byte>.Shared.Rent(maxBytes));
        try
        {
            var written = value.EncodeToUtf8(buffer);
            writer.WriteStringValue(buffer[..written]);
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
