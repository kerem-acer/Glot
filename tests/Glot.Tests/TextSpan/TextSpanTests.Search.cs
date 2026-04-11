namespace Glot.Tests;

public partial class TextSpanTests
{
    [Test]
    public async Task Contains_Found_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan("World"u8, TextEncoding.Utf8);

        // Act
        var result = span.Contains(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_NotFound_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan("xyz"u8, TextEncoding.Utf8);

        // Act
        var result = span.Contains(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Contains_EmptyValue_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var value = new TextSpan([], TextEncoding.Utf8);

        // Act
        var result = span.Contains(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public Task Contains_ReadOnlySpanByte_MatchesCorrectly()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var found = span.Contains("World"u8);
        var notFound = span.Contains("xyz"u8);

        // Assert
        return Verify(new { found, notFound });
    }

    [Test]
    public Task Contains_ReadOnlySpanChar_MatchesCorrectly()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);

        // Act
        var found = span.Contains("World".AsSpan());
        var notFound = span.Contains("xyz".AsSpan());

        // Assert
        return Verify(new { found, notFound });
    }

    [Test]
    public Task Contains_MultiByteUtf8_DistinguishesAccents()
    {
        // Arrange
        var span = new TextSpan("I love café"u8, TextEncoding.Utf8);

        // Act
        var found = span.Contains("café"u8);
        var notFound = span.Contains("cafe"u8);

        // Assert
        return Verify(new { found, notFound });
    }

    [Test]
    public async Task Contains_Emoji_ReturnsTrue()
    {
        // Arrange & Act
        var span = new TextSpan("Hello 🎉 World"u8, TextEncoding.Utf8);
        var result = span.Contains("🎉"u8);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("World", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = span.Contains(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_CrossEncodingMultiByte_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("café latte"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("café", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = span.Contains(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task IndexOf_AtBeginning_ReturnsZero()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(0);
    }

    [Test]
    public async Task IndexOf_AtMiddle_ReturnsRuneIndex()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan("World"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task IndexOf_AtEnd_ReturnsRuneIndex()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var index = span.RuneIndexOf("lo"u8);

        // Assert
        await Assert.That(index).IsEqualTo(3);
    }

    [Test]
    public async Task IndexOf_NotFound_ReturnsNegativeOne()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan("xyz"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_EmptyValue_ReturnsZero()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var value = new TextSpan([], TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(0);
    }

    [Test]
    public async Task IndexOf_ReadOnlySpanByte_ReturnsRuneIndex()
    {
        // Arrange & Act
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var index = span.RuneIndexOf("World"u8);

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task IndexOf_ReadOnlySpanChar_ReturnsRuneIndex()
    {
        // Arrange & Act
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var index = span.RuneIndexOf("World".AsSpan());

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task IndexOf_MultiByteContent_ReturnsRuneIndex()
    {
        // Arrange — "café World" = c(0) a(1) f(2) é(3) (4) W(5) o(6) r(7) l(8) d(9)
        var span = new TextSpan("café World"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf("World"u8);

        // Assert
        await Assert.That(index).IsEqualTo(5);
    }

    [Test]
    public async Task IndexOf_EmojiContent_ReturnsRuneIndex()
    {
        // Arrange — "Hi🎉!" = H(0) i(1) 🎉(2) !(3)
        var span = new TextSpan("Hi🎉!"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf("!"u8);

        // Assert
        await Assert.That(index).IsEqualTo(3);
    }

    [Test]
    public async Task IndexOf_MultipleOccurrences_ReturnsFirst()
    {
        // Arrange & Act
        var span = new TextSpan("abcabc"u8, TextEncoding.Utf8);
        var index = span.RuneIndexOf("bc"u8);

        // Assert
        await Assert.That(index).IsEqualTo(1);
    }

    [Test]
    public async Task IndexOf_CrossEncodingUtf8SearchUtf16_ReturnsRuneIndex()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("World", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task IndexOf_CrossEncodingUtf16SearchUtf8_ReturnsRuneIndex()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        var value = new TextSpan("World"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task IndexOf_CrossEncodingEmoji_ReturnsRuneIndex()
    {
        // Arrange
        var span = new TextSpan("A🎉B"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("🎉", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(1);
    }

    [Test]
    public async Task IndexOf_Utf16SameEncoding_ReturnsRuneIndex()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        var value = new TextSpan(TestHelpers.Encode("World", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task IndexOf_Utf32SameEncoding_ReturnsRuneIndex()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var span = new TextSpan(bytes, TextEncoding.Utf32);
        var value = new TextSpan(TestHelpers.Encode("World", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task LastIndexOf_MultipleOccurrences_ReturnsLast()
    {
        // Arrange & Act
        var span = new TextSpan("abcabc"u8, TextEncoding.Utf8);
        var index = span.LastRuneIndexOf("bc"u8);

        // Assert
        await Assert.That(index).IsEqualTo(4);
    }

    [Test]
    public async Task LastIndexOf_SingleOccurrence_ReturnsIndex()
    {
        // Arrange & Act
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var index = span.LastRuneIndexOf("World"u8);

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task LastIndexOf_NotFound_ReturnsNegativeOne()
    {
        // Arrange & Act
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var index = span.LastRuneIndexOf("xyz"u8);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task LastIndexOf_EmptyValue_ReturnsLength()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var value = new TextSpan([], TextEncoding.Utf8);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(5);
    }

    [Test]
    public async Task LastIndexOf_ReadOnlySpanByte_ReturnsLastIndex()
    {
        // Arrange & Act
        var span = new TextSpan("abab"u8, TextEncoding.Utf8);
        var index = span.LastRuneIndexOf("ab"u8);

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task LastIndexOf_ReadOnlySpanChar_ReturnsLastIndex()
    {
        // Arrange & Act
        var span = new TextSpan("abab"u8, TextEncoding.Utf8);
        var index = span.LastRuneIndexOf("ab".AsSpan());

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task LastIndexOf_MultiByteContent_ReturnsRuneIndex()
    {
        // Arrange — "éaéb" = é(0) a(1) é(2) b(3)
        var span = new TextSpan("éaéb"u8, TextEncoding.Utf8);

        // Act
        var index = span.LastRuneIndexOf("é"u8);

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task LastIndexOf_CrossEncoding_ReturnsLastIndex()
    {
        // Arrange
        var span = new TextSpan("abab"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("ab", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task LastIndexOf_Utf16SameEncoding_ReturnsLastIndex()
    {
        // Arrange
        var bytes = TestHelpers.Encode("abab", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        var value = new TextSpan(TestHelpers.Encode("ab", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task StartsWith_MatchingPrefix_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_NonMatchingPrefix_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan("World"u8, TextEncoding.Utf8);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task StartsWith_EmptyValue_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var value = new TextSpan([], TextEncoding.Utf8);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public Task StartsWith_ReadOnlySpanByte_MatchesCorrectly()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var found = span.StartsWith("He"u8);
        var notFound = span.StartsWith("lo"u8);

        // Assert
        return Verify(new { found, notFound });
    }

    [Test]
    public Task StartsWith_ReadOnlySpanChar_MatchesCorrectly()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var found = span.StartsWith("He".AsSpan());
        var notFound = span.StartsWith("lo".AsSpan());

        // Assert
        return Verify(new { found, notFound });
    }

    [Test]
    public Task StartsWith_MultiBytePrefix_DistinguishesAccents()
    {
        // Arrange
        var span = new TextSpan("café latte"u8, TextEncoding.Utf8);

        // Act
        var found = span.StartsWith("café"u8);
        var notFound = span.StartsWith("cafe"u8);

        // Assert
        return Verify(new { found, notFound });
    }

    [Test]
    public async Task StartsWith_Emoji_ReturnsTrue()
    {
        // Arrange & Act
        var span = new TextSpan("🎉Hello"u8, TextEncoding.Utf8);
        var result = span.StartsWith("🎉"u8);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_CrossEncodingEmoji_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("🎉Hello"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("🎉", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_Utf16SameEncoding_ReturnsTrue()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        var value = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_LongerValue_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hi"u8, TextEncoding.Utf8);
        var value = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EndsWith_MatchingSuffix_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan("World"u8, TextEncoding.Utf8);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_NonMatchingSuffix_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EndsWith_EmptyValue_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var value = new TextSpan([], TextEncoding.Utf8);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public Task EndsWith_ReadOnlySpanByte_MatchesCorrectly()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var found = span.EndsWith("lo"u8);
        var notFound = span.EndsWith("He"u8);

        // Assert
        return Verify(new { found, notFound });
    }

    [Test]
    public Task EndsWith_ReadOnlySpanChar_MatchesCorrectly()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var found = span.EndsWith("lo".AsSpan());
        var notFound = span.EndsWith("He".AsSpan());

        // Assert
        return Verify(new { found, notFound });
    }

    [Test]
    public Task EndsWith_MultiByteSuffix_DistinguishesAccents()
    {
        // Arrange
        var span = new TextSpan("hello café"u8, TextEncoding.Utf8);

        // Act
        var found = span.EndsWith("café"u8);
        var notFound = span.EndsWith("cafe"u8);

        // Assert
        return Verify(new { found, notFound });
    }

    [Test]
    public async Task EndsWith_Emoji_ReturnsTrue()
    {
        // Arrange & Act
        var span = new TextSpan("Hello🎉"u8, TextEncoding.Utf8);
        var result = span.EndsWith("🎉"u8);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello World"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("World", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_CrossEncodingEmoji_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello🎉"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("🎉", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_Utf16SameEncoding_ReturnsTrue()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello World", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        var value = new TextSpan(TestHelpers.Encode("World", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_LongerValue_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hi"u8, TextEncoding.Utf8);
        var value = new TextSpan("Hello"u8, TextEncoding.Utf8);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EndsWith_CrossEncodingLongerValue_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hi"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IndexOf_LargeText_ExercisesSimd()
    {
        // Arrange
        const int padding = 1000;
        var large = new string('x', padding) + "NEEDLE" + new string('y', padding);
        var bytes = TestHelpers.Encode(large, TextEncoding.Utf8);
        var span = new TextSpan(bytes, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf("NEEDLE"u8);

        // Assert
        await Assert.That(index).IsEqualTo(padding);
    }

    [Test]
    public async Task LastIndexOf_LargeText_ReturnsLastOccurrence()
    {
        // Arrange
        const int padding = 500;
        var large = new string('x', padding) + "NEEDLE" + new string('y', padding) + "NEEDLE" + new string('z', padding);
        var bytes = TestHelpers.Encode(large, TextEncoding.Utf8);
        var span = new TextSpan(bytes, TextEncoding.Utf8);
        const int expectedIndex = padding + 6 + padding;

        // Act
        var index = span.LastRuneIndexOf("NEEDLE"u8);

        // Assert
        await Assert.That(index).IsEqualTo(expectedIndex);
    }

    [Test]
    public Task Contains_LargeMultiByte_ExercisesSimd()
    {
        // Arrange
        const int padding = 500;
        var large = new string('日', padding) + "NEEDLE" + new string('本', padding);
        var bytes = TestHelpers.Encode(large, TextEncoding.Utf8);
        var span = new TextSpan(bytes, TextEncoding.Utf8);
        const int expectedLength = padding + 6 + padding;

        // Act
        var contains = span.Contains("NEEDLE"u8);
        var length = span.RuneLength;

        // Assert
        return Verify(new { contains, lengthMatches = length == expectedLength });
    }

    [Test]
    public async Task IndexOf_LargeCrossEncoding_ExercisesSimd()
    {
        // Arrange
        const int padding = 1000;
        var large = new string('A', padding) + "FIND" + new string('B', padding);
        var span = new TextSpan(TestHelpers.Encode(large, TextEncoding.Utf8), TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("FIND", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(padding);
    }

    [Test]
    public async Task IndexOf_Utf16SourceUtf8Value_CrossEncoding_ReturnsRuneIndex()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("Hello 🎉 World", TextEncoding.Utf16);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf16);
        var value = new TextSpan("🎉"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task IndexOf_Utf32SourceUtf8Value_CrossEncoding_ReturnsRuneIndex()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("Hello 🎉 World", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("🎉"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(6);
    }

    [Test]
    public async Task Contains_Utf16SourceUtf32Value_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("café latte", TextEncoding.Utf16);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf16);
        var value = new TextSpan(TestHelpers.Encode("café", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = span.Contains(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_Utf32SourceUtf8Value_NotFound_ReturnsFalse()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("xyz"u8, TextEncoding.Utf8);

        // Act
        var result = span.Contains(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task LastIndexOf_Utf16SourceUtf8Value_CrossEncoding_ReturnsLastIndex()
    {
        // Arrange — "ab🎉ab🎉" = a(0) b(1) 🎉(2) a(3) b(4) 🎉(5)
        var sourceBytes = TestHelpers.Encode("ab🎉ab🎉", TextEncoding.Utf16);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf16);
        var value = new TextSpan("🎉"u8, TextEncoding.Utf8);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(5);
    }

    [Test]
    public async Task LastIndexOf_Utf32SourceUtf8Value_CrossEncoding_ReturnsLastIndex()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("xNEEDLEyNEEDLE", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("NEEDLE"u8, TextEncoding.Utf8);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(8);
    }

    [Test]
    public async Task LastIndexOf_CrossEncodingNotFound_ReturnsNegativeOne()
    {
        // Arrange
        var span = new TextSpan(TestHelpers.Encode("Hello World", TextEncoding.Utf16), TextEncoding.Utf16);
        var value = new TextSpan("xyz"u8, TextEncoding.Utf8);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_CrossEncodingNotFound_ReturnsNegativeOne()
    {
        // Arrange
        var span = new TextSpan(TestHelpers.Encode("Hello World", TextEncoding.Utf16), TextEncoding.Utf16);
        var value = new TextSpan("xyz"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_Utf32CrossEncodingNotFound_ReturnsNegativeOne()
    {
        // Arrange
        var span = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf32), TextEncoding.Utf32);
        var value = new TextSpan("xyz"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task LastIndexOf_Utf32CrossEncodingNotFound_ReturnsNegativeOne()
    {
        // Arrange
        var span = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf32), TextEncoding.Utf32);
        var value = new TextSpan("xyz"u8, TextEncoding.Utf8);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task LastIndexOf_Utf16SourceUtf8Value_NotFound_ReturnsNegativeOne()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("abcdef", TextEncoding.Utf16);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf16);
        var value = new TextSpan("zzz"u8, TextEncoding.Utf8);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task LastIndexOf_EmptyValueSameEncoding_ReturnsLength()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);
        var span = new TextSpan(bytes, TextEncoding.Utf16);
        var value = new TextSpan([], TextEncoding.Utf16);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(5);
    }

    [Test]
    public async Task StartsWith_Utf16SourceUtf8Value_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("🎉Hello", TextEncoding.Utf16);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf16);
        var value = new TextSpan("🎉"u8, TextEncoding.Utf8);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_Utf32SourceUtf8Value_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("café", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("caf"u8, TextEncoding.Utf8);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_CrossEncodingEmptyValue_ReturnsTrue()
    {
        // Arrange
        var span = new TextSpan("Hello"u8, TextEncoding.Utf8);
        var value = new TextSpan([], TextEncoding.Utf16);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_CrossEncodingSourceShorterThanValue_ReturnsFalse()
    {
        // Arrange — source is a prefix of value, all runes match until source exhausted
        var span = new TextSpan("He"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = span.StartsWith(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EndsWith_Utf16SourceUtf8Value_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("Hello🎉", TextEncoding.Utf16);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf16);
        var value = new TextSpan("🎉"u8, TextEncoding.Utf8);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_Utf32SourceUtf8Value_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var sourceBytes = TestHelpers.Encode("Hello café", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_CrossEncodingValueLongerThanSource_ReturnsFalse()
    {
        // Arrange
        var span = new TextSpan("Hi"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("Hello World", TextEncoding.Utf32), TextEncoding.Utf32);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EndsWith_CrossEncodingSourceIsSuffixOfValue_ReturnsFalse()
    {
        // Arrange — source "lo" is suffix of value "Hello", all runes match from end until source exhausted
        var span = new TextSpan("lo"u8, TextEncoding.Utf8);
        var value = new TextSpan(TestHelpers.Encode("Hello", TextEncoding.Utf16), TextEncoding.Utf16);

        // Act
        var result = span.EndsWith(value);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task IndexOf_CrossEncodingFalsePrefix_ReturnsCorrectIndex()
    {
        // Arrange — transcoded prefix "c" matches at position 2, full verify confirms "café"
        var sourceBytes = TestHelpers.Encode("xxcafé", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(2);
    }

    [Test]
    public async Task IndexOf_CrossEncodingPrefixMatchButFullFails_SkipsToNext()
    {
        // Arrange — "cab" shares "c" prefix with "café", search must skip to find actual match
        var sourceBytes = TestHelpers.Encode("cab café", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(4);
    }

    [Test]
    public async Task IndexOf_CrossEncodingPrefixMatchNowhere_ReturnsNegativeOne()
    {
        // Arrange — "cab cab" has "c" prefix matches but never full "café"
        var sourceBytes = TestHelpers.Encode("cab cab", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_CrossEncodingPrefixAtEnd_ReturnsNegativeOne()
    {
        // Arrange — "xc" has prefix match at end but source too short for full "café"
        var sourceBytes = TestHelpers.Encode("xc", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var index = span.RuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }

    [Test]
    public async Task LastIndexOf_CrossEncodingPrefixMatchButFullFails_SkipsToPrevious()
    {
        // Arrange — "café cab" has "c" prefix at positions 0 and 5, only position 0 is full match
        var sourceBytes = TestHelpers.Encode("café cab", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(0);
    }

    [Test]
    public async Task LastIndexOf_CrossEncodingPrefixMatchNowhere_ReturnsNegativeOne()
    {
        // Arrange — "cab cab" has "c" prefix but never full "café"
        var sourceBytes = TestHelpers.Encode("cab cab", TextEncoding.Utf32);
        var span = new TextSpan(sourceBytes, TextEncoding.Utf32);
        var value = new TextSpan("café"u8, TextEncoding.Utf8);

        // Act
        var index = span.LastRuneIndexOf(value);

        // Assert
        await Assert.That(index).IsEqualTo(-1);
    }
}
