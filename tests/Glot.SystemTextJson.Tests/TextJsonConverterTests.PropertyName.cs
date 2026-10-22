using System.Text;
using System.Text.Json;

namespace Glot.SystemTextJson.Tests;

public partial class TextJsonConverterTests
{
    [Test]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf32)]
    public async Task Serialize_DictionaryKey_WritesPropertyName(TextEncoding encoding)
    {
        // Arrange
        const string key = "tr";
        const string value = "Merhaba";
        var dictionary = new Dictionary<Text, Text> { [Encode(key, encoding)] = Text.From(value) };

        // Act
        var json = JsonSerializer.Serialize(dictionary, _options);

        // Assert
        await Assert.That(json).IsEqualTo($$"""{"{{key}}":"{{value}}"}""");
    }

    [Test]
    public async Task Serialize_LongUtf32DictionaryKey_WritesPropertyName()
    {
        // Arrange — long enough that transcoding rents a buffer
        var key = new string('k', 200);
        var dictionary = new Dictionary<Text, int> { [Encode(key, TextEncoding.Utf32)] = 1 };

        // Act
        var json = JsonSerializer.Serialize(dictionary, _options);

        // Assert
        await Assert.That(json).IsEqualTo($$"""{"{{key}}":1}""");
    }

    [Test]
    public async Task Serialize_DictionaryKeyNeedingEscape_EscapesPropertyName()
    {
        // Arrange
        var dictionary = new Dictionary<Text, int> { [Text.From("a\"b")] = 1 };

        // Act
        var json = JsonSerializer.Serialize(dictionary, _options);

        // Assert
        await Assert.That(json).IsEqualTo("""{"a\u0022b":1}""");
    }

    [Test]
    public async Task Deserialize_DictionaryKey_ReadsPropertyName()
    {
        // Arrange
        const string key = "tr";
        const string value = "Merhaba";
        const string json = $$"""{"{{key}}":"{{value}}"}""";

        // Act
        var dictionary = JsonSerializer.Deserialize<Dictionary<Text, Text>>(json, _options);

        // Assert
        await Assert.That(dictionary![Text.From(key)].ToString()).IsEqualTo(value);
    }

    [Test]
    public async Task Deserialize_ReadOnlyDictionaryKey_ReadsPropertyName()
    {
        // Arrange
        const string key = "tr";
        const int value = 1;
        var json = $$"""{"{{key}}":{{value}}}""";

        // Act
        var dictionary = JsonSerializer.Deserialize<IReadOnlyDictionary<Text, int>>(json, _options);

        // Assert
        await Assert.That(dictionary![Text.From(key)]).IsEqualTo(value);
    }

    [Test]
    public async Task Deserialize_EscapedDictionaryKey_UnescapesPropertyName()
    {
        // Arrange
        const string json = """{"a\u0022b":1}""";

        // Act
        var dictionary = JsonSerializer.Deserialize<Dictionary<Text, int>>(json, _options);

        // Assert
        await Assert.That(dictionary!.ContainsKey(Text.From("a\"b"))).IsTrue();
    }

    static Text Encode(string value, TextEncoding encoding) => encoding switch
    {
        TextEncoding.Utf8 => Text.FromUtf8(Encoding.UTF8.GetBytes(value)),
        TextEncoding.Utf16 => Text.From(value),
        _ => Text.FromBytes(Encoding.UTF32.GetBytes(value), TextEncoding.Utf32),
    };
}
