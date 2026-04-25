using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Glot.SystemTextJson;

/// <summary>
/// Converts <see cref="Text"/> to and from a JSON string value.
/// </summary>
/// <remarks>
/// <para>On read, decodes JSON UTF-8 bytes directly into a <see cref="Text"/> without creating an intermediate string.</para>
/// <para>On write, UTF-8 and UTF-16 texts are written directly. Other encodings are transcoded through a stack- or pool-allocated buffer.</para>
/// </remarks>
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
            return Text.FromUtf8(buffer.Slice(0, written));
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
            writer.WriteStringValue(buffer.Slice(0, written));
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
