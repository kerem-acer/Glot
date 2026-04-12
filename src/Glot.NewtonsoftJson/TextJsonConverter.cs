using Newtonsoft.Json;

namespace Glot.NewtonsoftJson;

/// <summary>
/// Converts <see cref="Text"/> to and from a JSON string value.
/// </summary>
public sealed class TextJsonConverter : JsonConverter<Text>
{
    /// <inheritdoc/>
    public override Text ReadJson(JsonReader reader, Type objectType, Text existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return Text.Empty;
        }

        return Text.From((string)reader.Value!);
    }

    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, Text value, JsonSerializer serializer)
        => writer.WriteValue(value.ToString());
}
