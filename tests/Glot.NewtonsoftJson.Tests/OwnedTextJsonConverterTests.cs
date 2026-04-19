using Newtonsoft.Json;

namespace Glot.NewtonsoftJson.Tests;

public partial class OwnedTextJsonConverterTests
{
    readonly JsonSerializerSettings _settings = new()
    {
        Converters = { new OwnedTextJsonConverter() },
    };

    [Test]
    public async Task Serialize_NonEmpty_WritesString()
    {
        // Arrange
        const string value = "hello";
        using var owned = OwnedText.FromChars(value.AsSpan())!;

        // Act
        var json = JsonConvert.SerializeObject(owned, _settings);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
    }

    [Test]
    public async Task Deserialize_String_ReturnsValue()
    {
        // Arrange
        const string json = "\"hello\"";

        // Act
        using var owned = JsonConvert.DeserializeObject<OwnedText>(json, _settings)!;

        // Assert
        await Assert.That(owned.Text.ToString()).IsEqualTo("hello");
    }

    [Test]
    public async Task RoundTrip_NonEmpty_PreservesValue()
    {
        // Arrange
        const string value = "hello";
        using var original = OwnedText.FromChars(value.AsSpan())!;
        var originalText = original.Text;

        // Act
        var json = JsonConvert.SerializeObject(original, _settings);
        using var deserialized = JsonConvert.DeserializeObject<OwnedText>(json, _settings)!;

        // Assert
        await Assert.That(deserialized.Text).IsEqualTo(originalText);
    }

    [Test]
    public async Task Serialize_Null_WritesNull()
    {
        // Arrange
        OwnedText? owned = null;

        // Act
        var json = JsonConvert.SerializeObject(owned, _settings);

        // Assert
        await Assert.That(json).IsEqualTo("null");
    }

    [Test]
    public async Task Deserialize_Null_ReturnsNull()
    {
        // Arrange
        const string json = "null";

        // Act
        var owned = JsonConvert.DeserializeObject<OwnedText>(json, _settings);

        // Assert
        await Assert.That(owned).IsNull();
    }

    [Test]
    public async Task Deserialize_EmptyString_ReturnsEmpty()
    {
        // Arrange
        const string json = "\"\"";

        // Act
        var owned = JsonConvert.DeserializeObject<OwnedText>(json, _settings);

        // Assert
        await Assert.That(owned!.IsEmpty).IsTrue();
    }

    [Test]
    public async Task RoundTrip_Unicode_PreservesValue()
    {
        // Arrange
        const string value = "Hello \U0001F600 \u4E16\u754C";
        using var original = OwnedText.FromChars(value.AsSpan())!;
        var originalText = original.Text;

        // Act
        var json = JsonConvert.SerializeObject(original, _settings);
        using var deserialized = JsonConvert.DeserializeObject<OwnedText>(json, _settings)!;

        // Assert
        await Assert.That(deserialized.Text).IsEqualTo(originalText);
    }
}
