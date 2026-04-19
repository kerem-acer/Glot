namespace Glot.Tests;

public partial class TextTests
{
    // Concat with explicit encoding

    [Test]
    public async Task Concat_ExplicitEncoding_ForcesTranscode()
    {
        // Arrange
        var utf8A = Text.FromUtf8("Hello"u8);
        var utf16B = Text.From(" World");

        // Act
        var result = Text.Concat([utf8A, utf16B], TextEncoding.Utf8);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // Concat single value same encoding

    [Test]
    public async Task Concat_SingleValueSameEncoding_ReturnsSameValue()
    {
        // Arrange
        var value = Text.FromUtf8("Hello"u8);

        // Act
        var result = Text.Concat([value], TextEncoding.Utf8);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // Concat with empty values mixed in

    [Test]
    public async Task Concat_WithEmptyValuesMixedIn_ConcatenatesNonEmpty()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        var result = Text.Concat(Text.Empty, text, Text.Empty);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    // ConcatPooled with values

    [Test]
    public async Task ConcatPooled_WithValues_ReturnsNonNullOwnedText()
    {
        // Arrange
        var a = Text.From("Hello");
        var b = Text.From(" World");

        // Act
        using var result = Text.ConcatPooled(a, b);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo("Hello World");
    }

    // ConcatPooled with explicit encoding

    [Test]
    public async Task ConcatPooled_ExplicitEncoding_ForcesSpecificEncoding()
    {
        // Arrange
        var utf8A = Text.FromUtf8("Hello"u8);
        var utf16B = Text.From(" World");

        // Act
        using var result = Text.ConcatPooled([utf8A, utf16B], TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Text.ToString()).IsEqualTo("Hello World");
        await Assert.That(result.Text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // ConcatPooled all empty

    [Test]
    public async Task ConcatPooled_AllEmpty_ReturnsNull()
    {
        // Act
        var result = Text.ConcatPooled(Text.Empty, Text.Empty);

        // Assert
        await Assert.That(result).IsNull();
    }

    // Concat cross-encoding two values same encoding fast path

    [Test]
    public async Task Concat_TwoValuesSameEncoding_UsesFastPath()
    {
        // Arrange
        var a = Text.FromUtf8("Hello"u8);
        var b = Text.FromUtf8(" World"u8);

        // Act
        var result = Text.Concat(a, b);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(result.ByteLength).IsEqualTo(11);
    }

    // operator+ — left empty returns right

    [Test]
    public async Task OperatorPlus_LeftEmpty_ReturnsRight()
    {
        // Arrange
        var right = Text.From("Hello");

        // Act
        var result = Text.Empty + right;

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    // operator+ — right empty returns left

    [Test]
    public async Task OperatorPlus_RightEmpty_ReturnsLeft()
    {
        // Arrange
        var left = Text.From("Hello");

        // Act
        var result = left + Text.Empty;

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }
}
