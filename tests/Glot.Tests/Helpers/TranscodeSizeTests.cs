namespace Glot.Tests;

public class TranscodeSizeTests
{
    [Test]
    [Arguments(10, TextEncoding.Utf16, TextEncoding.Utf8, 15)]
    [Arguments(20, TextEncoding.Utf16, TextEncoding.Utf8, 30)]
    [Arguments(0, TextEncoding.Utf16, TextEncoding.Utf8, 0)]
    public async Task Estimate_Utf16ToUtf8_ReturnsDivTwoTimesThree(
        int sourceBytesLength,
        TextEncoding source,
        TextEncoding target,
        int expected)
    {
        // Act
        var result = TranscodeSize.Estimate(sourceBytesLength, source, target);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(10, TextEncoding.Utf8, TextEncoding.Utf16, 20)]
    [Arguments(5, TextEncoding.Utf8, TextEncoding.Utf16, 10)]
    public async Task Estimate_Utf8ToUtf16_ReturnsTimesTwo(
        int sourceBytesLength,
        TextEncoding source,
        TextEncoding target,
        int expected)
    {
        // Act
        var result = TranscodeSize.Estimate(sourceBytesLength, source, target);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(12, TextEncoding.Utf32, TextEncoding.Utf8, 12)]
    [Arguments(4, TextEncoding.Utf32, TextEncoding.Utf8, 4)]
    public async Task Estimate_Utf32ToUtf8_ReturnsSameLength(
        int sourceBytesLength,
        TextEncoding source,
        TextEncoding target,
        int expected)
    {
        // Act
        var result = TranscodeSize.Estimate(sourceBytesLength, source, target);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(10, TextEncoding.Utf8, TextEncoding.Utf32, 40)]
    [Arguments(3, TextEncoding.Utf8, TextEncoding.Utf32, 12)]
    public async Task Estimate_Utf8ToUtf32_ReturnsTimesFour(
        int sourceBytesLength,
        TextEncoding source,
        TextEncoding target,
        int expected)
    {
        // Act
        var result = TranscodeSize.Estimate(sourceBytesLength, source, target);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(12, TextEncoding.Utf32, TextEncoding.Utf16, 12)]
    [Arguments(8, TextEncoding.Utf32, TextEncoding.Utf16, 8)]
    public async Task Estimate_Utf32ToUtf16_ReturnsSameLength(
        int sourceBytesLength,
        TextEncoding source,
        TextEncoding target,
        int expected)
    {
        // Act
        var result = TranscodeSize.Estimate(sourceBytesLength, source, target);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(10, TextEncoding.Utf16, TextEncoding.Utf32, 20)]
    [Arguments(6, TextEncoding.Utf16, TextEncoding.Utf32, 12)]
    public async Task Estimate_Utf16ToUtf32_ReturnsTimesTwo(
        int sourceBytesLength,
        TextEncoding source,
        TextEncoding target,
        int expected)
    {
        // Act
        var result = TranscodeSize.Estimate(sourceBytesLength, source, target);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(10, TextEncoding.Utf8, TextEncoding.Utf8, 40)]
    [Arguments(10, TextEncoding.Utf16, TextEncoding.Utf16, 40)]
    [Arguments(10, TextEncoding.Utf32, TextEncoding.Utf32, 40)]
    public async Task Estimate_SameEncoding_ReturnsTimesFour(
        int sourceBytesLength,
        TextEncoding source,
        TextEncoding target,
        int expected)
    {
        // Act
        var result = TranscodeSize.Estimate(sourceBytesLength, source, target);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }
}
