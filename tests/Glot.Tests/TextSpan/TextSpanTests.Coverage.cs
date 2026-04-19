namespace Glot.Tests;

public partial class TextSpanTests
{
    // GetHashCode — UTF-8 non-empty returns non-zero

    [Test]
    public async Task GetHashCode_Utf8NonEmpty_ReturnsNonZero()
    {
        // Arrange & Act
        var hash = new TextSpan("Hello"u8, TextEncoding.Utf8).GetHashCode();

        // Assert
        await Assert.That(hash).IsNotEqualTo(0);
    }

    // GetHashCode — UTF-16 non-empty returns non-zero

    [Test]
    public async Task GetHashCode_Utf16NonEmpty_ReturnsNonZero()
    {
        // Arrange & Act
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        var hash = new TextSpan(bytes, TextEncoding.Utf16).GetHashCode();

        // Assert
        await Assert.That(hash).IsNotEqualTo(0);
    }

    // GetHashCode — UTF-32 non-empty returns non-zero

    [Test]
    public async Task GetHashCode_Utf32NonEmpty_ReturnsNonZero()
    {
        // Arrange & Act
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var hash = new TextSpan(bytes, TextEncoding.Utf32).GetHashCode();

        // Assert
        await Assert.That(hash).IsNotEqualTo(0);
    }

    // GetHashCode — empty returns 0

    [Test]
    public async Task GetHashCode_Empty_ReturnsZero()
    {
        // Arrange & Act
        var hash = new TextSpan([], TextEncoding.Utf8).GetHashCode();

        // Assert
        await Assert.That(hash).IsEqualTo(0);
    }

    // CompareTo — same content returns 0

    [Test]
    public async Task CompareTo_SameUtf16Content_ReturnsZero()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        var span1 = new TextSpan(bytes, TextEncoding.Utf16);
        var span2 = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var result = span1.CompareTo(span2);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // CompareTo — different content returns non-zero

    [Test]
    public async Task CompareTo_DifferentUtf16Content_ReturnsNonZero()
    {
        // Arrange
        var bytesA = TestHelpers.Encode("Apple", TextEncoding.Utf16);
        var bytesB = TestHelpers.Encode("Banana", TextEncoding.Utf16);
        var span1 = new TextSpan(bytesA, TextEncoding.Utf16);
        var span2 = new TextSpan(bytesB, TextEncoding.Utf16);

        // Act
        var result = span1.CompareTo(span2);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    // CompareTo — cross-encoding UTF-8 vs UTF-16 same content

    [Test]
    public async Task CompareTo_CrossEncodingUtf8VsUtf16_SameContent_ReturnsZero()
    {
        // Arrange
        var utf8 = new TextSpan("café"u8, TextEncoding.Utf8);
        var utf16Bytes = TestHelpers.Encode("café", TextEncoding.Utf16);
        var utf16 = new TextSpan(utf16Bytes, TextEncoding.Utf16);

        // Act
        var result = utf8.CompareTo(utf16);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // CompareTo — cross-encoding UTF-16 vs UTF-32

    [Test]
    public async Task CompareTo_CrossEncodingUtf16VsUtf32_SameContent_ReturnsZero()
    {
        // Arrange
        var utf16Bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        var utf32Bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var utf16 = new TextSpan(utf16Bytes, TextEncoding.Utf16);
        var utf32 = new TextSpan(utf32Bytes, TextEncoding.Utf32);

        // Act — neither side is UTF-8, exercises CompareBothTranscoded
        var result = utf16.CompareTo(utf32);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // CompareTo — cross-encoding UTF-16 vs UTF-32 different content

    [Test]
    public async Task CompareTo_CrossEncodingUtf16VsUtf32_DifferentContent_ReturnsNonZero()
    {
        // Arrange
        var utf16Bytes = TestHelpers.Encode("Apple", TextEncoding.Utf16);
        var utf32Bytes = TestHelpers.Encode("Banana", TextEncoding.Utf32);
        var utf16 = new TextSpan(utf16Bytes, TextEncoding.Utf16);
        var utf32 = new TextSpan(utf32Bytes, TextEncoding.Utf32);

        // Act
        var result = utf16.CompareTo(utf32);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    // Trim on all-whitespace text — returns empty

    [Test]
    public async Task Trim_AllWhitespace_ReturnsEmpty()
    {
        // Arrange & Act
        var span = new TextSpan("   \t\n  "u8, TextEncoding.Utf8);
        var isEmpty = span.Trim().IsEmpty;

        // Assert
        await Assert.That(isEmpty).IsTrue();
    }

    // TrimStart on UTF-16

    [Test]
    public async Task TrimStart_Utf16_RemovesLeadingWhitespace()
    {
        // Arrange
        var bytes = TestHelpers.Encode("  Hello", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var result = span.TrimStart().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    // TrimEnd on UTF-16

    [Test]
    public async Task TrimEnd_Utf16_RemovesTrailingWhitespace()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello  ", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);

        // Act
        var result = span.TrimEnd().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    // TrimStart on UTF-32

    [Test]
    public async Task TrimStart_Utf32_RemovesLeadingWhitespace()
    {
        // Arrange
        var bytes = TestHelpers.Encode("  Hello", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var result = span.TrimStart().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    // TrimEnd on UTF-32

    [Test]
    public async Task TrimEnd_Utf32_RemovesTrailingWhitespace()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello  ", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);

        // Act
        var result = span.TrimEnd().Equals("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    // EncodeToUtf16 from UTF-32 — verify chars written

    [Test]
    public async Task EncodeToUtf16_FromUtf32_WritesCorrectChars()
    {
        // Arrange
        var utf32Bytes = TestHelpers.Encode("café", TextEncoding.Utf32);
        var span = new TextSpan(utf32Bytes, TextEncoding.Utf32);
        var destination = new char[10];

        // Act
        var charsWritten = span.EncodeToUtf16(destination);
        var result = new string(destination, 0, charsWritten);

        // Assert
        const string expected = "café";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(charsWritten).IsEqualTo(4);
    }

    // EncodeToUtf8 from UTF-32 — verify bytes written

    [Test]
    public async Task EncodeToUtf8_FromUtf32_WritesCorrectBytes()
    {
        // Arrange
        var utf32Bytes = TestHelpers.Encode("café", TextEncoding.Utf32);
        var span = new TextSpan(utf32Bytes, TextEncoding.Utf32);
        var destination = new byte[20];

        // Act
        var bytesWritten = span.EncodeToUtf8(destination);
        var result = System.Text.Encoding.UTF8.GetString(destination, 0, bytesWritten);

        // Assert
        const string expected = "café";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(bytesWritten).IsEqualTo(5); // 'c'=1, 'a'=1, 'f'=1, 'é'=2
    }

    // EncodeToUtf16 from UTF-8 — verify chars written

    [Test]
    public async Task EncodeToUtf16_FromUtf8_WritesCorrectChars()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var destination = new char[10];

        // Act
        var charsWritten = span.EncodeToUtf16(destination);
        var result = new string(destination, 0, charsWritten);

        // Assert
        const string expected = "Hello";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(charsWritten).IsEqualTo(5);
    }

    // EncodeToUtf8 from UTF-16 — verify bytes written

    [Test]
    public async Task EncodeToUtf8_FromUtf16_WritesCorrectBytes()
    {
        // Arrange
        var utf16Bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        var span = new TextSpan(utf16Bytes, TextEncoding.Utf16);
        var destination = new byte[20];

        // Act
        var bytesWritten = span.EncodeToUtf8(destination);
        var result = System.Text.Encoding.UTF8.GetString(destination, 0, bytesWritten);

        // Assert
        const string expected = "Hello";
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(bytesWritten).IsEqualTo(5);
    }

    // EncodeToUtf16 — UTF-16 self-copy path

    [Test]
    public async Task EncodeToUtf16_FromUtf16_CopiesDirect()
    {
        // Arrange
        var utf16Bytes = TestHelpers.Encode("Test", TextEncoding.Utf16);
        var span = new TextSpan(utf16Bytes, TextEncoding.Utf16);
        var destination = new char[10];

        // Act
        var charsWritten = span.EncodeToUtf16(destination);
        var result = new string(destination, 0, charsWritten);

        // Assert
        const string expected = "Test";
        await Assert.That(result).IsEqualTo(expected);
    }

    // EncodeToUtf8 — UTF-8 self-copy path

    [Test]
    public async Task EncodeToUtf8_FromUtf8_CopiesDirect()
    {
        // Arrange
        var span = new TextSpan("Test"u8, TextEncoding.Utf8);
        var destination = new byte[10];

        // Act
        var bytesWritten = span.EncodeToUtf8(destination);
        var result = System.Text.Encoding.UTF8.GetString(destination, 0, bytesWritten);

        // Assert
        const string expected = "Test";
        await Assert.That(result).IsEqualTo(expected);
    }

    // CompareTo with UTF-32 on both sides, same content

    [Test]
    public async Task CompareTo_Utf32BothSides_SameContent_ReturnsZero()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var span1 = new TextSpan(bytes, TextEncoding.Utf32);
        var span2 = new TextSpan(bytes, TextEncoding.Utf32);

        // Act — same encoding, same content
        var result = span1.CompareTo(span2);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // EncodeToUtf16 from UTF-32 with emoji — exercises surrogate pair output

    [Test]
    public async Task EncodeToUtf16_FromUtf32WithEmoji_WritesSurrogatePair()
    {
        // Arrange — U+1F389 is 🎉, takes 2 UTF-16 chars (surrogate pair)
        var utf32Bytes = TestHelpers.Encode("\U0001F389", TextEncoding.Utf32);
        var span = new TextSpan(utf32Bytes, TextEncoding.Utf32);
        var destination = new char[4];

        // Act
        var charsWritten = span.EncodeToUtf16(destination);
        var result = new string(destination, 0, charsWritten);

        // Assert
        await Assert.That(result).IsEqualTo("\U0001F389");
        await Assert.That(charsWritten).IsEqualTo(2);
    }

    // EncodeToUtf8 from UTF-32 with emoji — exercises 4-byte output

    [Test]
    public async Task EncodeToUtf8_FromUtf32WithEmoji_WritesFourBytes()
    {
        // Arrange
        var utf32Bytes = TestHelpers.Encode("\U0001F389", TextEncoding.Utf32);
        var span = new TextSpan(utf32Bytes, TextEncoding.Utf32);
        var destination = new byte[8];

        // Act
        var bytesWritten = span.EncodeToUtf8(destination);
        var result = System.Text.Encoding.UTF8.GetString(destination, 0, bytesWritten);

        // Assert
        await Assert.That(result).IsEqualTo("\U0001F389");
        await Assert.That(bytesWritten).IsEqualTo(4);
    }

    // GetHashCode with large input — exercises rented array path (>256 runes)

    [Test]
    public async Task GetHashCode_LargeInput_UsesRentedArray()
    {
        // Arrange — 300 ASCII chars, more than 256 runes
        var largeStr = new string('A', 300);
        var bytes = TestHelpers.Encode(largeStr, TextEncoding.Utf8);
        var hash = new TextSpan(bytes, TextEncoding.Utf8).GetHashCode();

        // Assert — just verify it produces a consistent non-zero hash
        await Assert.That(hash).IsNotEqualTo(0);
    }

    // Cross-encoding backward search — LastIndexOfCrossEncoding

    [Test]
    public async Task LastRuneIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_FindsLast()
    {
        // Arrange — "hello world hello" in UTF-8, searching for UTF-16 "hello"
        var haystack = new TextSpan("hello world hello"u8, TextEncoding.Utf8);
        var needleBytes = TestHelpers.Encode("hello", TextEncoding.Utf16);
        var needle = new TextSpan(needleBytes, TextEncoding.Utf16);

        // Act
        var result = haystack.LastRuneIndexOf(needle);

        // Assert — last "hello" starts at rune index 12
        await Assert.That(result).IsEqualTo(12);
    }

    [Test]
    public async Task LastByteIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_Miss()
    {
        // Arrange
        var haystack = new TextSpan("hello world"u8, TextEncoding.Utf8);
        var needleBytes = TestHelpers.Encode("xyz", TextEncoding.Utf16);
        var needle = new TextSpan(needleBytes, TextEncoding.Utf16);

        // Act
        var result = haystack.LastByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    // Cross-encoding Contains with UTF-32

    [Test]
    public async Task Contains_CrossEncoding_Utf8HaystackUtf32Needle_ReturnsTrue()
    {
        // Arrange — "cafe" in UTF-8, searching for UTF-32 "afe"
        var haystack = new TextSpan("cafe"u8, TextEncoding.Utf8);
        var needleBytes = TestHelpers.Encode("afe", TextEncoding.Utf32);
        var needle = new TextSpan(needleBytes, TextEncoding.Utf32);

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_CrossEncoding_Utf32HaystackUtf8Needle_ReturnsTrue()
    {
        // Arrange — "hello" in UTF-32, searching for UTF-8 "llo"
        var haystackBytes = TestHelpers.Encode("hello", TextEncoding.Utf32);
        var haystack = new TextSpan(haystackBytes, TextEncoding.Utf32);
        var needle = new TextSpan("llo"u8, TextEncoding.Utf8);

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_CrossEncoding_Utf32HaystackUtf8Needle_Miss()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("hello", TextEncoding.Utf32);
        var haystack = new TextSpan(haystackBytes, TextEncoding.Utf32);
        var needle = new TextSpan("xyz"u8, TextEncoding.Utf8);

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsFalse();
    }

    // TryFormat byte — UTF-32 text (IUtf8SpanFormattable)

    [Test]
    public async Task TryFormat_Byte_Utf32Span_TranscodesRuneByRune()
    {
        // Arrange
        var utf32Bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var span = new TextSpan(utf32Bytes, TextEncoding.Utf32);
        var dest = new byte[20];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
        await Assert.That(System.Text.Encoding.UTF8.GetString(dest, 0, written)).IsEqualTo("Hello");
    }

    [Test]
    public async Task TryFormat_Byte_Utf32Span_TooSmall_ReturnsFalse()
    {
        // Arrange — UTF-32 "Hello" needs 5 UTF-8 bytes, provide only 2
        var utf32Bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var span = new TextSpan(utf32Bytes, TextEncoding.Utf32);
        var dest = new byte[2];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsFalse();
        await Assert.That(written).IsEqualTo(0);
    }

    // TryFormat char — UTF-32 text (ISpanFormattable)

    [Test]
    public async Task TryFormat_Char_Utf32Span_TranscodesCorrectly()
    {
        // Arrange
        var utf32Bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var span = new TextSpan(utf32Bytes, TextEncoding.Utf32);
        var dest = new char[10];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(5);
        await Assert.That(new string(dest, 0, written)).IsEqualTo("Hello");
    }

    // TryFormat byte — UTF-32 with multi-byte rune (emoji)

    [Test]
    public async Task TryFormat_Byte_Utf32WithEmoji_TranscodesCorrectly()
    {
        // Arrange — U+1F389 is 4 bytes in UTF-8
        var utf32Bytes = TestHelpers.Encode("\U0001F389", TextEncoding.Utf32);
        var span = new TextSpan(utf32Bytes, TextEncoding.Utf32);
        var dest = new byte[8];

        // Act
        var success = span.TryFormat(dest, out var written, default, null);

        // Assert
        await Assert.That(success).IsTrue();
        await Assert.That(written).IsEqualTo(4);
    }

    // Cross-encoding LastByteIndexOf with UTF-32 haystack and UTF-16 needle

    [Test]
    public async Task LastByteIndexOf_CrossEncoding_Utf32HaystackUtf16Needle_FindsLast()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("abcabc", TextEncoding.Utf32);
        var haystack = new TextSpan(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("abc", TextEncoding.Utf16);
        var needle = new TextSpan(needleBytes, TextEncoding.Utf16);

        // Act
        var result = haystack.LastByteIndexOf(needle);

        // Assert — last "abc" starts at byte offset 12 (3 code points * 4 bytes each)
        await Assert.That(result).IsEqualTo(12);
    }

    // Cross-encoding IndexOf with non-ASCII needle to exercise transcode pattern path

    [Test]
    public async Task ByteIndexOf_CrossEncoding_NonAsciiNeedle_FindsMatch()
    {
        // Arrange — "hello cafe world" in UTF-8, needle "cafe" in UTF-16
        var haystack = new TextSpan("hello cafe world"u8, TextEncoding.Utf8);
        var needleBytes = TestHelpers.Encode("cafe", TextEncoding.Utf16);
        var needle = new TextSpan(needleBytes, TextEncoding.Utf16);

        // Act
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    // Cross-encoding LastRuneIndexOf with empty needle — returns RuneLength

    [Test]
    public async Task LastRuneIndexOf_CrossEncoding_EmptyNeedle_ReturnsRuneLength()
    {
        // Arrange
        var haystack = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var needle = new TextSpan([], TextEncoding.Utf16);

        // Act
        var result = haystack.LastRuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    // Cross-encoding IndexOf with empty needle — returns 0

    [Test]
    public async Task ByteIndexOf_CrossEncoding_EmptyNeedle_ReturnsZero()
    {
        // Arrange
        var haystack = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var needle = new TextSpan([], TextEncoding.Utf16);

        // Act
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // Cross-encoding backward search with non-ASCII to exercise LastIndexOfCrossEncoding transcode path

    [Test]
    public async Task LastRuneIndexOf_CrossEncoding_NonAsciiUtf16Needle_FindsMatch()
    {
        // Arrange — multibyte needle forces Transcode path (not ASCII narrow)
        var haystack = new TextSpan("one \u00e9 two \u00e9 three"u8, TextEncoding.Utf8);
        var needleBytes = TestHelpers.Encode("\u00e9", TextEncoding.Utf16);
        var needle = new TextSpan(needleBytes, TextEncoding.Utf16);

        // Act
        var result = haystack.LastRuneIndexOf(needle);

        // Assert — last '\u00e9' is at rune index 10
        await Assert.That(result).IsEqualTo(10);
    }

    // Contains overloads for specific encoding slices

    [Test]
    public async Task Contains_Utf32IntSlice_ReturnsTrue()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var haystack = new TextSpan(haystackBytes, TextEncoding.Utf32);
        ReadOnlySpan<int> needle = ['H', 'e'];

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }
}
