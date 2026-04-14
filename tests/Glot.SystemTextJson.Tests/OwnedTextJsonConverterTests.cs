using System.Buffers;
using System.Text;
using System.Text.Json;

namespace Glot.SystemTextJson.Tests;

public class OwnedTextJsonConverterTests
{
    readonly JsonSerializerOptions _options = new()
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
        var json = JsonSerializer.Serialize(owned, _options);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
    }

    [Test]
    public async Task Serialize_Utf8_WritesString()
    {
        // Arrange
        const string value = "hello";
        using var owned = OwnedText.FromUtf8(Encoding.UTF8.GetBytes(value))!;

        // Act
        var json = JsonSerializer.Serialize(owned, _options);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
    }

    [Test]
    public async Task Serialize_Utf32_WritesString()
    {
        // Arrange
        const string value = "hello";
        var codePoints = new int[] { 'h', 'e', 'l', 'l', 'o' };
        using var owned = OwnedText.FromUtf32(codePoints)!;

        // Act
        var json = JsonSerializer.Serialize(owned, _options);

        // Assert
        await Assert.That(json).IsEqualTo($"\"{value}\"");
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

        using var owned = OwnedText.FromUtf32(codePoints)!;

        // Act
        var json = JsonSerializer.Serialize(owned, _options);
        using var deserialized = JsonSerializer.Deserialize<OwnedText>(json, _options);

        // Assert
        await Assert.That(deserialized!.Text).IsEqualTo(owned.Text);
    }

    [Test]
    public async Task Deserialize_String_ReturnsValue()
    {
        // Arrange
        const string json = "\"hello\"";

        // Act
        using var owned = JsonSerializer.Deserialize<OwnedText>(json, _options);

        // Assert
        await Assert.That(owned!.Text.ToString()).IsEqualTo("hello");
    }

    [Test]
    public async Task Deserialize_EscapedString_ReturnsValue()
    {
        // Arrange
        const string json = "\"hello\\nworld\"";

        // Act
        using var owned = JsonSerializer.Deserialize<OwnedText>(json, _options);

        // Assert
        await Assert.That(owned!.Text.ToString()).IsEqualTo("hello\nworld");
    }

    [Test]
    public async Task Deserialize_LargeEscapedString_ReturnsValue()
    {
        // Arrange — escaped string > 512 bytes to hit ArrayPool rent path
        var sb = new StringBuilder();
        sb.Append('"');
        for (var i = 0; i < 300; i++)
        {
            sb.Append("ab\\n");
        }
        sb.Append('"');
        var json = sb.ToString();

        // Act
        using var owned = JsonSerializer.Deserialize<OwnedText>(json, _options);

        // Assert
        await Assert.That(owned!.Text.RuneLength).IsEqualTo(900);
    }

    [Test]
    public async Task RoundTrip_NonEmpty_PreservesValue()
    {
        // Arrange
        const string value = "hello";
        using var original = OwnedText.FromChars(value.AsSpan())!;
        var originalText = original.Text;

        // Act
        var json = JsonSerializer.Serialize(original, _options);
        using var deserialized = JsonSerializer.Deserialize<OwnedText>(json, _options);

        // Assert
        await Assert.That(deserialized!.Text).IsEqualTo(originalText);
    }

    [Test]
    public async Task Serialize_Null_WritesNull()
    {
        // Arrange
        OwnedText? owned = null;

        // Act
        var json = JsonSerializer.Serialize(owned, _options);

        // Assert
        await Assert.That(json).IsEqualTo("null");
    }

    [Test]
    public async Task Deserialize_Null_ReturnsNull()
    {
        // Arrange
        const string json = "null";

        // Act
        var owned = JsonSerializer.Deserialize<OwnedText>(json, _options);

        // Assert
        await Assert.That(owned).IsNull();
    }

    [Test]
    public async Task Deserialize_EmptyString_ReturnsNull()
    {
        // Arrange
        const string json = "\"\"";

        // Act
        var owned = JsonSerializer.Deserialize<OwnedText>(json, _options);

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
        var json = JsonSerializer.Serialize(original, _options);
        using var deserialized = JsonSerializer.Deserialize<OwnedText>(json, _options);

        // Assert
        await Assert.That(deserialized!.Text).IsEqualTo(originalText);
    }

    [Test]
    public async Task Deserialize_MultiSegment_ReturnsValue()
    {
        // Arrange — split JSON across segments so HasValueSequence is true
        var json = Encoding.UTF8.GetBytes("\"hello world\"");
        var first = new MemorySegment<byte>(json.AsMemory(0, 4));
        var last = first.Append(json.AsMemory(4));
        var sequence = new ReadOnlySequence<byte>(first, 0, last, last.Memory.Length);

        // Act
        var reader = new Utf8JsonReader(sequence);
        reader.Read();
        using var owned = JsonSerializer.Deserialize<OwnedText>(ref reader, _options);

        // Assert
        await Assert.That(owned!.Text.ToString()).IsEqualTo("hello world");
    }

    [Test]
    public async Task Deserialize_MultiSegmentEscaped_ReturnsValue()
    {
        // Arrange — escaped value spanning multiple segments
        var json = Encoding.UTF8.GetBytes("\"hello\\nworld\"");
        var first = new MemorySegment<byte>(json.AsMemory(0, 5));
        var last = first.Append(json.AsMemory(5));
        var sequence = new ReadOnlySequence<byte>(first, 0, last, last.Memory.Length);

        // Act
        var reader = new Utf8JsonReader(sequence);
        reader.Read();
        using var owned = JsonSerializer.Deserialize<OwnedText>(ref reader, _options);

        // Assert
        await Assert.That(owned!.Text.ToString()).IsEqualTo("hello\nworld");
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
