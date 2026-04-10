namespace Glot.Tests;

public partial class TextSpanTests
{
    // Slice — Length correctness

    [Test]
    public async Task Slice_Offset_LengthIsCorrect()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var length = span.RuneSlice(6).RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(5);
    }

    [Test]
    public async Task Slice_OffsetAndCount_LengthIsCorrect()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var length = span.RuneSlice(2, 3).RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(3);
    }

    [Test]
    public async Task Slice_MultiByteUtf8_LengthIsRuneCount()
    {
        // Arrange — "café日本" = c(0) a(1) f(2) é(3) 日(4) 本(5) — 6 runes
        var span = new TextSpan("café日本"u8, TextEncoding.Utf8);

        // Act
        var sliced = span.RuneSlice(2, 3);
        var length = sliced.RuneLength;

        // Assert — "féb" → f, é, 日 = 3 runes
        await Assert.That(length).IsEqualTo(3);
    }

    [Test]
    public async Task Slice_Utf16Surrogate_LengthIsRuneCount()
    {
        // Arrange — "A🎉B" = A(0) 🎉(1) B(2) — 3 runes
        var bytes = TestHelpers.Encode("A🎉B", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var length = span.RuneSlice(1).RuneLength;

        // Assert — "🎉B" = 2 runes
        await Assert.That(length).IsEqualTo(2);
    }

    [Test]
    public async Task Slice_Utf32_LengthIsCorrect()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var length = span.RuneSlice(2, 2).RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(2);
    }

    [Test]
    public async Task Slice_ToEmpty_LengthIsZero()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var length = span.RuneSlice(5).RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(0);
    }

    [Test]
    public async Task Indexer_Range_LengthIsCorrect()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var length = span[2..7].RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(5);
    }

    // Trim — Length correctness

    [Test]
    public async Task TrimStart_LengthIsCorrect()
    {
        // Arrange — "  Hello" = 2 spaces + 5 chars = 7 runes
        var span = new TextSpan("  Hello"u8, TextEncoding.Utf8);

        // Act
        var length = span.TrimStart().RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(5);
    }

    [Test]
    public async Task TrimEnd_LengthIsCorrect()
    {
        // Arrange
        var span = new TextSpan("Hello  "u8, TextEncoding.Utf8);

        // Act
        var length = span.TrimEnd().RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(5);
    }

    [Test]
    public async Task Trim_LengthIsCorrect()
    {
        // Arrange
        var span = new TextSpan("  Hello  "u8, TextEncoding.Utf8);

        // Act
        var length = span.Trim().RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(5);
    }

    [Test]
    public async Task Trim_AllWhitespace_LengthIsZero()
    {
        // Arrange
        var span = new TextSpan("   \t\n"u8, TextEncoding.Utf8);

        // Act
        var length = span.Trim().RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(0);
    }

    [Test]
    public async Task Trim_MultiByteContent_LengthIsCorrect()
    {
        // Arrange — " café " = space + c + a + f + é + space = 6 runes, trim → 4
        var span = new TextSpan(" café "u8, TextEncoding.Utf8);

        // Act
        var length = span.Trim().RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(4);
    }

    [Test]
    public async Task TrimStart_NoWhitespace_LengthUnchanged()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var length = span.TrimStart().RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(5);
    }

    [Test]
    public async Task TrimEnd_Utf16_LengthIsCorrect()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hi  ", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var length = span.TrimEnd().RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(2);
    }

    // Split — Length correctness on yielded segments

    [Test]
    public async Task Split_Segments_HaveCorrectLength()
    {
        // Arrange
        var span = new TextSpan("ab,cde,f"u8, TextEncoding.Utf8);
        var sep = new TextSpan(","u8, TextEncoding.Utf8);
        const int expectedFirst = 2;
        const int expectedSecond = 3;
        const int expectedThird = 1;

        // Act
        var lengths = new List<int>();
        foreach (var part in span.Split(sep))
        {
            lengths.Add(part.RuneLength);
        }

        // Assert
        await Assert.That(lengths[0]).IsEqualTo(expectedFirst);
        await Assert.That(lengths[1]).IsEqualTo(expectedSecond);
        await Assert.That(lengths[2]).IsEqualTo(expectedThird);
    }

    [Test]
    public async Task Split_MultiByteSegments_HaveCorrectLength()
    {
        // Arrange — "café,日本語" → "café"(4 runes), "日本語"(3 runes)
        var span = new TextSpan("café,日本語"u8, TextEncoding.Utf8);
        var sep = new TextSpan(","u8, TextEncoding.Utf8);
        const int expectedFirst = 4;
        const int expectedSecond = 3;

        // Act
        var lengths = new List<int>();
        foreach (var part in span.Split(sep))
        {
            lengths.Add(part.RuneLength);
        }

        // Assert
        await Assert.That(lengths[0]).IsEqualTo(expectedFirst);
        await Assert.That(lengths[1]).IsEqualTo(expectedSecond);
    }

    [Test]
    public async Task Split_EmptySeparator_SingleSegmentHasCorrectLength()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var sep = new TextSpan([], TextEncoding.Utf8);

        // Act
        var length = 0;
        foreach (var part in span.Split(sep))
        {
            length = part.RuneLength;
        }

        // Assert
        await Assert.That(length).IsEqualTo(5);
    }

    [Test]
    public async Task Split_AdjacentSeparators_EmptySegmentsHaveZeroLength()
    {
        // Arrange — "a,,b" → "a"(1), ""(0), "b"(1)
        var span = new TextSpan("a,,b"u8, TextEncoding.Utf8);
        var sep = new TextSpan(","u8, TextEncoding.Utf8);
        const int expectedEmpty = 0;

        // Act
        var lengths = new List<int>();
        foreach (var part in span.Split(sep))
        {
            lengths.Add(part.RuneLength);
        }

        // Assert
        await Assert.That(lengths[0]).IsEqualTo(1);
        await Assert.That(lengths[1]).IsEqualTo(expectedEmpty);
        await Assert.That(lengths[2]).IsEqualTo(1);
    }

    [Test]
    public async Task Split_CrossEncoding_SegmentsHaveCorrectLength()
    {
        // Arrange — UTF-8 source, UTF-16 separator
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var sepBytes = TestHelpers.Encode(" ", TextEncoding.Utf16);
        var sep = new TextSpan(sepBytes, TextEncoding.Utf16);
        const int expectedFirst = 5;
        const int expectedSecond = 5;

        // Act
        var lengths = new List<int>();
        foreach (var part in span.Split(sep))
        {
            lengths.Add(part.RuneLength);
        }

        // Assert
        await Assert.That(lengths[0]).IsEqualTo(expectedFirst);
        await Assert.That(lengths[1]).IsEqualTo(expectedSecond);
    }
}
