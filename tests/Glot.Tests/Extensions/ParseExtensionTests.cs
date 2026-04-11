namespace Glot.Tests;

public partial class ParseExtensionTests
{
    // int

    [Test]
    public async Task Int_Parse_FromText()
    {
        // Arrange
        var text = Text.From("42");

        // Act
        var result = int.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task Int_TryParse_FromText_Succeeds()
    {
        // Arrange
        var text = Text.FromUtf8("123"u8);

        // Act
        var success = int.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(123);
    }

    [Test]
    public async Task Int_TryParse_Invalid_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("not a number");

        // Act
        var success = int.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(result).IsEqualTo(0);
    }

    // long

    [Test]
    public async Task Long_Parse_FromText()
    {
        // Arrange
        var text = Text.From("9876543210");

        // Act
        var result = long.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(9876543210L);
    }

    // double

    [Test]
    public async Task Double_TryParse_FromText()
    {
        // Arrange
        var text = Text.From("3.14");

        // Act
        var success = double.TryParse(text, System.Globalization.CultureInfo.InvariantCulture, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(3.14);
    }

    // bool

    [Test]
    public async Task Bool_Parse_FromText()
    {
        // Act
        var result = bool.Parse(Text.From("True"));

        // Assert
        await Assert.That(result).IsTrue();
    }

    // Guid

    [Test]
    public async Task Guid_TryParse_FromText()
    {
        // Arrange
        var expected = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var text = Text.From("12345678-1234-1234-1234-123456789abc");

        // Act
        var success = Guid.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    // DateTime

    [Test]
    public async Task DateTime_Parse_FromText()
    {
        // Arrange
        var text = Text.From("2026-04-10");

        // Act
        var result = DateTime.Parse(text, System.Globalization.CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result.Year).IsEqualTo(2026);
        await Assert.That(result.Month).IsEqualTo(4);
        await Assert.That(result.Day).IsEqualTo(10);
    }

    // UTF-8 fast path

    [Test]
    public async Task Int_ParseUtf8_FromUtf8Text()
    {
        // Arrange
        var text = Text.FromUtf8("42"u8);

        // Act
        var result = int.ParseUtf8(text);

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task Int_TryParseUtf8_Succeeds()
    {
        // Arrange
        var text = Text.FromUtf8("123"u8);

        // Act
        var success = int.TryParseUtf8(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(123);
    }

    [Test]
    public async Task Double_ParseUtf8_FromUtf8Text()
    {
        // Arrange
        var text = Text.FromUtf8("3.14"u8);

        // Act
        var result = double.ParseUtf8(text, System.Globalization.CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result).IsEqualTo(3.14);
    }

    [Test]
    public async Task Guid_TryParseUtf8_Succeeds()
    {
        // Arrange
        var expected = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var text = Text.FromUtf8("12345678-1234-1234-1234-123456789abc"u8);

        // Act
        var success = Guid.TryParseUtf8(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    // Large input — exceeds StackAllocThreshold (256), triggers ArrayPool rent/return

    [Test]
    public async Task Int_Parse_LargeUtf8Input_UsesArrayPool()
    {
        // Arrange — pad with leading spaces to exceed 256 chars
        var padded = new string(' ', 300) + "42";
        var text = Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes(padded));

        // Act
        var result = int.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(42);
    }

    [Test]
    public async Task Int_TryParse_LargeUtf8Input_UsesArrayPool()
    {
        // Arrange
        var padded = new string(' ', 300) + "99";
        var text = Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes(padded));

        // Act
        var success = int.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(99);
    }

    [Test]
    public async Task Int_ParseUtf8_LargeUtf16Input_UsesArrayPool()
    {
        // Arrange — UTF-16 text forces transcode, padding exceeds threshold
        var padded = new string(' ', 300) + "77";
        var text = Text.From(padded);

        // Act
        var result = int.ParseUtf8(text);

        // Assert
        await Assert.That(result).IsEqualTo(77);
    }

    [Test]
    public async Task Int_TryParseUtf8_LargeUtf16Input_UsesArrayPool()
    {
        // Arrange
        var padded = new string(' ', 300) + "55";
        var text = Text.From(padded);

        // Act
        var success = int.TryParseUtf8(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(55);
    }

    // TryParse single-arg overload (no provider)

    [Test]
    public async Task Int_TryParse_NoProvider_Succeeds()
    {
        // Arrange
        var text = Text.From("42");

        // Act
        var success = int.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(42);
    }
}
