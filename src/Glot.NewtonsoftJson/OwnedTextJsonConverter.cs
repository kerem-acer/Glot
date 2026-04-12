using Newtonsoft.Json;

namespace Glot.NewtonsoftJson;

/// <summary>
/// Converts <see cref="OwnedText"/> to and from a JSON string value.
/// The caller is responsible for disposing deserialized <see cref="OwnedText"/> values.
/// </summary>
public sealed class OwnedTextJsonConverter : JsonConverter<OwnedText>
{
    /// <inheritdoc/>
    public override OwnedText ReadJson(JsonReader reader, Type objectType, OwnedText existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return default;
        }

        return OwnedText.FromChars(((string)reader.Value!).AsSpan());
    }

    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, OwnedText value, JsonSerializer serializer)
        => writer.WriteValue(value.Text.ToString());
}
