using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Glot.SystemTextJson;

/// <summary>
/// Converts <see cref="OwnedText"/> to and from a JSON string value.
/// The caller is responsible for disposing deserialized <see cref="OwnedText"/> values.
/// </summary>
/// <remarks>
/// <para>On read, decodes JSON UTF-8 bytes directly into a pooled buffer. The caller must dispose deserialized <see cref="OwnedText"/> values.</para>
/// <para>On write, UTF-8 and UTF-16 texts are written directly. Other encodings are transcoded through a stack- or pool-allocated buffer.</para>
/// </remarks>
public sealed class OwnedTextJsonConverter : JsonConverter<OwnedText>
{
    const int StackAllocThreshold = 512;

    /// <inheritdoc/>
    public override OwnedText? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (!reader.ValueIsEscaped)
        {
            return reader.HasValueSequence ? OwnedText.FromUtf8(reader.ValueSequence) : OwnedText.FromUtf8(reader.ValueSpan);
        }

        {
            long length = reader.HasValueSequence ? reader.ValueSequence.Length : reader.ValueSpan.Length;
            var buffer = ArrayPool<byte>.Shared.Rent((int)length);
            var written = reader.CopyString(buffer);
            return OwnedText.Create(buffer, written, TextEncoding.Utf8);
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, OwnedText value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        var text = value.Text;
        switch (text.Encoding)
        {
            case TextEncoding.Utf8:
                writer.WriteStringValue(text.Bytes);
                break;

            case TextEncoding.Utf16:
                writer.WriteStringValue(text.Chars);
                break;

            default:
                WriteTranscoded(writer, text);
                break;
        }
    }

    static void WriteTranscoded(Utf8JsonWriter writer, Text text)
    {
        var maxBytes = text.RuneLength * 4;
        byte[]? rented = null;
        var buffer = maxBytes <= StackAllocThreshold ? stackalloc byte[StackAllocThreshold] : (rented = ArrayPool<byte>.Shared.Rent(maxBytes));
        try
        {
            var written = text.EncodeToUtf8(buffer);
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
