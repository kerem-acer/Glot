using System.Text;

namespace Glot.Tests;

public class RuneExtensionsTests
{
    [Test]
    [Arguments('A', TextEncoding.Utf8, 1)]
    [Arguments('A', TextEncoding.Utf16, 2)]
    [Arguments('A', TextEncoding.Utf32, 4)]
    [Arguments('é', TextEncoding.Utf8, 2)]
    [Arguments('é', TextEncoding.Utf16, 2)]
    [Arguments('é', TextEncoding.Utf32, 4)]
    [Arguments('日', TextEncoding.Utf8, 3)]
    [Arguments('日', TextEncoding.Utf16, 2)]
    [Arguments('日', TextEncoding.Utf32, 4)]
    public async Task GetByteCount_BmpRune_ReturnsCorrectSize(char c, TextEncoding encoding, int expected)
    {
        // Arrange
        var rune = new Rune(c);

        // Act
        var result = rune.GetByteCount(encoding);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(0x1F389, TextEncoding.Utf8, 4)]
    [Arguments(0x1F389, TextEncoding.Utf16, 4)]
    [Arguments(0x1F389, TextEncoding.Utf32, 4)]
    public async Task GetByteCount_SupplementaryRune_ReturnsCorrectSize(int codePoint, TextEncoding encoding, int expected)
    {
        // Arrange — 🎉 = U+1F389, surrogate pair in UTF-16 (4 bytes)
        var rune = new Rune(codePoint);

        // Act
        var result = rune.GetByteCount(encoding);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }
}
