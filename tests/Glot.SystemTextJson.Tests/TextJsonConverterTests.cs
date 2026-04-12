using System.Buffers;
using System.Text;
using System.Text.Json;

namespace Glot.SystemTextJson.Tests;

public class TextJsonConverterTests
{
    readonly JsonSerializerOptions _options = new()
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
        var json = JsonSerializer.Serialize(text, _options);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
    }

    [Test]
    public async Task Serialize_Utf8_WritesString()
    {
        // Arrange
        const string value = "hello";
        var text = Text.FromUtf8(Encoding.UTF8.GetBytes(value));

        // Act
        var json = JsonSerializer.Serialize(text, _options);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
    }

    [Test]
    public async Task Serialize_Utf32_WritesString()
    {
        // Arrange
        const string value = "hello";
        var codePoints = new int[] { 'h', 'e', 'l', 'l', 'o' };
        var text = Text.FromUtf32(codePoints);

        // Act
        var json = JsonSerializer.Serialize(text, _options);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
    }

    [Test]
    public async Task Serialize_Utf32Unicode_WritesString()
    {
        // Arrange — emoji U+1F600
        var codePoints = new int[] { 0x1F600 };
        var text = Text.FromUtf32(codePoints);

        // Act
        var json = JsonSerializer.Serialize(text, _options);
        var deserialized = JsonSerializer.Deserialize<Text>(json, _options);

        // Assert
        await Assert.That(deserialized).IsEqualTo(text);
    }

    [Test]
    public async Task Serialize_LargeUtf32_WritesString()
    {
        // Arrange — >128 runes to exceed stackalloc threshold (512 bytes) in WriteTranscoded
        var codePoints = new int[200];
        for (var i = 0; i < codePoints.Length; i++)
        {
            codePoints[i] = 'A' + (i % 26);
        }

        var text = Text.FromUtf32(codePoints);

        // Act
        var json = JsonSerializer.Serialize(text, _options);
        var deserialized = JsonSerializer.Deserialize<Text>(json, _options);

        // Assert
        await Assert.That(deserialized).IsEqualTo(text);
    }

    [Test]
    public async Task Deserialize_String_ReturnsText()
    {
        // Arrange
        const string json = "\"hello\"";

        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _options);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("hello");
    }

    [Test]
    public async Task Deserialize_EscapedString_ReturnsText()
    {
        // Arrange — JSON with escape sequences
        const string json = "\"hello\\nworld\\t!\"";

        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _options);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("hello\nworld\t!");
    }

    [Test]
    public async Task Deserialize_EscapedUnicode_ReturnsText()
    {
        // Arrange — JSON unicode escape \u4E16 = 世
        const string json = "\"\\u4E16\\u754C\"";

        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _options);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("世界");
    }

    [Test]
    public async Task Deserialize_EscapedSurrogatePair_ReturnsText()
    {
        // Arrange — JSON surrogate pair for U+1F600 😀
        const string json = "\"\\uD83D\\uDE00\"";

        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _options);
        var expected = Text.From("\U0001F600");

        // Assert
        await Assert.That(text).IsEqualTo(expected);
    }

    [Test]
    public async Task Deserialize_LargeEscapedString_ReturnsText()
    {
        // Arrange — escaped string > 512 bytes to hit ArrayPool path
        var sb = new StringBuilder();
        sb.Append('"');
        for (var i = 0; i < 300; i++)
        {
            sb.Append("ab\\n");
        }
        sb.Append('"');
        var json = sb.ToString();

        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _options);

        // Assert
        await Assert.That(text.RuneLength).IsEqualTo(900);
    }

    [Test]
    public async Task RoundTrip_NonEmpty_PreservesValue()
    {
        // Arrange
        const string value = "hello";
        Text original = Text.From(value);

        // Act
        var json = JsonSerializer.Serialize(original, _options);
        var deserialized = JsonSerializer.Deserialize<Text>(json, _options);

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
        var json = JsonSerializer.Serialize(original, _options);
        var deserialized = JsonSerializer.Deserialize<Text>(json, _options);

        // Assert
        await Assert.That(deserialized).IsEqualTo(original);
    }

    [Test]
    public async Task Serialize_Empty_WritesEmptyString()
    {
        // Arrange
        var text = Text.Empty;

        // Act
        var json = JsonSerializer.Serialize(text, _options);

        // Assert
        await Assert.That(json).IsEqualTo("\"\"");
    }

    [Test]
    public async Task Deserialize_Null_ReturnsEmpty()
    {
        // Arrange
        const string json = "null";

        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _options);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Deserialize_EmptyString_ReturnsEmpty()
    {
        // Arrange
        const string json = "\"\"";

        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _options);

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
        var json = JsonSerializer.Serialize(original, _options);
        var deserialized = JsonSerializer.Deserialize<Text>(json, _options);

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
        var json = JsonSerializer.Serialize(container, _options);

        // Assert
        return Verify(json);
    }

    [Test]
    public Task Deserialize_InContainer_ReadsCorrectly()
    {
        // Arrange
        const string json = """{"Name":"world","Label":"test"}""";

        // Act
        var container = JsonSerializer.Deserialize<TextContainer>(json, _options);

        // Assert
        return Verify(container);
    }

    [Test]
    public async Task Deserialize_MultiSegment_ReturnsText()
    {
        // Arrange — split JSON across segments so HasValueSequence is true
        var json = Encoding.UTF8.GetBytes("\"hello world\"");
        var first = new MemorySegment<byte>(json.AsMemory(0, 4));
        var last = first.Append(json.AsMemory(4));
        var sequence = new ReadOnlySequence<byte>(first, 0, last, last.Memory.Length);

        // Act
        var reader = new Utf8JsonReader(sequence);
        reader.Read();
        var text = JsonSerializer.Deserialize<Text>(ref reader, _options);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("hello world");
    }

    [Test]
    public async Task Deserialize_MultiSegmentEscaped_ReturnsText()
    {
        // Arrange — escaped value spanning multiple segments
        var json = Encoding.UTF8.GetBytes("\"hello\\nworld\"");
        var first = new MemorySegment<byte>(json.AsMemory(0, 5));
        var last = first.Append(json.AsMemory(5));
        var sequence = new ReadOnlySequence<byte>(first, 0, last, last.Memory.Length);

        // Act
        var reader = new Utf8JsonReader(sequence);
        reader.Read();
        var text = JsonSerializer.Deserialize<Text>(ref reader, _options);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("hello\nworld");
    }

    sealed class TextContainer
    {
        public Text Name { get; set; }
        public Text Label { get; set; }
    }

    sealed class MemorySegment<T> : ReadOnlySequenceSegment<T>
    {
        public MemorySegment(ReadOnlyMemory<T> memory)
        {
            Memory = memory;
        }

        public MemorySegment<T> Append(ReadOnlyMemory<T> next)
        {
            var segment = new MemorySegment<T>(next) { RunningIndex = RunningIndex + Memory.Length };
            Next = segment;
            return segment;
        }
    }
}
