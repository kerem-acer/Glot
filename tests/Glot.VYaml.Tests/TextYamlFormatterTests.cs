using System.Text;
using VYaml.Serialization;

namespace Glot.VYaml.Tests;

public class TextYamlFormatterTests
{
    readonly YamlSerializerOptions _options = new()
    {
        Resolver = CompositeResolver.Create(
            [TextYamlFormatter.Instance],
            [StandardResolver.Instance]),
    };

    [Test]
    public async Task Serialize_NonEmpty_WritesString()
    {
        // Arrange
        const string value = "hello";
        Text text = Text.From(value);

        // Act
        var yaml = YamlSerializer.SerializeToString(text, _options);

        // Assert
        await Assert.That(yaml.Trim()).IsEqualTo(value);
    }

    [Test]
    public async Task Serialize_Utf8_WritesScalar()
    {
        // Arrange
        const string value = "hello";
        var text = Text.FromUtf8(Encoding.UTF8.GetBytes(value));

        // Act
        var yaml = YamlSerializer.SerializeToString(text, _options);

        // Assert
        await Assert.That(yaml.Trim()).IsEqualTo(value);
    }

    [Test]
    public async Task Serialize_Utf32_WritesScalar()
    {
        // Arrange
        var codePoints = new int[] { 'h', 'e', 'l', 'l', 'o' };
        var text = Text.FromUtf32(codePoints);

        // Act
        var yaml = YamlSerializer.SerializeToString(text, _options);

        // Assert
        await Assert.That(yaml.Trim()).IsEqualTo("hello");
    }

    [Test]
    public async Task Serialize_Utf32Unicode_WritesScalar()
    {
        // Arrange — emoji U+1F600
        var codePoints = new int[] { 0x1F600 };
        var text = Text.FromUtf32(codePoints);
        var expected = Text.From("\U0001F600");

        // Act
        var yaml = YamlSerializer.SerializeToString(text, _options);
        var bytes = Encoding.UTF8.GetBytes(yaml);
        var deserialized = YamlSerializer.Deserialize<Text>(bytes, _options);

        // Assert
        await Assert.That(deserialized).IsEqualTo(expected);
    }

    [Test]
    public async Task Deserialize_String_ReturnsText()
    {
        // Arrange
        const string yaml = "hello";
        var bytes = Encoding.UTF8.GetBytes(yaml);

        // Act
        var text = YamlSerializer.Deserialize<Text>(bytes, _options);

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
        var yaml = YamlSerializer.SerializeToString(original, _options);
        var bytes = Encoding.UTF8.GetBytes(yaml);
        var deserialized = YamlSerializer.Deserialize<Text>(bytes, _options);

        // Assert
        await Assert.That(deserialized).IsEqualTo(original);
    }

    [Test]
    public async Task RoundTrip_Utf8_PreservesValue()
    {
        // Arrange
        const string value = "hello";
        var original = Text.FromUtf8(Encoding.UTF8.GetBytes(value));

        // Act
        var yaml = YamlSerializer.SerializeToString(original, _options);
        var bytes = Encoding.UTF8.GetBytes(yaml);
        var deserialized = YamlSerializer.Deserialize<Text>(bytes, _options);

        // Assert
        await Assert.That(deserialized).IsEqualTo(original);
    }

    [Test]
    public async Task Serialize_Empty_WritesEmptyString()
    {
        // Arrange
        var text = Text.Empty;

        // Act
        var yaml = YamlSerializer.SerializeToString(text, _options);

        // Assert
        await Assert.That(yaml.Trim()).IsEqualTo("\"\"");
    }

    [Test]
    public async Task Deserialize_Null_ReturnsEmpty()
    {
        // Arrange
        const string yaml = "null";
        var bytes = Encoding.UTF8.GetBytes(yaml);

        // Act
        var text = YamlSerializer.Deserialize<Text>(bytes, _options);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Deserialize_EmptyString_ReturnsEmpty()
    {
        // Arrange
        const string yaml = "''";
        var bytes = Encoding.UTF8.GetBytes(yaml);

        // Act
        var text = YamlSerializer.Deserialize<Text>(bytes, _options);

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
        var yaml = YamlSerializer.SerializeToString(original, _options);
        var bytes = Encoding.UTF8.GetBytes(yaml);
        var deserialized = YamlSerializer.Deserialize<Text>(bytes, _options);

        // Assert
        await Assert.That(deserialized).IsEqualTo(original);
    }
}
