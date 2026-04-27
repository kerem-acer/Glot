namespace Glot.Tests;

public partial class OwnedTextTests
{
    [Test]
    public async Task Equals_SameContentSameEncoding_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello";
        using var a = OwnedText.FromUtf8("Hello"u8)!;
        using var b = OwnedText.FromUtf8("Hello"u8)!;

        // Act
        var result = a.Equals(b);

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(a.Equals(content)).IsTrue();
    }

    [Test]
    public async Task Equals_SameContentDifferentEncoding_ReturnsTrue()
    {
        // Arrange
        using var utf8 = OwnedText.FromUtf8("Hello"u8)!;
        using var utf16 = OwnedText.FromChars("Hello".AsSpan())!;

        // Act
        var result = utf8.Equals(utf16);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Equals_DifferentContent_ReturnsFalse()
    {
        // Arrange
        using var a = OwnedText.FromUtf8("Hello"u8)!;
        using var b = OwnedText.FromUtf8("World"u8)!;

        // Act & Assert
        await Assert.That(a.Equals(b)).IsFalse();
    }

    [Test]
    public async Task Equals_NullOwnedText_ReturnsFalse()
    {
        // Arrange
        using var a = OwnedText.FromUtf8("Hello"u8)!;

        // Act & Assert
        await Assert.That(a.Equals((OwnedText?)null)).IsFalse();
    }

    [Test]
    public async Task Equals_NullString_ReturnsFalse()
    {
        // Arrange
        using var a = OwnedText.FromUtf8("Hello"u8)!;

        // Act & Assert
        await Assert.That(a.Equals((string?)null)).IsFalse();
    }

    [Test]
    public async Task Equals_AgainstText_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello";
        using var owned = OwnedText.FromUtf8("Hello"u8)!;
        var text = Text.From(content);

        // Act & Assert
        await Assert.That(owned.Equals(text)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstSpans_ReturnsTrue()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("Hello"u8)!;

        // Act & Assert
        await Assert.That(owned.Equals("Hello"u8, TextEncoding.Utf8)).IsTrue();
        await Assert.That(owned.Equals("Hello".AsSpan())).IsTrue();
    }

    [Test]
    public async Task Equals_Object_DispatchesByType()
    {
        // Arrange
        const string content = "Hello";
        using var owned = OwnedText.FromUtf8("Hello"u8)!;

        // Act & Assert
        await Assert.That(owned.Equals((object)Text.From(content))).IsTrue();
        await Assert.That(owned.Equals((object)content)).IsTrue();
        await Assert.That(owned.Equals(new object())).IsFalse();
    }

    [Test]
    public async Task GetHashCode_SameUtf8Content_MatchesText()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("Hello"u8)!;
        var text = Text.FromUtf8("Hello"u8);

        // Act & Assert — both wrap the same Text.GetHashCode logic
        await Assert.That(owned.GetHashCode()).IsEqualTo(text.GetHashCode());
    }

    [Test]
    public async Task Operators_Equality_NullSafe()
    {
        // Arrange
        using var a = OwnedText.FromUtf8("Hello"u8)!;
        using var b = OwnedText.FromUtf8("Hello"u8)!;
        OwnedText? nullA = null;
        OwnedText? nullB = null;

        // Act & Assert
        await Assert.That(a == b).IsTrue();
        await Assert.That(a != b).IsFalse();
        await Assert.That(a == nullA).IsFalse();
        await Assert.That(nullA == nullB).IsTrue();
    }

    [Test]
    public async Task CompareTo_LessThan_ReturnsNegative()
    {
        // Arrange
        using var a = OwnedText.FromUtf8("A"u8)!;
        using var b = OwnedText.FromUtf8("B"u8)!;

        // Act
        var result = a.CompareTo(b);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_Null_ReturnsPositive()
    {
        // Arrange
        using var a = OwnedText.FromUtf8("A"u8)!;

        // Act
        var result = a.CompareTo((OwnedText?)null);

        // Assert
        await Assert.That(result).IsGreaterThan(0);
    }

    [Test]
    public async Task CompareTo_AgainstText_OrdersByRune()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("A"u8)!;
        var text = Text.FromUtf8("B"u8);

        // Act
        var result = owned.CompareTo(text);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }
}
