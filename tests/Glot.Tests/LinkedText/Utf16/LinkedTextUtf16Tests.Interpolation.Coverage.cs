namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    // ISpanFormattable: int hole

    [Test]
    public async Task Interpolation_IntHole_FormatsViaSpanFormattable()
    {
        // Arrange
        const int value = 12345;

        // Act
        LinkedTextUtf16 linked = $"count={value}";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "count=12345";
        await Assert.That(result).IsEqualTo(expected);
    }

    // ISpanFormattable: double hole (locale-safe)

    [Test]
    public async Task Interpolation_DoubleHole_FormatsLocaleSafe()
    {
        // Arrange
        const double value = 2.718;

        // Act
        LinkedTextUtf16 linked = $"e={value}";
        var result = linked.AsSpan().ToString();

        // Assert
        var expected = $"e={value}";
        await Assert.That(result).IsEqualTo(expected);
    }

    // ISpanFormattable: DateTime with format specifier

    [Test]
    public async Task Interpolation_DateTimeWithFormat_UsesFormatSpecifier()
    {
        // Arrange
        var date = new DateTime(2026, 1, 15, 10, 30, 0);

        // Act
        LinkedTextUtf16 linked = $"at={date:HH:mm}";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "at=10:30";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Large string hole — exercises buffer growth in AppendFormattedCore (string fast path)

    [Test]
    public async Task Interpolation_LargeStringHole_HandlesLargeContent()
    {
        // Arrange
        var largeStr = new string('z', 1000);

        // Act
        LinkedTextUtf16 linked = $"start:{largeStr}:end";
        var result = linked.AsSpan().ToString();

        // Assert
        var expected = $"start:{largeStr}:end";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Text hole with UTF-8 encoding — forces transcode via AppendFormatted(Text)

    [Test]
    public async Task Interpolation_Utf8TextHole_TranscodesCorrectly()
    {
        // Arrange
        var utf8Text = Text.FromUtf8("transcoded"u8);

        // Act
        LinkedTextUtf16 linked = $"value:{utf8Text}:end";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "value:transcoded:end";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Null string hole — exercises null check in AppendFormatted(string?)

    [Test]
    public async Task Interpolation_NullStringHole_Skipped()
    {
        // Arrange
        string? nullStr = null;

        // Act
        LinkedTextUtf16 linked = $"before{nullStr}after";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "beforeafter";
        await Assert.That(result).IsEqualTo(expected);
    }

    // ReadOnlyMemory<char> hole — exercises AppendFormatted(ReadOnlyMemory<char>)

    [Test]
    public async Task Interpolation_ReadOnlyMemoryCharHole_ZeroCopy()
    {
        // Arrange
        var mem = "memory".AsMemory();

        // Act
        LinkedTextUtf16 linked = $"from:{mem}:end";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "from:memory:end";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Format specifier on int (D5) — exercises AppendFormatted<T>(T, string?)

    [Test]
    public async Task Interpolation_IntWithD5Format_PadsCorrectly()
    {
        // Arrange
        const int value = 42;

        // Act
        LinkedTextUtf16 linked = $"id={value:D5}";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "id=00042";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Empty ReadOnlyMemory<char> hole — exercises early return

    [Test]
    public async Task Interpolation_EmptyMemoryHole_Skipped()
    {
        // Arrange
        var emptyMem = ReadOnlyMemory<char>.Empty;

        // Act
        LinkedTextUtf16 linked = $"before{emptyMem}after";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "beforeafter";
        await Assert.That(result).IsEqualTo(expected);
    }

    // ISpanFormattable retry path — value that doesn't fit initial 256-char buffer

    [Test]
    public async Task Interpolation_LargeFormattable_TriggersBufferRetry()
    {
        // Arrange — create a decimal with many digits after formatting
        const decimal value = 123456789.123456789m;

        // Act
        LinkedTextUtf16 linked = $"amount={value:N10}";
        var result = linked.AsSpan().ToString();

        // Assert
        var expected = $"amount={value:N10}";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Multiple typed holes in one interpolation

    [Test]
    public async Task Interpolation_MixedHoles_AllFormattedCorrectly()
    {
        // Arrange
        const int count = 5;
        var label = "items";
        var date = new DateTime(2026, 6, 15);

        // Act
        LinkedTextUtf16 linked = $"{count} {label} on {date:yyyy-MM-dd}";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "5 items on 2026-06-15";
        await Assert.That(result).IsEqualTo(expected);
    }

    // UTF-16 Text hole — exercises TryGetUtf16Memory fast path in AppendFormatted(Text)

    [Test]
    public async Task Interpolation_Utf16TextHole_UsesZeroCopyPath()
    {
        // Arrange
        var utf16Text = Text.From("fast-path");

        // Act
        LinkedTextUtf16 linked = $"result:{utf16Text}";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "result:fast-path";
        await Assert.That(result).IsEqualTo(expected);
    }

    // IFormattable fallback (non-ISpanFormattable) with format specifier

    [Test]
    public async Task Interpolation_FormattableWithFormat_UsesIFormattable()
    {
        // Arrange
        var obj = new FormattableOnly(42);

        // Act
        LinkedTextUtf16 linked = $"val={obj:X}";
        var result = linked.AsSpan().ToString();

        // Assert
        const string expected = "val=2A";
        await Assert.That(result).IsEqualTo(expected);
    }

    sealed class FormattableOnly(int value) : IFormattable
    {
        public string ToString(string? format, IFormatProvider? formatProvider)
            => value.ToString(format, formatProvider);
    }
}
