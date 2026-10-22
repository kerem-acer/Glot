using System.Text.Json;
using System.Text.Json.Serialization;

namespace Glot.SystemTextJson;

/// <summary>
/// Converts <see cref="Text"/> to and from a JSON string value, and to and from a property name when used as a dictionary key.
/// </summary>
/// <remarks>
/// <para>On read, decodes JSON UTF-8 bytes directly into a <see cref="Text"/> without creating an intermediate string.
/// By default, <c>null</c> reads as an empty <see cref="Text"/> and numbers and booleans read as their raw JSON text;
/// the constructor parameters turn either off. Objects and arrays always throw a <see cref="JsonException"/>.</para>
/// <para>On write, UTF-8 and UTF-16 texts are written directly. Other encodings are transcoded through a stack- or pool-allocated buffer.</para>
/// <para>Strings, property names and values are read and written by <see cref="Utf8JsonReaderGlotExtensions.GetText"/>,
/// <see cref="Utf8JsonWriterGlotExtensions.WriteTextValue"/> and <see cref="Utf8JsonWriterGlotExtensions.WriteTextPropertyName"/>,
/// which need no <see cref="JsonSerializerOptions"/>.</para>
/// </remarks>
public sealed class TextJsonConverter : JsonConverter<Text>
{
    readonly bool _allowNull;
    readonly bool _allowNumbersAndBooleans;

    /// <summary>
    /// Creates a converter that reads <c>null</c> as an empty <see cref="Text"/> and numbers and booleans as their raw JSON text.
    /// </summary>
    public TextJsonConverter()
        : this(allowNull: true, allowNumbersAndBooleans: true)
    {
    }

    /// <summary>
    /// Creates a converter that reads JSON strings, and optionally <c>null</c>, numbers and booleans.
    /// </summary>
    /// <param name="allowNull">
    /// <see langword="true"/> to read <c>null</c> as an empty <see cref="Text"/>; <see langword="false"/> to throw a <see cref="JsonException"/>.
    /// A <c>null</c> for <see cref="Nullable{T}"/> of <see cref="Text"/> reads as <see langword="null"/> either way.
    /// </param>
    /// <param name="allowNumbersAndBooleans">
    /// <see langword="true"/> to read a number or boolean as its raw JSON text, so <c>1.2</c> reads as <c>"1.2"</c>;
    /// <see langword="false"/> to throw a <see cref="JsonException"/>.
    /// </param>
    public TextJsonConverter(bool allowNull = true, bool allowNumbersAndBooleans = true)
    {
        _allowNull = allowNull;
        _allowNumbersAndBooleans = allowNumbersAndBooleans;
    }

    /// <inheritdoc/>
    public override Text Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return reader.GetText();
            case JsonTokenType.Number or JsonTokenType.True or JsonTokenType.False when _allowNumbersAndBooleans:
                // A number or boolean is never escaped, so its raw JSON is the text.
                return reader.HasValueSequence
                    ? Text.FromUtf8(reader.ValueSequence)
                    : Text.FromUtf8(reader.ValueSpan);
            case JsonTokenType.Null when _allowNull:
                return default;
            default:
                throw ConverterExceptions.NotAString(reader.TokenType, typeof(Text));
        }
    }

    /// <inheritdoc/>
    public override Text ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetText();

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Text value, JsonSerializerOptions options)
        => writer.WriteTextValue(value);

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, Text value, JsonSerializerOptions options)
        => writer.WriteTextPropertyName(value);
}
