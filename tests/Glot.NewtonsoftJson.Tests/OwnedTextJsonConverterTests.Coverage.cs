using Newtonsoft.Json;

namespace Glot.NewtonsoftJson.Tests;

public partial class OwnedTextJsonConverterTests
{
    [Test]
    public async Task Serialize_Utf8OwnedText_WritesString()
    {
        // Arrange
        const string expected = "hello";
        using var owned = OwnedText.FromUtf8("hello"u8)!;

        // Act
        var json = JsonConvert.SerializeObject(owned, _settings);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{expected}\"");
    }

    [Test]
    public async Task Serialize_Utf32OwnedText_WritesString()
    {
        // Arrange
        const string expected = "hello";
        var codePoints = new int[] { 'h', 'e', 'l', 'l', 'o' };
        using var owned = OwnedText.FromUtf32(codePoints)!;

        // Act
        var json = JsonConvert.SerializeObject(owned, _settings);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{expected}\"");
    }

    [Test]
    public async Task RoundTrip_Utf8OwnedText_PreservesValue()
    {
        // Arrange
        const string expected = "hello";
        using var original = OwnedText.FromUtf8("hello"u8)!;

        // Act
        var json = JsonConvert.SerializeObject(original, _settings);
        using var deserialized = JsonConvert.DeserializeObject<OwnedText>(json, _settings)!;

        // Assert
        await Assert.That(deserialized.Text.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task RoundTrip_Utf32OwnedText_PreservesValue()
    {
        // Arrange
        var codePoints = new int[] { 0x1F600 };
        using var original = OwnedText.FromUtf32(codePoints)!;
        var originalStr = original.Text.ToString();

        // Act
        var json = JsonConvert.SerializeObject(original, _settings);
        using var deserialized = JsonConvert.DeserializeObject<OwnedText>(json, _settings)!;

        // Assert
        await Assert.That(deserialized.Text.ToString()).IsEqualTo(originalStr);
    }

    // Serialize empty OwnedText (non-null but empty) — exercises Text.ToString on empty

    [Test]
    public async Task Serialize_EmptyOwnedText_WritesEmptyString()
    {
        // Arrange
        var owned = OwnedText.FromChars([]);

        // Act
        var json = JsonConvert.SerializeObject(owned, _settings);

        // Assert
        await Assert.That(json).IsEqualTo("\"\"");
    }

    // Serialize UTF-16 backed OwnedText — exercises non-UTF-8 encoding path

    [Test]
    public async Task Serialize_Utf16OwnedText_WritesString()
    {
        // Arrange
        const string expected = "utf16 text";
        using var owned = OwnedText.FromChars(expected.AsSpan())!;

        // Act
        var json = JsonConvert.SerializeObject(owned, _settings);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{expected}\"");
    }

    // Deserialize and re-serialize round trip with multibyte

    [Test]
    public async Task RoundTrip_Multibyte_PreservesContent()
    {
        // Arrange
        const string value = "\u00e9\u00e8\u00ea";
        using var original = OwnedText.FromChars(value.AsSpan())!;

        // Act
        var json = JsonConvert.SerializeObject(original, _settings);
        using var deserialized = JsonConvert.DeserializeObject<OwnedText>(json, _settings)!;

        // Assert
        await Assert.That(deserialized.Text.ToString()).IsEqualTo(value);
    }

    // Serialize within a wrapper object — exercises converter in typical usage

    [Test]
    public async Task Serialize_InWrapperObject_WritesCorrectJson()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("nested"u8)!;
        var wrapper = new { name = owned };

        // Act
        var json = JsonConvert.SerializeObject(wrapper, _settings);

        // Assert
        await Assert.That(json).IsEqualTo("{\"name\":\"nested\"}");
    }
}
