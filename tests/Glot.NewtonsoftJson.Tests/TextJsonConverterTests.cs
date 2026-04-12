using Newtonsoft.Json;

namespace Glot.NewtonsoftJson.Tests;

public class TextJsonConverterTests
{
    readonly JsonSerializerSettings _settings = new()
    {
        Converters = { new TextJsonConverter() },
    };

    [Test]
    public async Task Serialize_NonEmpty_WritesString()
    {
        // Arrange
        const string value = "hello";
        Text text = Text.From(value);

        // Act
        var json = JsonConvert.SerializeObject(text, _settings);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
    }

    [Test]
    public async Task Deserialize_String_ReturnsText()
    {
        // Arrange
        const string json = "\"hello\"";

        // Act
        var text = JsonConvert.DeserializeObject<Text>(json, _settings);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("hello");
    }

    [Test]
    public async Task RoundTrip_NonEmpty_PreservesValue()
    {
        // Arrange
        const string value = "hello";
        Text original = Text.From(value);

        // Act
        var json = JsonConvert.SerializeObject(original, _settings);
        var deserialized = JsonConvert.DeserializeObject<Text>(json, _settings);

        // Assert
        await Assert.That(deserialized).IsEqualTo(original);
    }

    [Test]
    public async Task Serialize_Empty_WritesEmptyString()
    {
        // Arrange
        var text = Text.Empty;

        // Act
        var json = JsonConvert.SerializeObject(text, _settings);

        // Assert
        await Assert.That(json).IsEqualTo("\"\"");
    }

    [Test]
    public async Task Deserialize_Null_ReturnsEmpty()
    {
        // Arrange
        const string json = "null";

        // Act
        var text = JsonConvert.DeserializeObject<Text>(json, _settings);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Deserialize_EmptyString_ReturnsEmpty()
    {
        // Arrange
        const string json = "\"\"";

        // Act
        var text = JsonConvert.DeserializeObject<Text>(json, _settings);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    [Test]
    public async Task RoundTrip_Unicode_PreservesValue()
    {
        // Arrange
        const string value = "Hello \U0001F600 \u4E16\u754C";
        Text original = Text.From(value);

        // Act
        var json = JsonConvert.SerializeObject(original, _settings);
        var deserialized = JsonConvert.DeserializeObject<Text>(json, _settings);

        // Assert
        await Assert.That(deserialized).IsEqualTo(original);
    }

    [Test]
    public Task Serialize_InContainer_WritesCorrectly()
    {
        // Arrange
        var container = new TextContainer
        {
            Name = Text.From("world"),
            Label = Text.From("test"),
        };

        // Act
        var json = JsonConvert.SerializeObject(container, _settings);

        // Assert
        return Verify(json);
    }

    [Test]
    public Task Deserialize_InContainer_ReadsCorrectly()
    {
        // Arrange
        const string json = """{"Name":"world","Label":"test"}""";

        // Act
        var container = JsonConvert.DeserializeObject<TextContainer>(json, _settings);

        // Assert
        return Verify(container);
    }

    sealed class TextContainer
    {
        public Text Name { get; set; }
        public Text Label { get; set; }
    }
}
