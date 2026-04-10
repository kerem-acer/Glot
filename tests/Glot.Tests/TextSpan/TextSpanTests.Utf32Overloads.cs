using System.Runtime.InteropServices;

namespace Glot.Tests;

public partial class TextSpanTests
{
    static ReadOnlySpan<int> ToUtf32Span(string s)
        => MemoryMarshal.Cast<byte, int>(TestHelpers.Encode(s, TextEncoding.Utf32));

    // Contains — ReadOnlySpan<int>

    [Test]
    public async Task Contains_Utf32Span_Found_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var result = span.Contains(ToUtf32Span("World"));

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_Utf32Span_NotFound_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.Contains(ToUtf32Span("xyz"));

        // Assert
        await Assert.That(result).IsFalse();
    }

    // RuneIndexOf — ReadOnlySpan<int>

    [Test]
    public async Task RuneIndexOf_Utf32Span_ReturnsRunePosition()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var pos = span.RuneIndexOf(ToUtf32Span("World"));

        // Assert
        await Assert.That(pos).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_Utf32Span_NotFound_ReturnsNegativeOne()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var pos = span.RuneIndexOf(ToUtf32Span("xyz"));

        // Assert
        await Assert.That(pos).IsEqualTo(-1);
    }

    // LastRuneIndexOf — ReadOnlySpan<int>

    [Test]
    public async Task LastRuneIndexOf_Utf32Span_ReturnsLastRunePosition()
    {
        // Arrange
        var span = new TextSpan("abcabc"u8, TextEncoding.Utf8);

        // Act
        var pos = span.LastRuneIndexOf(ToUtf32Span("abc"));

        // Assert
        await Assert.That(pos).IsEqualTo(3);
    }

    // StartsWith — ReadOnlySpan<int>

    [Test]
    public async Task StartsWith_Utf32Span_Match_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var result = span.StartsWith(ToUtf32Span("Hello"));

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_Utf32Span_NoMatch_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.StartsWith(ToUtf32Span("World"));

        // Assert
        await Assert.That(result).IsFalse();
    }

    // EndsWith — ReadOnlySpan<int>

    [Test]
    public async Task EndsWith_Utf32Span_Match_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var result = span.EndsWith(ToUtf32Span("World"));

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_Utf32Span_NoMatch_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.EndsWith(ToUtf32Span("Hello World"));

        // Assert
        await Assert.That(result).IsFalse();
    }

    // Equals — ReadOnlySpan<int>

    [Test]
    public async Task Equals_Utf32Span_Match_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.Equals(ToUtf32Span("Hello"));

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Equals_Utf32Span_NoMatch_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.Equals(ToUtf32Span("World"));

        // Assert
        await Assert.That(result).IsFalse();
    }

    // Split — ReadOnlySpan<int>

    [Test]
    public async Task Split_Utf32Span_SplitsCorrectly()
    {
        // Arrange
        var span = new TextSpan("a,b,c"u8, TextEncoding.Utf8);

        // Act
        var count = 0;
        foreach (var _ in span.Split(ToUtf32Span(",")))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    // Encoding parameter overloads

    [Test]
    public async Task Contains_ReadOnlySpanByte_Utf16Encoding_Works()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var searchBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act
        var result = span.Contains(searchBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task RuneIndexOf_ReadOnlySpanByte_Utf16Encoding_Works()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var searchBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act
        var pos = span.RuneIndexOf(searchBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(pos).IsEqualTo(6);
    }

    [Test]
    public async Task StartsWith_ReadOnlySpanByte_Utf32Encoding_Works()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var searchBytes = TestHelpers.Encode("Hel", TextEncoding.Utf32);

        // Act
        var result = span.StartsWith(searchBytes, TextEncoding.Utf32);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Equals_ReadOnlySpanByte_Utf16Encoding_Works()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var valueBytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);

        // Act
        var result = span.Equals(valueBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Split_ReadOnlySpanByte_Utf16Encoding_Works()
    {
        // Arrange
        var span = new TextSpan("a,b,c"u8, TextEncoding.Utf8);
        var sepBytes = TestHelpers.Encode(",", TextEncoding.Utf16);

        // Act
        var count = 0;
        foreach (var _ in span.Split(sepBytes, TextEncoding.Utf16))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }
}
