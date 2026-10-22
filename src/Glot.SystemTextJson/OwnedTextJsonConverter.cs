using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Glot.SystemTextJson;

/// <summary>
/// Converts <see cref="OwnedText"/> to and from a JSON string value.
/// The caller is responsible for disposing deserialized <see cref="OwnedText"/> values.
/// </summary>
/// <remarks>
/// <para>On read, decodes JSON UTF-8 bytes directly into a pooled buffer. The caller must dispose deserialized <see cref="OwnedText"/> values.
/// A JSON <c>null</c> reads as <see langword="null"/>. By default, numbers and booleans read as their raw JSON text;
/// the constructor parameter turns that off. Objects and arrays always throw a <see cref="JsonException"/>.</para>
/// <para>On write, UTF-8 and UTF-16 texts are written directly. Other encodings are transcoded through a stack- or pool-allocated buffer.</para>
/// </remarks>
public sealed class OwnedTextJsonConverter : JsonConverter<OwnedText>
{
    readonly bool _allowNumbersAndBooleans;

    /// <summary>
    /// Creates a converter that reads numbers and booleans as their raw JSON text.
    /// </summary>
    public OwnedTextJsonConverter()
        : this(allowNumbersAndBooleans: true)
    {
    }

    /// <summary>
    /// Creates a converter that reads JSON strings and <c>null</c>, and optionally numbers and booleans.
    /// </summary>
    /// <param name="allowNumbersAndBooleans">
    /// <see langword="true"/> to read a number or boolean as its raw JSON text, so <c>1.2</c> reads as <c>"1.2"</c>;
    /// <see langword="false"/> to throw a <see cref="JsonException"/>.
    /// </param>
    public OwnedTextJsonConverter(bool allowNumbersAndBooleans = true) => _allowNumbersAndBooleans = allowNumbersAndBooleans;

    /// <inheritdoc/>
    public override OwnedText? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.String:
            case JsonTokenType.Number or JsonTokenType.True or JsonTokenType.False when _allowNumbersAndBooleans:
                break;
            default:
                throw ConverterExceptions.NotAString(reader.TokenType, typeof(OwnedText));
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

        writer.WriteTextValue(value.Text);
    }
}
