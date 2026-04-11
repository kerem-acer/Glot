namespace Glot.Tests;

public partial class PrimitiveParseExtensionTests
{
    static Text CreateText(string value, TextEncoding encoding) => encoding switch
    {
        TextEncoding.Utf16 => Text.From(value),
        TextEncoding.Utf8 => Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes(value)),
        TextEncoding.Utf32 => Text.FromUtf32(ToCodePoints(value)),
        _ => throw new ArgumentOutOfRangeException(nameof(encoding)),
    };

    static int[] ToCodePoints(string s)
    {
        var runes = new List<int>();
        foreach (var rune in s.EnumerateRunes())
        {
            runes.Add(rune.Value);
        }

        return [.. runes];
    }

    // byte

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task ByteParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const byte expected = 255;
        var text = CreateText("255", encoding);

        // Act
        var result = byte.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task ByteTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const byte expected = 42;
        var text = CreateText("42", encoding);

        // Act
        var success = byte.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ByteParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => byte.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task ByteTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = byte.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // sbyte

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task SByteParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const sbyte expected = -42;
        var text = CreateText("-42", encoding);

        // Act
        var result = sbyte.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task SByteTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const sbyte expected = 127;
        var text = CreateText("127", encoding);

        // Act
        var success = sbyte.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task SByteParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => sbyte.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task SByteTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = sbyte.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // short

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task ShortParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const short expected = -1234;
        var text = CreateText("-1234", encoding);

        // Act
        var result = short.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task ShortTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const short expected = 32767;
        var text = CreateText("32767", encoding);

        // Act
        var success = short.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ShortParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => short.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task ShortTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = short.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // ushort

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task UShortParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const ushort expected = 65535;
        var text = CreateText("65535", encoding);

        // Act
        var result = ushort.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task UShortTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const ushort expected = 1000;
        var text = CreateText("1000", encoding);

        // Act
        var success = ushort.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task UShortParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => ushort.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task UShortTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = ushort.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // int

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task IntParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const int expected = -2_000_000;
        var text = CreateText("-2000000", encoding);

        // Act
        var result = int.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task IntTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const int expected = 42;
        var text = CreateText("42", encoding);

        // Act
        var success = int.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task IntParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => int.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task IntTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = int.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // uint

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task UIntParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const uint expected = 4_000_000_000;
        var text = CreateText("4000000000", encoding);

        // Act
        var result = uint.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task UIntTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const uint expected = 100;
        var text = CreateText("100", encoding);

        // Act
        var success = uint.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task UIntParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => uint.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task UIntTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = uint.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // long

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task LongParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const long expected = 9_876_543_210L;
        var text = CreateText("9876543210", encoding);

        // Act
        var result = long.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task LongTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const long expected = -999L;
        var text = CreateText("-999", encoding);

        // Act
        var success = long.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task LongParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => long.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task LongTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = long.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // ulong

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task ULongParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const ulong expected = 18_000_000_000_000_000_000UL;
        var text = CreateText("18000000000000000000", encoding);

        // Act
        var result = ulong.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task ULongTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const ulong expected = 0UL;
        var text = CreateText("0", encoding);

        // Act
        var success = ulong.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ULongParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => ulong.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task ULongTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = ulong.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // float

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task FloatParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const float expected = -42f;
        var text = CreateText("-42", encoding);

        // Act
        var result = float.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task FloatTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const float expected = 7f;
        var text = CreateText("7", encoding);

        // Act
        var success = float.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task FloatParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => float.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task FloatTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = float.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // double

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task DoubleParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const double expected = -12345d;
        var text = CreateText("-12345", encoding);

        // Act
        var result = double.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task DoubleTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const double expected = 777d;
        var text = CreateText("777", encoding);

        // Act
        var success = double.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task DoubleParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => double.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task DoubleTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = double.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // decimal

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task DecimalParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const decimal expected = 99999m;
        var text = CreateText("99999", encoding);

        // Act
        var result = decimal.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task DecimalTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const decimal expected = -50m;
        var text = CreateText("-50", encoding);

        // Act
        var success = decimal.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task DecimalParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => decimal.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task DecimalTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = decimal.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // bool

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task BoolParse_True_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        var text = CreateText("True", encoding);

        // Act
        var result = bool.Parse(text);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task BoolParse_False_ReturnsFalse(TextEncoding encoding)
    {
        // Arrange
        var text = CreateText("False", encoding);

        // Act
        var result = bool.Parse(text);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task BoolTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        var text = CreateText("True", encoding);

        // Act
        var success = bool.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task BoolParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => bool.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task BoolTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = bool.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // Guid

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task GuidParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        var expected = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
        var text = CreateText("a1b2c3d4-e5f6-7890-abcd-ef1234567890", encoding);

        // Act
        var result = Guid.Parse(text);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task GuidTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        var expected = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
        var text = CreateText("a1b2c3d4-e5f6-7890-abcd-ef1234567890", encoding);

        // Act
        var success = Guid.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task GuidParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => Guid.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task GuidTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = Guid.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // DateTime

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task DateTimeParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const int expectedYear = 2024;
        const int expectedMonth = 6;
        const int expectedDay = 15;
        var text = CreateText("2024-06-15", encoding);

        // Act
        var result = DateTime.Parse(text);

        // Assert
        await Assert.That(result.Year).IsEqualTo(expectedYear);
        await Assert.That(result.Month).IsEqualTo(expectedMonth);
        await Assert.That(result.Day).IsEqualTo(expectedDay);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task DateTimeTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const int expectedYear = 2024;
        var text = CreateText("2024-06-15", encoding);

        // Act
        var success = DateTime.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result.Year).IsEqualTo(expectedYear);
    }

    [Test]
    public async Task DateTimeParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => DateTime.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task DateTimeTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = DateTime.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }

    // DateTimeOffset

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task DateTimeOffsetParse_ValidValue_ReturnsExpected(TextEncoding encoding)
    {
        // Arrange
        const int expectedYear = 2024;
        const int expectedMonth = 6;
        const int expectedDay = 15;
        var text = CreateText("2024-06-15T10:30:00Z", encoding);

        // Act
        var result = DateTimeOffset.Parse(text);

        // Assert
        await Assert.That(result.Year).IsEqualTo(expectedYear);
        await Assert.That(result.Month).IsEqualTo(expectedMonth);
        await Assert.That(result.Day).IsEqualTo(expectedDay);
    }

    [Test]
    [Arguments(TextEncoding.Utf16)]
    [Arguments(TextEncoding.Utf8)]
    [Arguments(TextEncoding.Utf32)]
    public async Task DateTimeOffsetTryParse_ValidValue_ReturnsTrue(TextEncoding encoding)
    {
        // Arrange
        const int expectedYear = 2024;
        var text = CreateText("2024-06-15T10:30:00Z", encoding);

        // Act
        var success = DateTimeOffset.TryParse(text, out var result);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(result.Year).IsEqualTo(expectedYear);
    }

    [Test]
    public async Task DateTimeOffsetParse_InvalidValue_ThrowsFormatException()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act & Assert
        await Assert.That(() => DateTimeOffset.Parse(text)).Throws<FormatException>();
    }

    [Test]
    public async Task DateTimeOffsetTryParse_InvalidValue_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("xyz");

        // Act
        var success = DateTimeOffset.TryParse(text, out _);

        // Assert
        await Assert.That(success).IsFalse();
    }
}
