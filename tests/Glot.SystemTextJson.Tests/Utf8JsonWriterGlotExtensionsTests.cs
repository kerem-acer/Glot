using System.Buffers;
using System.Text;
using System.Text.Json;

namespace Glot.SystemTextJson.Tests;

public class Utf8JsonWriterGlotExtensionsTests
{
    [Test]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf32)]
    public async Task WriteTextValue_Encoding_WritesString(TextEncoding encoding)
    {
        // Arrange
        const string value = "hello";
        var text = TestText.Encode(value, encoding);

        // Act
        var json = Write(writer => writer.WriteTextValue(text));

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
    }

    [Test]
    public async Task WriteTextValue_LongUtf32_WritesString()
    {
        // Arrange — long enough that transcoding rents a buffer
        var value = new string('v', 200);
        var text = TestText.Encode(value, TextEncoding.Utf32);

        // Act
        var json = Write(writer => writer.WriteTextValue(text));

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
    }

    [Test]
    public async Task WriteTextValue_NeedsEscape_EscapesLikeSerializer()
    {
        // Arrange
        const string value = "say \"hi\"";

        // Act
        var json = Write(writer => writer.WriteTextValue(Text.From(value)));

        // Assert
        await Assert.That(json).IsEqualTo(JsonSerializer.Serialize(value));
    }

    [Test]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf32)]
    public async Task WriteTextPropertyName_Encoding_WritesName(TextEncoding encoding)
    {
        // Arrange
        const string name = "tr";
        var text = TestText.Encode(name, encoding);

        // Act
        var json = Write(writer =>
        {
            writer.WriteStartObject();
            writer.WriteTextPropertyName(text);
            writer.WriteNumberValue(1);
            writer.WriteEndObject();
        });

        // Assert
        await Assert.That(json).IsEqualTo($$"""{"{{name}}":1}""");
    }

    [Test]
    public async Task WriteTextPropertyName_LongUtf32_WritesName()
    {
        // Arrange — long enough that transcoding rents a buffer
        var name = new string('k', 200);
        var text = TestText.Encode(name, TextEncoding.Utf32);

        // Act
        var json = Write(writer =>
        {
            writer.WriteStartObject();
            writer.WriteTextPropertyName(text);
            writer.WriteNumberValue(1);
            writer.WriteEndObject();
        });

        // Assert
        await Assert.That(json).IsEqualTo($$"""{"{{name}}":1}""");
    }

    static string Write(Action<Utf8JsonWriter> write)
    {
        var buffer = new ArrayBufferWriter<byte>();
        using (var writer = new Utf8JsonWriter(buffer))
        {
            write(writer);
        }

        return Encoding.UTF8.GetString(buffer.WrittenSpan);
    }
}
