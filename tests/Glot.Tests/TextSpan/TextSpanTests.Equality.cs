namespace Glot.Tests;

public partial class TextSpanTests
{
    [Test]
    [Arguments("Hello", TextEncoding.Utf8)]
    [Arguments("Hello", TextEncoding.Utf16)]
    [Arguments("Hello", TextEncoding.Utf32)]
    [Arguments("café", TextEncoding.Utf8)]
    [Arguments("🎉🎊", TextEncoding.Utf8)]
    public async Task Equals_SameEncodingSameContent_ReturnsTrue(string input, TextEncoding encoding)
    {
        // Arrange
        var bytes1 = TestHelpers.Encode(input, encoding);
        var bytes2 = TestHelpers.Encode(input, encoding);
        var span1 = new TextSpan(bytes1, encoding);
        var span2 = new TextSpan(bytes2, encoding);

        // Act
        var result = span1.Equals(span2);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Equals_SameEncodingDifferentContent_ReturnsFalse()
    {
        // Arrange
        var span1 = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("World"u8, TextEncoding.Utf8);

        // Act
        var result = span1.Equals(span2);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    [Arguments("Hello")]
    [Arguments("café")]
    [Arguments("日本語")]
    [Arguments("🎉")]
    [Arguments("")]
    public async Task Equals_CrossEncodingUtf8Utf16_ReturnsTrue(string input)
    {
        // Arrange
        var utf8 = new TextSpan(TestHelpers.Encode(input, TextEncoding.Utf8), TextEncoding.Utf8);
        var utf16 = new TextSpan(TestHelpers.Encode(input, TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = utf8.Equals(utf16);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    [Arguments("Hello")]
    [Arguments("🎉")]
    public async Task Equals_CrossEncodingUtf8Utf32_ReturnsTrue(string input)
    {
        // Arrange
        var utf8 = new TextSpan(TestHelpers.Encode(input, TextEncoding.Utf8), TextEncoding.Utf8);
        var utf32 = new TextSpan(TestHelpers.Encode(input, TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = utf8.Equals(utf32);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    [Arguments("Hello")]
    [Arguments("🎉")]
    public async Task Equals_CrossEncodingUtf16Utf32_ReturnsTrue(string input)
    {
        // Arrange
        var utf16 = new TextSpan(TestHelpers.Encode(input, TextEncoding.Utf16), TextEncoding.Utf16);
        var utf32 = new TextSpan(TestHelpers.Encode(input, TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = utf16.Equals(utf32);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Equals_CrossEncodingDifferentContent_ReturnsFalse()
    {
        // Arrange
        var utf8 = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var utf16 = new TextSpan(TestHelpers.Encode("World", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = utf8.Equals(utf16);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public Task Equals_CrossEncodingDifferentLength_ReturnsFalse()
    {
        // Arrange
        var shorter = new TextSpan("Hi"u8, TextEncoding.Utf8);
        var longer = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var shorterEqualsLonger = shorter.Equals(longer);
        var longerEqualsShorter = longer.Equals(shorter);

        // Assert
        return Verify(new { shorterEqualsLonger, longerEqualsShorter });
    }

    [Test]
    public async Task Equals_ReadOnlySpanByte_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.Equals("Hello"u8);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Equals_ReadOnlySpanChar_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Equals_ReadOnlySpanCharEmoji_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("🎉"u8, TextEncoding.Utf8);

        // Act
        var result = span.Equals("🎉".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }


    [Test]
    public async Task OperatorEquals_SameContent_ReturnsTrue()
    {
        // Arrange
        var span1 = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span1 == span2;

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task OperatorEquals_DifferentContent_ReturnsFalse()
    {
        // Arrange
        var span1 = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("World"u8, TextEncoding.Utf8);

        // Act
        var result = span1 == span2;

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task OperatorNotEquals_DifferentContent_ReturnsTrue()
    {
        // Arrange
        var span1 = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("World"u8, TextEncoding.Utf8);

        // Act
        var result = span1 != span2;

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task OperatorNotEquals_SameContent_ReturnsFalse()
    {
        // Arrange
        var span1 = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span1 != span2;

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    [Arguments("Hello")]
    [Arguments("café")]
    [Arguments("日本語")]
    [Arguments("🎉")]
    public Task GetHashCode_SameContentDifferentEncodings_ReturnsSameHash(string input)
    {
        // Arrange & Act
        var hash8 = new TextSpan(TestHelpers.Encode(input, TextEncoding.Utf8), TextEncoding.Utf8).GetHashCode();
        var hash16 = new TextSpan(TestHelpers.Encode(input, TextEncoding.Utf16), TextEncoding.Utf16).GetHashCode();
        var hash32 = new TextSpan(TestHelpers.Encode(input, TextEncoding.Utf32), TextEncoding.Utf32).GetHashCode();

        // Assert
        return Verify(new { hash8EqualHash16 = hash8 == hash16, hash8EqualHash32 = hash8 == hash32 }).UseParameters(input);
    }

    [Test]
    public async Task GetHashCode_DifferentContent_ReturnsDifferentHash()
    {
        // Arrange & Act
        var hash1 = new TextSpan("Hello"u8, TextEncoding.Utf8).GetHashCode();
        var hash2 = new TextSpan("World"u8, TextEncoding.Utf8).GetHashCode();

        // Assert
        await Assert.That(hash1).IsNotEqualTo(hash2);
    }

    [Test]
    public async Task GetHashCode_EmptySpansDifferentEncodings_ReturnsSameHash()
    {
        // Arrange & Act
        var hash8 = new TextSpan([], TextEncoding.Utf8).GetHashCode();
        var hash16 = new TextSpan([], TextEncoding.Utf16).GetHashCode();

        // Assert
        await Assert.That(hash8).IsEqualTo(hash16);
    }

    [Test]
    public async Task CompareTo_EqualContent_ReturnsZero()
    {
        // Arrange
        var span1 = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span1.CompareTo(span2);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task CompareTo_LessThan_ReturnsNegative()
    {
        // Arrange
        var span1 = new TextSpan("A"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("B"u8, TextEncoding.Utf8);

        // Act
        var result = span1.CompareTo(span2);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_GreaterThan_ReturnsPositive()
    {
        // Arrange
        var span1 = new TextSpan("B"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("A"u8, TextEncoding.Utf8);

        // Act
        var result = span1.CompareTo(span2);

        // Assert
        await Assert.That(result).IsGreaterThan(0);
    }

    [Test]
    public async Task CompareTo_ShorterPrefix_ReturnsNegative()
    {
        // Arrange
        var span1 = new TextSpan("AB"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("ABC"u8, TextEncoding.Utf8);

        // Act
        var result = span1.CompareTo(span2);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_LongerPrefix_ReturnsPositive()
    {
        // Arrange
        var span1 = new TextSpan("ABC"u8, TextEncoding.Utf8);
        var span2 = new TextSpan("AB"u8, TextEncoding.Utf8);

        // Act
        var result = span1.CompareTo(span2);

        // Assert
        await Assert.That(result).IsGreaterThan(0);
    }

    [Test]
    public async Task CompareTo_CrossEncoding_ReturnsZero()
    {
        // Arrange
        var utf8 = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var utf16 = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = utf8.CompareTo(utf16);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task CompareTo_BothEmpty_ReturnsZero()
    {
        // Arrange
        var span1 = new TextSpan([], TextEncoding.Utf8);
        var span2 = new TextSpan([], TextEncoding.Utf16);

        // Act
        var result = span1.CompareTo(span2);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task CompareTo_EmptyVsNonEmpty_ReturnsNegative()
    {
        // Arrange
        var empty = new TextSpan([], TextEncoding.Utf8);
        var nonEmpty = new TextSpan("A"u8, TextEncoding.Utf8);

        // Act
        var result = empty.CompareTo(nonEmpty);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    // CompareTo cross-encoding — both-non-UTF-8 streaming path (CompareBothTranscoded)

    [Test]
    public async Task CompareTo_Utf16Vs_Utf32_SameContent_ReturnsZero()
    {
        // Arrange — same content, both non-UTF-8: hits CompareBothTranscoded streaming.
        var utf16 = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf16), TextEncoding.Utf16);
        var utf32 = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = utf16.CompareTo(utf32);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task CompareTo_Utf16Vs_Utf32_DifferentContent_ReturnsSignedByRune()
    {
        // Arrange — "apple" < "apply" by the fifth rune.
        var utf16Apple = new TextSpan(TestHelpers.Encode("apple", TextEncoding.Utf16), TextEncoding.Utf16);
        var utf32Apply = new TextSpan(TestHelpers.Encode("apply", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = utf16Apple.CompareTo(utf32Apply);

        // Assert — 'e' (0x65) < 'y' (0x79) → negative.
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_Utf16Vs_Utf32_DifferentLength_PrefixMatch_ReturnsSign()
    {
        // Arrange — shared prefix, one longer.
        var shortSide = new TextSpan(TestHelpers.Encode("Hi", TextEncoding.Utf16), TextEncoding.Utf16);
        var longSide = new TextSpan(TestHelpers.Encode("Hi there", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var shortVsLong = shortSide.CompareTo(longSide);
        var longVsShort = longSide.CompareTo(shortSide);

        // Assert
        await Assert.That(shortVsLong).IsLessThan(0);
        await Assert.That(longVsShort).IsGreaterThan(0);
    }

    [Test]
    public async Task CompareTo_Utf16Vs_Utf32_SurrogatePair_OrdersByRune()
    {
        // Arrange — U+10000 (supplementary, surrogate pair in UTF-16) vs U+E000 (BMP).
        // Byte-level UTF-16 LE would say U+10000 (D8 00 ...) < U+E000 (E0 00);
        // rune-level says U+10000 > U+E000. The streaming path transcodes to UTF-8
        // where byte order preserves scalar order.
        var supplementary = new TextSpan(TestHelpers.Encode("𐀀", TextEncoding.Utf16), TextEncoding.Utf16);
        var bmp = new TextSpan(TestHelpers.Encode("", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = supplementary.CompareTo(bmp);

        // Assert — U+10000 (65536) > U+E000 (57344).
        await Assert.That(result).IsGreaterThan(0);
    }

    [Test]
    public async Task CompareTo_Utf16Vs_Utf32_MultiChunk_SharedPrefix_ReturnsZero()
    {
        // Arrange — content larger than one 512-byte UTF-8 chunk, same on both sides.
        // Forces the streaming loop to process multiple chunks from each side.
        var text = new string('A', 2000);
        var utf16 = new TextSpan(TestHelpers.Encode(text, TextEncoding.Utf16), TextEncoding.Utf16);
        var utf32 = new TextSpan(TestHelpers.Encode(text, TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = utf16.CompareTo(utf32);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task CompareTo_Utf16Vs_Utf32_MultiChunk_MidStreamDiff_ReturnsSign()
    {
        // Arrange — 2000 identical chars then diverge at position 1500.
        var a = new string('A', 1500) + "X" + new string('A', 499);
        var b = new string('A', 1500) + "Y" + new string('A', 499);
        var utf16 = new TextSpan(TestHelpers.Encode(a, TextEncoding.Utf16), TextEncoding.Utf16);
        var utf32 = new TextSpan(TestHelpers.Encode(b, TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = utf16.CompareTo(utf32);

        // Assert — 'X' (0x58) < 'Y' (0x59) → negative.
        await Assert.That(result).IsLessThan(0);
    }

    // Equals cross-encoding — same rune count different content (length short-circuit doesn't fire)

    [Test]
    public async Task Equals_Utf16Vs_Utf32_SameRuneCount_DifferentContent_ReturnsFalse()
    {
        // Arrange — both 5 runes but different content. Length short-circuit doesn't apply;
        // streaming EqualsCrossEncoding must walk and detect the mismatch.
        var utf16 = new TextSpan(TestHelpers.Encode("apple", TextEncoding.Utf16), TextEncoding.Utf16);
        var utf32 = new TextSpan(TestHelpers.Encode("apply", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = utf16.Equals(utf32);

        // Assert
        await Assert.That(result).IsFalse();
    }
}
