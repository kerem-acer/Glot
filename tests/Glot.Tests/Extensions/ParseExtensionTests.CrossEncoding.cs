namespace Glot.Tests;

public partial class ParseExtensionTests
{
    // Parse from UTF-8 text via ISpanParsable (transcodes to UTF-16)

    [Test]
    public async Task Int_Parse_FromUtf8Text_Transcodes()
    {
        // Arrange
        var text = Text.FromUtf8("42"u8);

        // Act
        var result = int.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task Int_TryParse_FromUtf8Text_Transcodes()
    {
        // Arrange
        var text = Text.FromUtf8("99"u8);

        // Act
        var success = int.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(99);
    }

    [Test]
    public async Task Double_Parse_FromUtf8Text_Transcodes()
    {
        // Arrange
        var text = Text.FromUtf8("2.71"u8);

        // Act
        var result = double.Parse(text, System.Globalization.CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result).IsEqualTo(2.71);
    }

    [Test]
    public async Task Int_TryParse_FromUtf8Text_Invalid_ReturnsFalse()
    {
        // Arrange
        var text = Text.FromUtf8("abc"u8);

        // Act
        var success = int.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(result).IsEqualTo(0);
    }

    // ParseUtf8 from UTF-16 text (transcodes to UTF-8)

    [Test]
    public async Task Int_ParseUtf8_FromUtf16Text_Transcodes()
    {
        // Arrange
        var text = Text.From("42");

        // Act
        var result = int.ParseUtf8(text);

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task Int_TryParseUtf8_FromUtf16Text_Transcodes()
    {
        // Arrange
        var text = Text.From("99");

        // Act
        var success = int.TryParseUtf8(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(99);
    }

    [Test]
    public async Task Double_ParseUtf8_FromUtf16Text_Transcodes()
    {
        // Arrange
        var text = Text.From("2.71");

        // Act
        var result = double.ParseUtf8(text, System.Globalization.CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result).IsEqualTo(2.71);
    }

    [Test]
    public async Task Int_TryParseUtf8_Invalid_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("not-a-number");

        // Act
        var success = int.TryParseUtf8(text, out var result);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(result).IsEqualTo(0);
    }

    // TryParse with provider from non-UTF16 text

    [Test]
    public async Task Double_TryParse_FromUtf8Text_WithProvider()
    {
        // Arrange
        var text = Text.FromUtf8("1.23"u8);

        // Act
        var success = double.TryParse(text, System.Globalization.CultureInfo.InvariantCulture, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(1.23);
    }

    // TryParseUtf8 with provider from non-UTF8 text

    [Test]
    public async Task Double_TryParseUtf8_FromUtf16Text_WithProvider()
    {
        // Arrange
        var text = Text.From("1.23");

        // Act
        var success = double.TryParseUtf8(text, System.Globalization.CultureInfo.InvariantCulture, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(1.23);
    }

    // Parse from UTF-32 text (exercises non-UTF16 path)

    static readonly int[] Utf32SeventySeven = ['7', '7'];
    static readonly int[] Utf32FiftyFive = ['5', '5'];

    [Test]
    public async Task Int_Parse_FromUtf32Text_Transcodes()
    {
        // Arrange
        var text = Text.FromUtf32(Utf32SeventySeven);

        // Act
        var result = int.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(77);
    }

    [Test]
    public async Task Int_ParseUtf8_FromUtf32Text_Transcodes()
    {
        // Arrange
        var text = Text.FromUtf32(Utf32FiftyFive);

        // Act
        var result = int.ParseUtf8(text);

        // Assert
        await Assert.That(result).IsEqualTo(55);
    }
}
