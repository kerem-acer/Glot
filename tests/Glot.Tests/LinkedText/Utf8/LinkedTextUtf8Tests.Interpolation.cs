namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    // Interpolation with int — exercises AppendFormattedCore<T> for IUtf8SpanFormattable

    [Test]
    public async Task Interpolation_WithInt_FormatsCorrectly()
    {
        // Arrange
        const int value = 42;

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"count={value}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "count=42";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with double — IUtf8SpanFormattable

    [Test]
    public async Task Interpolation_WithDouble_FormatsCorrectly()
    {
        // Arrange
        const double value = 3.14;

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"pi={value}");
        var result = owned.AsSpan().ToString();

        // Assert
        var expected = $"pi={value}";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with DateTime — IUtf8SpanFormattable with format specifier

    [Test]
    public async Task Interpolation_WithDateTime_FormatsCorrectly()
    {
        // Arrange
        var date = new DateTime(2026, 4, 19);

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"date={date:yyyy-MM-dd}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "date=2026-04-19";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with empty string literal — exercises AppendLiteral with empty

    [Test]
    public async Task Interpolation_EmptyLiteral_ProducesCorrectResult()
    {
        // Arrange
        const int value = 7;

        // Act — empty literal between formatted holes
        using var owned = OwnedLinkedTextUtf8.Create($"{value}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "7";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with multiple segments — multiple formatted values

    [Test]
    public async Task Interpolation_MultipleValues_FormatsAll()
    {
        // Arrange
        const int a = 1;
        const int b = 2;
        const int c = 3;

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"{a}+{b}={c}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "1+2=3";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with large formatted value — forces format buffer retry/growth

    [Test]
    public async Task Interpolation_LargeFormattedValue_ForcesBufferGrowth()
    {
        // Arrange — a string longer than 256 bytes, passed as a string hole
        var largeStr = new string('A', 300);

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"prefix{largeStr}suffix");
        var result = owned.AsSpan().ToString();

        // Assert
        var expected = $"prefix{largeStr}suffix";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with Text hole — exercises AppendFormatted(Text)

    [Test]
    public async Task Interpolation_WithUtf8TextHole_ZeroCopy()
    {
        // Arrange
        var text = Text.FromUtf8("world"u8);

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"hello {text}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "hello world";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with UTF-16 Text hole — exercises transcode path

    [Test]
    public async Task Interpolation_WithUtf16TextHole_Transcodes()
    {
        // Arrange
        var text = Text.From("world");

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"hello {text}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "hello world";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with TextSpan hole — exercises AppendFormatted(TextSpan)

    [Test]
    public async Task Interpolation_WithTextSpanHole_Transcodes()
    {
        // Arrange
        var text = Text.From("world");
        var span = text.AsSpan();

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"hello {span}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "hello world";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with null string hole — exercises null early return

    [Test]
    public async Task Interpolation_WithNullStringHole_Skipped()
    {
        // Arrange
        string? nullStr = null;

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"hello{nullStr}world");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "helloworld";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with empty Text hole — exercises empty early return

    [Test]
    public async Task Interpolation_WithEmptyTextHole_Skipped()
    {
        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"hello{Text.Empty}world");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "helloworld";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with ReadOnlyMemory<byte> hole — exercises AppendFormatted(ReadOnlyMemory<byte>)

    [Test]
    public async Task Interpolation_WithMemoryHole_ZeroCopy()
    {
        // Arrange
        ReadOnlyMemory<byte> mem = "world"u8.ToArray();

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"hello {mem}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "hello world";
        await Assert.That(result).IsEqualTo(expected);
    }

    // Interpolation with format specifier on int — exercises AppendFormatted<T>(T, string?)

    [Test]
    public async Task Interpolation_WithFormatSpecifier_FormatsCorrectly()
    {
        // Arrange
        const int value = 42;

        // Act
        using var owned = OwnedLinkedTextUtf8.Create($"val={value:D5}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "val=00042";
        await Assert.That(result).IsEqualTo(expected);
    }
}
