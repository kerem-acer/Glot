namespace Glot.Tests;

public partial class TextSpanTests
{
    [Test]
    public async Task Slice_OffsetZero_ReturnsFullSpan()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span.RuneSlice(0).Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Slice_OffsetMiddle_ReturnsSuffix()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span.RuneSlice(2).Equals("llo".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Slice_OffsetEnd_ReturnsEmpty()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span.RuneSlice(5).IsEmpty;

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Slice_MultiByteUtf8_SlicesAtRuneBoundary()
    {
        // Arrange
        var span = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var sliced = span.RuneSlice(3);
        var eq = sliced.Equals("é".AsSpan());
        var len = sliced.RuneLength;

        // Assert
        await Assert.That(eq).IsTrue();
        await Assert.That(len).IsEqualTo(1);
    }

    [Test]
    public async Task Slice_EmojiUtf8_SlicesAtRuneBoundary()
    {
        // Arrange
        var span = new TextSpan("Hi🎉!"u8, TextEncoding.Utf8);

        // Act
        var sliced = span.RuneSlice(2);
        var eq = sliced.Equals("🎉!".AsSpan());
        var len = sliced.RuneLength;

        // Assert
        await Assert.That(eq).IsTrue();
        await Assert.That(len).IsEqualTo(2);
    }

    [Test]
    public async Task Slice_Utf16_SlicesCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var result = span.RuneSlice(3).Equals("lo".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Slice_Utf32_SlicesCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var result = span.RuneSlice(1).Equals("ello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Slice_OffsetAndCount_ReturnsSubstring()
    {
        // Arrange & Act
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var result = span.RuneSlice(6, 5).Equals("World".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Slice_ZeroCount_ReturnsEmpty()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span.RuneSlice(2, 0).IsEmpty;

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Slice_MultiByteOffsetAndCount_SlicesAtRuneBoundaries()
    {
        // Arrange — "cafébar" = c(0) a(1) f(2) é(3) b(4) a(5) r(6)
        var span = new TextSpan("cafébar"u8, TextEncoding.Utf8);

        // Act
        var result = span.RuneSlice(2, 3).Equals("féb".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Slice_Utf16SurrogatePair_SlicesCorrectly()
    {
        // Arrange — A(0) 🎉(1) B(2)
        var bytes = TestHelpers.Encode("A🎉B", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var result = span.RuneSlice(1, 1).Equals("🎉".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Indexer_FullRange_ReturnsFullSpan()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span[..].Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Indexer_StartRange_ReturnsPrefix()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span[..3].Equals("Hel".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Indexer_EndRange_ReturnsSuffix()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span[3..].Equals("lo".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Indexer_MiddleRange_ReturnsSubstring()
    {
        // Arrange & Act
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var result = span[2..7].Equals("llo W".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Indexer_FromEndRange_ReturnsSuffix()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var result = span[^2..].Equals("lo".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Slice_LargeUtf8_ExercisesSimd()
    {
        // Arrange
        var large = new string('A', 2000) + "TARGET";
        var bytes = TestHelpers.Encode(large, TextEncoding.Utf8);
        var span = new TextSpan(bytes, TextEncoding.Utf8);

        // Act
        var sliced = span.RuneSlice(2000);
        var eq = sliced.Equals("TARGET".AsSpan());

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task Slice_LargeUtf16WithSurrogates_ExercisesSimd()
    {
        // Arrange — skip 2001 runes (2000 A's + 1 emoji)
        var large = new string('A', 2000) + "🎉" + "TARGET";
        var bytes = TestHelpers.Encode(large, TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var sliced = span.RuneSlice(2001);
        var eq = sliced.Equals("TARGET".AsSpan());

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task Slice_OutOfRangeOffsetUtf8_ThrowsArgumentOutOfRange()
    {
        // Arrange
        var span = new TextSpan("Hi"u8, TextEncoding.Utf8);
        Exception? caught = null;

        // Act
        try
        {
            _ = span.RuneSlice(10);
        }
        catch (Exception ex)
        {
            caught = ex;
        }

        // Assert
        await Assert.That(caught).IsTypeOf<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Slice_OutOfRangeOffsetUtf16_ThrowsArgumentOutOfRange()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hi", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        Exception? caught = null;

        // Act
        try
        {
            _ = span.RuneSlice(10);
        }
        catch (Exception ex)
        {
            caught = ex;
        }

        // Assert
        await Assert.That(caught).IsTypeOf<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Slice_Utf16OffsetAtEnd_ReturnsEmpty()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hi", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var sliced = span.RuneSlice(2);
        var isEmpty = sliced.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }
}
