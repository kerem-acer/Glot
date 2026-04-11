namespace Glot.Tests;

public partial class TextSpanTests
{
    [Test]
    public Task Split_BasicComma_ReturnsThreeParts()
    {
        // Arrange
        var span = new TextSpan("a,b,c"u8, TextEncoding.Utf8);
        var sep = new TextSpan(","u8, TextEncoding.Utf8);

        // Act
        var parts = new List<string>();
        foreach (var part in span.Split(sep))
        {
            parts.Add(part.ToString());
        }

        // Assert
        return Verify(parts);
    }

    [Test]
    public async Task Split_NoSeparatorFound_ReturnsSinglePart()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var sep = new TextSpan(","u8, TextEncoding.Utf8);

        // Act
        var count = TestHelpers.CollectSplitCount(span, sep);

        // Assert
        await Assert.That(count).IsEqualTo(1);
    }

    [Test]
    public async Task Split_EmptySeparator_ReturnsSinglePart()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var sep = new TextSpan([], TextEncoding.Utf8);

        // Act
        var count = TestHelpers.CollectSplitCount(span, sep);

        // Assert
        await Assert.That(count).IsEqualTo(1);
    }

    [Test]
    public async Task Split_MultiCharSeparator_SplitsCorrectly()
    {
        // Arrange
        var span = new TextSpan("a::b::c"u8, TextEncoding.Utf8);
        var sep = new TextSpan("::"u8, TextEncoding.Utf8);

        // Act
        var count = TestHelpers.CollectSplitCount(span, sep);

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_AdjacentSeparators_ProducesEmptyParts()
    {
        // Arrange
        var span = new TextSpan("a,,b"u8, TextEncoding.Utf8);
        var sep = new TextSpan(","u8, TextEncoding.Utf8);

        // Act
        var count = TestHelpers.CollectSplitCount(span, sep);

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_SeparatorAtStart_ProducesLeadingEmptyPart()
    {
        // Arrange
        var span = new TextSpan(",a,b"u8, TextEncoding.Utf8);
        var sep = new TextSpan(","u8, TextEncoding.Utf8);

        // Act
        var count = TestHelpers.CollectSplitCount(span, sep);

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_SeparatorAtEnd_ProducesTrailingEmptyPart()
    {
        // Arrange
        var span = new TextSpan("a,b,"u8, TextEncoding.Utf8);
        var sep = new TextSpan(","u8, TextEncoding.Utf8);

        // Act
        var count = TestHelpers.CollectSplitCount(span, sep);

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_ReadOnlySpanByteOverload_SplitsCorrectly()
    {
        // Arrange
        var span = new TextSpan("a-b-c"u8, TextEncoding.Utf8);

        // Act
        var count = 0;
        foreach (var _ in span.Split("-"u8))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_ReadOnlySpanCharOverload_SplitsCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("a,b,c", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var count = 0;
        foreach (var _ in span.Split(",".AsSpan()))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public Task Split_Utf16Content_ProducesCorrectParts()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        var sepBytes = TestHelpers.Encode(" ", TextEncoding.Utf16);
        var sep = new TextSpan(sepBytes, TextEncoding.Utf16);

        // Act
        var parts = TestHelpers.CollectSplitParts(span, sep);

        // Assert
        return Verify(parts);
    }

    [Test]
    public async Task Split_CrossEncodingSeparator_SplitsCorrectly()
    {
        // Arrange
        var span = new TextSpan("a,b,c"u8, TextEncoding.Utf8);
        var sep = new TextSpan(TestHelpers.Encode(",", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var count = TestHelpers.CollectSplitCount(span, sep);

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_MultiByteContent_SplitsCorrectly()
    {
        // Arrange
        var span = new TextSpan("café,日本"u8, TextEncoding.Utf8);
        var sep = new TextSpan(","u8, TextEncoding.Utf8);

        // Act
        var count = TestHelpers.CollectSplitCount(span, sep);

        // Assert
        await Assert.That(count).IsEqualTo(2);
    }

    [Test]
    public async Task Split_Utf32Source_SplitsCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("a,b,c", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);
        var sep = new TextSpan(TestHelpers.Encode(",", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var count = TestHelpers.CollectSplitCount(span, sep);

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }
}
