using System.Text;
using VYaml.Serialization;

namespace Glot.VYaml.Tests;

public class OwnedTextYamlFormatterTests
{
    readonly YamlSerializerOptions _options = new()
    {
        Resolver = CompositeResolver.Create(
            [OwnedTextYamlFormatter.Instance],
            [StandardResolver.Instance]),
    };

    [Test]
    public async Task Serialize_NonEmpty_WritesString()
    {
        // Arrange
        const string value = "hello";
        using var owned = OwnedText.FromChars(value.AsSpan())!;

        // Act
        var yaml = YamlSerializer.SerializeToString(owned, _options);

        // Assert
        await Assert.That(yaml.Trim()).IsEqualTo(value);
    }

    [Test]
    public async Task Serialize_Utf8_WritesScalar()
    {
        // Arrange
        const string value = "hello";
        using var owned = OwnedText.FromUtf8(Encoding.UTF8.GetBytes(value))!;

        // Act
        var yaml = YamlSerializer.SerializeToString(owned, _options);

        // Assert
        await Assert.That(yaml.Trim()).IsEqualTo(value);
    }

    [Test]
    public async Task Serialize_Utf32_WritesScalar()
    {
        // Arrange
        var codePoints = new int[] { 'h', 'e', 'l', 'l', 'o' };
        using var owned = OwnedText.FromUtf32(codePoints)!;

        // Act
        var yaml = YamlSerializer.SerializeToString(owned, _options);

        // Assert
        await Assert.That(yaml.Trim()).IsEqualTo("hello");
    }

    [Test]
    public async Task Deserialize_String_ReturnsValue()
    {
        // Arrange
        const string yaml = "hello";
        var bytes = Encoding.UTF8.GetBytes(yaml);

        // Act
        using var owned = YamlSerializer.Deserialize<OwnedText>(bytes, _options)!;

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
        var yaml = YamlSerializer.SerializeToString(original, _options);
        var bytes = Encoding.UTF8.GetBytes(yaml);
        using var deserialized = YamlSerializer.Deserialize<OwnedText>(bytes, _options)!;

        // Assert
        await Assert.That(deserialized.Text).IsEqualTo(originalText);
    }

    [Test]
    public async Task Serialize_Null_WritesNull()
    {
        // Arrange
        OwnedText? owned = null;

        // Act
        var yaml = YamlSerializer.SerializeToString(owned, _options);

        // Assert
        await Assert.That(yaml.Trim()).IsEqualTo("null");
    }

    [Test]
    public async Task Deserialize_Null_ReturnsNull()
    {
        // Arrange
        const string yaml = "null";
        var bytes = Encoding.UTF8.GetBytes(yaml);

        // Act
        var owned = YamlSerializer.Deserialize<OwnedText>(bytes, _options);

        // Assert
        await Assert.That(owned).IsNull();
    }

    [Test]
    public async Task Deserialize_EmptyString_ReturnsNull()
    {
        // Arrange
        const string yaml = "''";
        var bytes = Encoding.UTF8.GetBytes(yaml);

        // Act
        var owned = YamlSerializer.Deserialize<OwnedText>(bytes, _options);

        // Assert
        await Assert.That(owned).IsNull();
    }

    [Test]
    public async Task RoundTrip_Unicode_PreservesValue()
    {
        // Arrange
        const string value = "Hello \U0001F600 \u4E16\u754C";
        using var original = OwnedText.FromChars(value.AsSpan())!;
        var originalText = original.Text;

        // Act
        var yaml = YamlSerializer.SerializeToString(original, _options);
        var bytes = Encoding.UTF8.GetBytes(yaml);
        using var deserialized = YamlSerializer.Deserialize<OwnedText>(bytes, _options)!;

        // Assert
        await Assert.That(deserialized.Text).IsEqualTo(originalText);
    }
}
