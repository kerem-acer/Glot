using System.Globalization;

namespace Glot.Tests;

public partial class TextTests
{
    // ToUpperInvariant — UTF-8 ASCII

    [Test]
    public async Task ToUpperInvariant_Utf8Ascii_ReturnsUppercased()
    {
        // Arrange
        const string expected = "HELLO";
        var text = Text.FromUtf8("hello"u8);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // ToLowerInvariant — UTF-8 ASCII

    [Test]
    public async Task ToLowerInvariant_Utf8Ascii_ReturnsLowercased()
    {
        // Arrange
        const string expected = "hello";
        var text = Text.FromUtf8("HELLO"u8);

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    // ToUpperInvariant — UTF-8 non-ASCII (falls through ASCII fast path)

    [Test]
    public async Task ToUpperInvariant_Utf8NonAscii_FallsThroughAsciiPath()
    {
        // Arrange
        const string expected = "CAFI";
        var text = Text.FromUtf8("cafI"u8);

        // Act — "cafI" is ASCII, so ASCII fast path handles it even with mixed case
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToUpperInvariant_Utf8Cafe_NonAsciiBypassesAsciiPath()
    {
        // Arrange — "cafe" with accent forces culture path since non-ASCII
        const string expected = "CAFE\u0301";
        var text = Text.FromUtf8("cafe\u0301"u8);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // ToUpperInvariant / ToLowerInvariant — UTF-32

    [Test]
    public async Task ToUpperInvariant_Utf32_ReturnsUppercased()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x68, 0x65, 0x6C, 0x6C, 0x6F]; // hello
        var text = Text.FromUtf32(cp);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("HELLO");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf32);
    }

    [Test]
    public async Task ToLowerInvariant_Utf32_ReturnsLowercased()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x48, 0x45, 0x4C, 0x4C, 0x4F]; // HELLO
        var text = Text.FromUtf32(cp);

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("hello");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf32);
    }

    // ToUpperInvariant / ToLowerInvariant — UTF-16 char[] backed via FromChars

    [Test]
    public async Task ToUpperInvariant_Utf16CharBacked_ReturnsUppercased()
    {
        // Arrange
        const string expected = "HELLO";
        var text = Text.FromChars("hello".AsSpan());

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task ToLowerInvariant_Utf16CharBacked_ReturnsLowercased()
    {
        // Arrange
        const string expected = "hello";
        var text = Text.FromChars("HELLO".AsSpan());

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // ToUpper / ToLower — culture-specific

    [Test]
    public async Task ToUpper_InvariantCulture_ReturnsUppercased()
    {
        // Arrange
        const string expected = "HELLO";
        var text = Text.From("hello");

        // Act
        var result = text.ToUpper(CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToLower_InvariantCulture_ReturnsLowercased()
    {
        // Arrange
        const string expected = "hello";
        var text = Text.From("HELLO");

        // Act
        var result = text.ToLower(CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // ToUpper / ToLower — culture-specific empty

    [Test]
    public async Task ToUpper_Empty_ReturnsEmpty()
    {
        // Act
        var result = Text.Empty.ToUpper(CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    [Test]
    public async Task ToLower_Empty_ReturnsEmpty()
    {
        // Act
        var result = Text.Empty.ToLower(CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    // Pooled culture-specific variants

    [Test]
    public async Task ToUpperPooled_InvariantCulture_ReturnsOwnedText()
    {
        // Arrange
        const string expected = "HELLO";
        var text = Text.From("hello");

        // Act
        using var result = text.ToUpperPooled(CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToLowerPooled_InvariantCulture_ReturnsOwnedText()
    {
        // Arrange
        const string expected = "hello";
        var text = Text.From("HELLO");

        // Act
        using var result = text.ToLowerPooled(CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    // Pooled empty

    [Test]
    public async Task ToUpperPooled_Empty_ReturnsOwnedTextEmpty()
    {
        // Act
        var result = Text.Empty.ToUpperPooled(CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result).IsSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task ToLowerPooled_Empty_ReturnsOwnedTextEmpty()
    {
        // Act
        var result = Text.Empty.ToLowerPooled(CultureInfo.InvariantCulture);

        // Assert
        await Assert.That(result).IsSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task ToUpperInvariantPooled_Empty_ReturnsOwnedTextEmpty()
    {
        // Act
        var result = Text.Empty.ToUpperInvariantPooled();

        // Assert
        await Assert.That(result).IsSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task ToLowerInvariantPooled_Empty_ReturnsOwnedTextEmpty()
    {
        // Act
        var result = Text.Empty.ToLowerInvariantPooled();

        // Assert
        await Assert.That(result).IsSameReferenceAs(OwnedText.Empty);
    }

    // Pooled no-change — already correct case

    [Test]
    public async Task ToUpperInvariantPooled_AlreadyUpper_ReturnsCopyWithSameContent()
    {
        // Arrange
        const string expected = "HELLO";
        var text = Text.FromUtf8("HELLO"u8);

        // Act
        using var result = text.ToUpperInvariantPooled();

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToLowerInvariantPooled_Utf8AlreadyLower_ReturnsCopy()
    {
        // Arrange
        const string expected = "hello";
        var text = Text.FromUtf8("hello"u8);

        // Act
        using var result = text.ToLowerInvariantPooled();

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    // UTF-32 ASCII upper/lower — exercises ToggleCaseAsciiUtf32 path

    [Test]
    public async Task ToUpperInvariant_Utf32Ascii_UsesToggleCaseAsciiPath()
    {
        // Arrange — all ASCII code points
        ReadOnlySpan<int> cp = [0x61, 0x62, 0x63]; // abc
        var text = Text.FromUtf32(cp);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("ABC");
        await Assert.That(result.Encoding).IsEqualTo(TextEncoding.Utf32);
    }

    [Test]
    public async Task ToLowerInvariant_Utf32Ascii_UsesToggleCaseAsciiPath()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x41, 0x42, 0x43]; // ABC
        var text = Text.FromUtf32(cp);

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task ToUpperInvariantPooled_Utf32Ascii_UsesToggleCasePooledPath()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x61, 0x62, 0x63]; // abc
        var text = Text.FromUtf32(cp);

        // Act
        using var result = text.ToUpperInvariantPooled();

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("ABC");
    }

    [Test]
    public async Task ToLowerInvariantPooled_Utf32Ascii_UsesToggleCasePooledPath()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x41, 0x42, 0x43]; // ABC
        var text = Text.FromUtf32(cp);

        // Act
        using var result = text.ToLowerInvariantPooled();

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("abc");
    }

    // UTF-32 already correct case — no-change fast path

    [Test]
    public async Task ToUpperInvariant_Utf32AlreadyUpper_ReturnsThis()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x41, 0x42, 0x43]; // ABC
        var text = Text.FromUtf32(cp);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("ABC");
    }

    [Test]
    public async Task ToLowerInvariant_Utf32AlreadyLower_ReturnsThis()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x61, 0x62, 0x63]; // abc
        var text = Text.FromUtf32(cp);

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("abc");
    }

    // Mixed case (trailing unchanged run after case change)

    [Test]
    public async Task ToUpperInvariant_MixedCaseTrailingUnchanged_Works()
    {
        // Arrange — 'a' changes to 'A', 'B' and 'C' are already uppercase (trailing unchanged run)
        const string expected = "ABC";
        var text = Text.FromUtf8("aBC"u8);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToLowerInvariant_MixedCaseTrailingUnchanged_Works()
    {
        // Arrange — 'A' changes to 'a', 'b' and 'c' are already lowercase (trailing unchanged run)
        const string expected = "abc";
        var text = Text.FromUtf8("Abc"u8);

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // Already correct case for each encoding — no-change fast path

    [Test]
    public async Task ToUpperInvariant_Utf8AlreadyUpper_ReturnsThis()
    {
        // Arrange
        const string expected = "HELLO";
        var text = Text.FromUtf8("HELLO"u8);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToLowerInvariant_Utf8AlreadyLower_ReturnsThis()
    {
        // Arrange
        const string expected = "hello";
        var text = Text.FromUtf8("hello"u8);

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToUpperInvariant_Utf16AlreadyUpper_ReturnsThis()
    {
        // Arrange
        const string expected = "HELLO";
        var text = Text.FromChars("HELLO".AsSpan());

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToLowerInvariant_Utf16AlreadyLower_ReturnsThis()
    {
        // Arrange
        const string expected = "hello";
        var text = Text.FromChars("hello".AsSpan());

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // Non-ASCII falls through ASCII fast path

    [Test]
    public async Task ToUpperInvariant_Utf8CafeWithAccent_NonAsciiBypassesAsciiPath()
    {
        // Arrange — "caf\u00e9" contains non-ASCII byte, so ASCII fast path returns false
        const string expected = "CAF\u00c9";
        var text = Text.FromUtf8("caf\u00e9"u8);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToLowerInvariant_Utf8CafeWithAccent_NonAsciiBypassesAsciiPath()
    {
        // Arrange
        const string expected = "caf\u00e9";
        var text = Text.FromUtf8("CAF\u00c9"u8);

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo(expected);
    }

    // UTF-32 non-ASCII falls through ASCII fast path

    [Test]
    public async Task ToUpperInvariant_Utf32NonAscii_FallsToCulturePath()
    {
        // Arrange — code point 0xE9 (e-acute) is non-ASCII, so UTF-32 ASCII fast path returns false
        ReadOnlySpan<int> cp = [0x63, 0x61, 0x66, 0xE9]; // cafe with accent
        var text = Text.FromUtf32(cp);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("CAF\u00c9");
    }

    // UTF-16 non-ASCII pooled

    [Test]
    public async Task ToUpperInvariantPooled_Utf16NonAscii_Uppercases()
    {
        // Arrange
        const string expected = "CAF\u00c9";
        var text = Text.FromChars("caf\u00e9".AsSpan());

        // Act
        using var result = text.ToUpperInvariantPooled();

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task ToLowerInvariantPooled_Utf16NonAscii_Lowercases()
    {
        // Arrange
        const string expected = "caf\u00e9";
        var text = Text.FromChars("CAF\u00c9".AsSpan());

        // Act
        using var result = text.ToLowerInvariantPooled();

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo(expected);
    }

    // UTF-32 pooled already correct

    [Test]
    public async Task ToUpperInvariantPooled_Utf32AlreadyUpper_ReturnsCopy()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x41, 0x42, 0x43]; // ABC
        var text = Text.FromUtf32(cp);

        // Act
        using var result = text.ToUpperInvariantPooled();

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("ABC");
    }

    [Test]
    public async Task ToLowerInvariantPooled_Utf32AlreadyLower_ReturnsCopy()
    {
        // Arrange
        ReadOnlySpan<int> cp = [0x61, 0x62, 0x63]; // abc
        var text = Text.FromUtf32(cp);

        // Act
        using var result = text.ToLowerInvariantPooled();

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("abc");
    }
}
