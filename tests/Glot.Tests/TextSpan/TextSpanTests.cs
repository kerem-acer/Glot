namespace Glot.Tests;

public partial class TextSpanTests
{
    [Test]
    [Arguments("Hello", TextEncoding.Utf8, 5)]
    [Arguments("Hello", TextEncoding.Utf16, 5)]
    [Arguments("Hello", TextEncoding.Utf32, 5)]
    [Arguments("café", TextEncoding.Utf8, 4)]
    [Arguments("café", TextEncoding.Utf16, 4)]
    [Arguments("café", TextEncoding.Utf32, 4)]
    [Arguments("日本語", TextEncoding.Utf8, 3)]
    [Arguments("日本語", TextEncoding.Utf16, 3)]
    [Arguments("日本語", TextEncoding.Utf32, 3)]
    [Arguments("🎉", TextEncoding.Utf8, 1)]
    [Arguments("🎉", TextEncoding.Utf16, 1)]
    [Arguments("🎉", TextEncoding.Utf32, 1)]
    [Arguments("Hi🎉!", TextEncoding.Utf8, 4)]
    [Arguments("Hi🎉!", TextEncoding.Utf16, 4)]
    [Arguments("Hi🎉!", TextEncoding.Utf32, 4)]
    public async Task Length_VariousInputs_ReturnsRuneCount(string input, TextEncoding encoding, int expected)
    {
        // Arrange
        var bytes = TestHelpers.Encode(input, encoding);

        // Act
        var span = new TextSpan(bytes, encoding);
        var length = span.RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf32)]
    public async Task Length_EmptySpan_ReturnsZero(TextEncoding encoding)
    {
        // Arrange & Act
        var span = new TextSpan([], encoding);
        var length = span.RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(0);
    }

    [Test]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf32)]
    public async Task IsEmpty_EmptySpan_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange & Act
        var span = new TextSpan([], encoding);
        var isEmpty = span.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmpty_NonEmptySpan_ReturnsFalse()
    {
        // Arrange & Act
        var span = new TextSpan("x"u8, TextEncoding.Utf8);
        var isEmpty = span.IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsFalse();
    }

    [Test]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf32)]
    public async Task Encoding_AllValues_ReturnsCorrectEncoding(TextEncoding encoding)
    {
        // Arrange
        var bytes = TestHelpers.Encode("A", encoding);

        // Act
        var span = new TextSpan(bytes, encoding);
        var result = span.Encoding;

        // Assert
        await Assert.That(result).IsEqualTo(encoding);
    }

    [Test]
    public async Task Bytes_AfterConstruction_MatchesSource()
    {
        // Arrange
        ReadOnlySpan<byte> source = "Hello"u8;

        // Act
        var span = new TextSpan(source, TextEncoding.Utf8);
        var areEqual = span.Bytes.SequenceEqual(source);

        // Assert
        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task Length_LargeUtf8_ExercisesSimd()
    {
        // Arrange
        var large = new string('A', 2000) + new string('é', 500) + new string('日', 300);
        var bytes = TestHelpers.Encode(large, TextEncoding.Utf8);
        const int expectedLength = 2000 + 500 + 300;

        // Act
        var span = new TextSpan(bytes, TextEncoding.Utf8);
        var length = span.RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(expectedLength);
    }

    [Test]
    public async Task Length_LargeUtf16WithSurrogates_ExercisesSimd()
    {
        // Arrange
        var large = new string('A', 2000) + "🎉" + new string('B', 2000);
        var bytes = TestHelpers.Encode(large, TextEncoding.Utf16);
        const int expectedLength = 2000 + 1 + 2000;

        // Act
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        var length = span.RuneLength;

        // Assert
        await Assert.That(length).IsEqualTo(expectedLength);
    }
}
