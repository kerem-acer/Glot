using System.Runtime.InteropServices;

namespace Glot.Tests;

public partial class TextTests
{
    // ──────────────────────────────────────────────
    //  Contains — same-encoding
    // ──────────────────────────────────────────────

    [Test]
    public async Task Contains_Utf8SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.FromUtf8("World"u8);

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_Utf8SameEncoding_Miss_ReturnsFalse()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.FromUtf8("xyz"u8);

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Contains_CrossEncoding_Utf8HaystackUtf16Needle_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.From("World");

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_CrossEncoding_Utf8HaystackUtf16Needle_Miss_ReturnsFalse()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.From("xyz");

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Contains_Utf16SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.From("Hello World");
        var needle = Text.From("World");

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_Utf32SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var needle = Text.FromBytes(needleBytes, TextEncoding.Utf32);

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_EmptyNeedle_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);
        var needle = Text.Empty;

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // Contains(string) paths

    [Test]
    public async Task Contains_String_Utf16Haystack_Match_ReturnsTrue()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.Contains("World");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_String_Utf8Haystack_Miss_ReturnsFalse()
    {
        // Arrange — forces cross-encoding via AsSpan
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.Contains("xyz");

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Contains_String_NullOrEmpty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(haystack.Contains("")).IsTrue();
    }

    // Contains(ReadOnlySpan<byte>) paths

    [Test]
    public async Task Contains_SpanByte_SameEncodingUtf8_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.Contains("World"u8);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_SpanByte_CrossEncoding_Utf8HaystackUtf16Bytes_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act
        var result = haystack.Contains(needleBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_SpanByte_Empty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.Contains(ReadOnlySpan<byte>.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // Contains(ReadOnlySpan<char>) paths

    [Test]
    public async Task Contains_SpanChar_Utf16Haystack_Match_ReturnsTrue()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.Contains("World".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_SpanChar_Utf8Haystack_Match_ReturnsTrue()
    {
        // Arrange — exercises cross-encoding path
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.Contains("World".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_SpanChar_Empty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.Contains(ReadOnlySpan<char>.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // Contains(ReadOnlySpan<int>) paths

    [Test]
    public async Task Contains_SpanInt_Utf32Haystack_Match_ReturnsTrue()
    {
        // Arrange — exercises Encoding == Utf32 fast path
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.Contains(needleInts);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_SpanInt_Utf8Haystack_Match_ReturnsTrue()
    {
        // Arrange — exercises cross-encoding path
        var haystack = Text.FromUtf8("Hello World"u8);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.Contains(needleInts);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_SpanInt_Empty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.Contains(ReadOnlySpan<int>.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // ──────────────────────────────────────────────
    //  StartsWith — same-encoding, cross-encoding, miss, empty
    // ──────────────────────────────────────────────

    [Test]
    public async Task StartsWith_Utf8SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var prefix = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.StartsWith(prefix);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_Utf8SameEncoding_Miss_ReturnsFalse()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var prefix = Text.FromUtf8("xyz"u8);

        // Act
        var result = haystack.StartsWith(prefix);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task StartsWith_CrossEncoding_Utf8HaystackUtf16Needle_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var prefix = Text.From("Hello");

        // Act
        var result = haystack.StartsWith(prefix);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_CrossEncoding_Utf8HaystackUtf16Needle_Miss_ReturnsFalse()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var prefix = Text.From("xyz");

        // Act
        var result = haystack.StartsWith(prefix);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task StartsWith_Utf16SameEncoding_Match_ReturnsTrue()
    {
        // Arrange — exercises Encoding == Utf16 fast path for string
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.StartsWith("Hello");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_Utf32SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var prefixBytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var prefix = Text.FromBytes(prefixBytes, TextEncoding.Utf32);

        // Act
        var result = haystack.StartsWith(prefix);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_EmptyNeedle_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.StartsWith(Text.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // StartsWith(string) paths

    [Test]
    public async Task StartsWith_String_Utf16Haystack_Match_ReturnsTrue()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.StartsWith("Hello");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_String_Utf8Haystack_Match_ReturnsTrue()
    {
        // Arrange — exercises cross-encoding path
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.StartsWith("Hello");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_String_NullOrEmpty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(haystack.StartsWith("")).IsTrue();
    }

    // StartsWith(ReadOnlySpan<byte>) paths

    [Test]
    public async Task StartsWith_SpanByte_SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.StartsWith("Hello"u8);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_SpanByte_CrossEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var prefixBytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);

        // Act
        var result = haystack.StartsWith(prefixBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_SpanByte_Empty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.StartsWith(ReadOnlySpan<byte>.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // StartsWith(ReadOnlySpan<char>) paths

    [Test]
    public async Task StartsWith_SpanChar_Utf16Haystack_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.StartsWith("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_SpanChar_Utf8Haystack_Match_ReturnsTrue()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.StartsWith("Hello".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_SpanChar_Empty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.From("Hello");

        // Act
        var result = haystack.StartsWith(ReadOnlySpan<char>.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // StartsWith(ReadOnlySpan<int>) paths

    [Test]
    public async Task StartsWith_SpanInt_Utf32Haystack_Match_ReturnsTrue()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var prefixBytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var prefixInts = MemoryMarshal.Cast<byte, int>(prefixBytes);

        // Act
        var result = haystack.StartsWith(prefixInts);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_SpanInt_Utf8Haystack_Match_ReturnsTrue()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("Hello World"u8);
        var prefixBytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);
        var prefixInts = MemoryMarshal.Cast<byte, int>(prefixBytes);

        // Act
        var result = haystack.StartsWith(prefixInts);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_SpanInt_Empty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.StartsWith(ReadOnlySpan<int>.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // ──────────────────────────────────────────────
    //  EndsWith — same-encoding, cross-encoding, miss, empty
    // ──────────────────────────────────────────────

    [Test]
    public async Task EndsWith_Utf8SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var suffix = Text.FromUtf8("World"u8);

        // Act
        var result = haystack.EndsWith(suffix);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_Utf8SameEncoding_Miss_ReturnsFalse()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var suffix = Text.FromUtf8("xyz"u8);

        // Act
        var result = haystack.EndsWith(suffix);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EndsWith_CrossEncoding_Utf8HaystackUtf16Needle_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var suffix = Text.From("World");

        // Act
        var result = haystack.EndsWith(suffix);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_CrossEncoding_Utf8HaystackUtf16Needle_Miss_ReturnsFalse()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var suffix = Text.From("xyz");

        // Act
        var result = haystack.EndsWith(suffix);

        // Assert
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EndsWith_Utf16SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.EndsWith("World");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_Utf32SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var suffixBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var suffix = Text.FromBytes(suffixBytes, TextEncoding.Utf32);

        // Act
        var result = haystack.EndsWith(suffix);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_EmptyNeedle_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.EndsWith(Text.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // EndsWith(string) paths

    [Test]
    public async Task EndsWith_String_Utf16Haystack_Match_ReturnsTrue()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.EndsWith("World");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_String_Utf8Haystack_Match_ReturnsTrue()
    {
        // Arrange — exercises cross-encoding path
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.EndsWith("World");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_String_NullOrEmpty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(haystack.EndsWith("")).IsTrue();
    }

    // EndsWith(ReadOnlySpan<byte>) paths

    [Test]
    public async Task EndsWith_SpanByte_SameEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.EndsWith("World"u8);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_SpanByte_CrossEncoding_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var suffixBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act
        var result = haystack.EndsWith(suffixBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_SpanByte_Empty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.EndsWith(ReadOnlySpan<byte>.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // EndsWith(ReadOnlySpan<char>) paths

    [Test]
    public async Task EndsWith_SpanChar_Utf16Haystack_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.EndsWith("World".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_SpanChar_Utf8Haystack_Match_ReturnsTrue()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.EndsWith("World".AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_SpanChar_Empty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.From("Hello");

        // Act
        var result = haystack.EndsWith(ReadOnlySpan<char>.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // EndsWith(ReadOnlySpan<int>) paths

    [Test]
    public async Task EndsWith_SpanInt_Utf32Haystack_Match_ReturnsTrue()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var suffixBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var suffixInts = MemoryMarshal.Cast<byte, int>(suffixBytes);

        // Act
        var result = haystack.EndsWith(suffixInts);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_SpanInt_Utf8Haystack_Match_ReturnsTrue()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("Hello World"u8);
        var suffixBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var suffixInts = MemoryMarshal.Cast<byte, int>(suffixBytes);

        // Act
        var result = haystack.EndsWith(suffixInts);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_SpanInt_Empty_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.EndsWith(ReadOnlySpan<int>.Empty);

        // Assert
        await Assert.That(result).IsTrue();
    }

    // ──────────────────────────────────────────────
    //  RuneIndexOf — same-encoding, cross-encoding, miss, empty
    // ──────────────────────────────────────────────

    [Test]
    public async Task RuneIndexOf_Utf8SameEncoding_Match_ReturnsPosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.FromUtf8("World"u8);

        // Act
        var result = haystack.RuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_Utf8SameEncoding_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.FromUtf8("xyz"u8);

        // Act
        var result = haystack.RuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task RuneIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_Match_ReturnsPosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.From("World");

        // Act
        var result = haystack.RuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.From("xyz");

        // Act
        var result = haystack.RuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task RuneIndexOf_Utf16SameEncoding_Match_ReturnsPosition()
    {
        // Arrange — exercises Encoding == Utf16 fast path for string overload
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.RuneIndexOf("World");

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_Utf32SameEncoding_Match_ReturnsPosition()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.RuneIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_EmptyNeedle_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);
        var needle = Text.Empty;

        // Act
        var result = haystack.RuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    [Test]
    public async Task RuneIndexOf_CrossEncoding_EmptyNeedle_ReturnsZero()
    {
        // Arrange — empty needle on cross-encoding path (different encoding but empty)
        var haystack = Text.FromUtf8("Hello"u8);
        var needle = Text.From("");

        // Act
        var result = haystack.RuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // RuneIndexOf(string) paths

    [Test]
    public async Task RuneIndexOf_String_Utf8Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange — forces cross-encoding path
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.RuneIndexOf("xyz");

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task RuneIndexOf_String_Utf16Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange — exercises Encoding == Utf16 fast path miss
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.RuneIndexOf("xyz");

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task RuneIndexOf_String_NullOrEmpty_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(haystack.RuneIndexOf("")).IsEqualTo(0);
    }

    // RuneIndexOf(ReadOnlySpan<byte>) paths

    [Test]
    public async Task RuneIndexOf_SpanByte_SameEncoding_Match_ReturnsPosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.RuneIndexOf("World"u8);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_SpanByte_SameEncoding_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.RuneIndexOf("xyz"u8);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task RuneIndexOf_SpanByte_CrossEncoding_Match_ReturnsPosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act
        var result = haystack.RuneIndexOf(needleBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_SpanByte_Empty_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.RuneIndexOf(ReadOnlySpan<byte>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // RuneIndexOf(ReadOnlySpan<char>) paths

    [Test]
    public async Task RuneIndexOf_SpanChar_Utf16Haystack_Match_ReturnsPosition()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.RuneIndexOf("World".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_SpanChar_Utf16Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.From("Hello World");

        // Act
        var result = haystack.RuneIndexOf("xyz".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task RuneIndexOf_SpanChar_Utf8Haystack_Match_ReturnsPosition()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.RuneIndexOf("World".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_SpanChar_Empty_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.RuneIndexOf(ReadOnlySpan<char>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // RuneIndexOf(ReadOnlySpan<int>) paths

    [Test]
    public async Task RuneIndexOf_SpanInt_Utf32Haystack_Match_ReturnsPosition()
    {
        // Arrange — exercises Encoding == Utf32 fast path
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.RuneIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_SpanInt_Utf32Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("xyz", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.RuneIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task RuneIndexOf_SpanInt_Utf8Haystack_Match_ReturnsPosition()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("Hello World"u8);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.RuneIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_SpanInt_Empty_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.RuneIndexOf(ReadOnlySpan<int>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // ──────────────────────────────────────────────
    //  LastRuneIndexOf — same-encoding, cross-encoding, miss, empty
    // ──────────────────────────────────────────────

    [Test]
    public async Task LastRuneIndexOf_Utf8SameEncoding_Match_ReturnsLastPosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needle = Text.FromUtf8("abc"u8);

        // Act
        var result = haystack.LastRuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_Utf8SameEncoding_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needle = Text.FromUtf8("xyz"u8);

        // Act
        var result = haystack.LastRuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastRuneIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_Match_ReturnsLastPosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needle = Text.From("abc");

        // Act
        var result = haystack.LastRuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needle = Text.From("xyz");

        // Act
        var result = haystack.LastRuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastRuneIndexOf_Utf16SameEncoding_Match_ReturnsLastPosition()
    {
        // Arrange
        var haystack = Text.From("abcabc");

        // Act
        var result = haystack.LastRuneIndexOf("abc");

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_Utf32SameEncoding_Match_ReturnsLastPosition()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("abcabc", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("abc", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.LastRuneIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_EmptyNeedle_ReturnsRuneLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);
        var needle = Text.Empty;

        // Act
        var result = haystack.LastRuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    [Test]
    public async Task LastRuneIndexOf_CrossEncoding_EmptyNeedle_ReturnsRuneLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);
        var needle = Text.From("");

        // Act
        var result = haystack.LastRuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    // LastRuneIndexOf(string) paths

    [Test]
    public async Task LastRuneIndexOf_String_Utf8Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange — forces cross-encoding path
        var haystack = Text.FromUtf8("abcabc"u8);

        // Act
        var result = haystack.LastRuneIndexOf("xyz");

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastRuneIndexOf_String_Utf16Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange — exercises Encoding == Utf16 fast path miss
        var haystack = Text.From("abcabc");

        // Act
        var result = haystack.LastRuneIndexOf("xyz");

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastRuneIndexOf_String_NullOrEmpty_ReturnsRuneLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(haystack.LastRuneIndexOf("")).IsEqualTo(5);
    }

    // LastRuneIndexOf(ReadOnlySpan<byte>) paths

    [Test]
    public async Task LastRuneIndexOf_SpanByte_SameEncoding_Match_ReturnsLastPosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);

        // Act
        var result = haystack.LastRuneIndexOf("abc"u8);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_SpanByte_SameEncoding_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);

        // Act
        var result = haystack.LastRuneIndexOf("xyz"u8);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastRuneIndexOf_SpanByte_CrossEncoding_Match_ReturnsLastPosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needleBytes = TestHelpers.Encode("abc", TextEncoding.Utf16);

        // Act
        var result = haystack.LastRuneIndexOf(needleBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_SpanByte_Empty_ReturnsRuneLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.LastRuneIndexOf(ReadOnlySpan<byte>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    // LastRuneIndexOf(ReadOnlySpan<char>) paths

    [Test]
    public async Task LastRuneIndexOf_SpanChar_Utf16Haystack_Match_ReturnsLastPosition()
    {
        // Arrange
        var haystack = Text.From("abcabc");

        // Act
        var result = haystack.LastRuneIndexOf("abc".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_SpanChar_Utf16Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.From("abcabc");

        // Act
        var result = haystack.LastRuneIndexOf("xyz".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastRuneIndexOf_SpanChar_Utf8Haystack_Match_ReturnsLastPosition()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("abcabc"u8);

        // Act
        var result = haystack.LastRuneIndexOf("abc".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_SpanChar_Empty_ReturnsRuneLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.LastRuneIndexOf(ReadOnlySpan<char>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    // LastRuneIndexOf(ReadOnlySpan<int>) paths

    [Test]
    public async Task LastRuneIndexOf_SpanInt_Utf32Haystack_Match_ReturnsLastPosition()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("abcabc", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("abc", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.LastRuneIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_SpanInt_Utf32Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("abcabc", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("xyz", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.LastRuneIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastRuneIndexOf_SpanInt_Utf8Haystack_Match_ReturnsLastPosition()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("abcabc"u8);
        var needleBytes = TestHelpers.Encode("abc", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.LastRuneIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_SpanInt_Empty_ReturnsRuneLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.LastRuneIndexOf(ReadOnlySpan<int>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    // ──────────────────────────────────────────────
    //  ByteIndexOf — same-encoding, cross-encoding, miss, empty
    // ──────────────────────────────────────────────

    [Test]
    public async Task ByteIndexOf_Utf8SameEncoding_Match_ReturnsBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.FromUtf8("World"u8);

        // Act
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_Utf8SameEncoding_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.FromUtf8("xyz"u8);

        // Act
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ByteIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_Match_ReturnsBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.From("World");

        // Act
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needle = Text.From("xyz");

        // Act
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ByteIndexOf_Utf16SameEncoding_Match_ReturnsBytePosition()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("Hello World");

        // Act — "World" starts at char index 6, byte index 12
        var result = haystack.ByteIndexOf("World");

        // Assert
        await Assert.That(result).IsEqualTo(12);
    }

    [Test]
    public async Task ByteIndexOf_Utf32SameEncoding_Match_ReturnsBytePosition()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var needle = Text.FromBytes(needleBytes, TextEncoding.Utf32);

        // Act — "World" starts at rune index 6, byte index 24 (6 * 4)
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(24);
    }

    [Test]
    public async Task ByteIndexOf_EmptyNeedle_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);
        var needle = Text.Empty;

        // Act
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // ByteIndexOf(string) paths

    [Test]
    public async Task ByteIndexOf_String_Utf16Haystack_Match_ReturnsBytePosition()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("Hello World");

        // Act — "World" at char 6 = byte 12
        var result = haystack.ByteIndexOf("World");

        // Assert
        await Assert.That(result).IsEqualTo(12);
    }

    [Test]
    public async Task ByteIndexOf_String_Utf8Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange — forces cross-encoding path
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.ByteIndexOf("xyz");

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ByteIndexOf_String_NullOrEmpty_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(haystack.ByteIndexOf("")).IsEqualTo(0);
    }

    // ByteIndexOf(ReadOnlySpan<byte>) paths

    [Test]
    public async Task ByteIndexOf_SpanByte_SameEncoding_Match_ReturnsBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.ByteIndexOf("World"u8);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_SpanByte_SameEncoding_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.ByteIndexOf("xyz"u8);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task ByteIndexOf_SpanByte_CrossEncoding_Match_ReturnsBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act
        var result = haystack.ByteIndexOf(needleBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_SpanByte_Empty_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.ByteIndexOf(ReadOnlySpan<byte>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // ByteIndexOf(ReadOnlySpan<char>) paths

    [Test]
    public async Task ByteIndexOf_SpanChar_Utf16Haystack_Match_ReturnsBytePosition()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("Hello World");

        // Act — "World" at char 6 = byte 12
        var result = haystack.ByteIndexOf("World".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(12);
    }

    [Test]
    public async Task ByteIndexOf_SpanChar_Utf8Haystack_Match_ReturnsBytePosition()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("Hello World"u8);

        // Act
        var result = haystack.ByteIndexOf("World".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_SpanChar_Empty_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.ByteIndexOf(ReadOnlySpan<char>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // ByteIndexOf(ReadOnlySpan<int>) paths

    [Test]
    public async Task ByteIndexOf_SpanInt_Utf32Haystack_Match_ReturnsBytePosition()
    {
        // Arrange — exercises Encoding == Utf32 fast path
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act — "World" at rune 6 = byte 24
        var result = haystack.ByteIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(24);
    }

    [Test]
    public async Task ByteIndexOf_SpanInt_Utf8Haystack_Match_ReturnsBytePosition()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("Hello World"u8);
        var needleBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.ByteIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_SpanInt_Empty_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.ByteIndexOf(ReadOnlySpan<int>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // ByteIndexOf(TextSpan) internal overload

    [Test]
    public async Task ByteIndexOf_TextSpan_SameEncoding_Match_ReturnsBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needleText = Text.FromUtf8("World"u8);
        var needleSpan = needleText.AsSpan();

        // Act
        var result = haystack.ByteIndexOf(needleSpan);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_TextSpan_CrossEncoding_Match_ReturnsBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello World"u8);
        var needleText = Text.From("World");
        var needleSpan = needleText.AsSpan();

        // Act
        var result = haystack.ByteIndexOf(needleSpan);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_TextSpan_Empty_ReturnsZero()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);
        var emptySpan = Text.Empty.AsSpan();

        // Act
        var result = haystack.ByteIndexOf(emptySpan);

        // Assert
        await Assert.That(result).IsEqualTo(0);
    }

    // ──────────────────────────────────────────────
    //  LastByteIndexOf — same-encoding, cross-encoding, miss, empty
    // ──────────────────────────────────────────────

    [Test]
    public async Task LastByteIndexOf_Utf8SameEncoding_Match_ReturnsLastBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needle = Text.FromUtf8("abc"u8);

        // Act
        var result = haystack.LastByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_Utf8SameEncoding_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needle = Text.FromUtf8("xyz"u8);

        // Act
        var result = haystack.LastByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastByteIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_Match_ReturnsLastBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needle = Text.From("abc");

        // Act
        var result = haystack.LastByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_CrossEncoding_Utf8HaystackUtf16Needle_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needle = Text.From("xyz");

        // Act
        var result = haystack.LastByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastByteIndexOf_Utf16SameEncoding_Match_ReturnsLastBytePosition()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("abcabc");

        // Act — "abc" last occurrence at char 3 = byte 6
        var result = haystack.LastByteIndexOf("abc");

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task LastByteIndexOf_Utf32SameEncoding_Match_ReturnsLastBytePosition()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("abcabc", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("abc", TextEncoding.Utf32);
        var needle = Text.FromBytes(needleBytes, TextEncoding.Utf32);

        // Act — "abc" last occurrence at rune 3 = byte 12
        var result = haystack.LastByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(12);
    }

    [Test]
    public async Task LastByteIndexOf_EmptyNeedle_ReturnsByteLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);
        var needle = Text.Empty;

        // Act
        var result = haystack.LastByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    // LastByteIndexOf(string) paths

    [Test]
    public async Task LastByteIndexOf_String_Utf16Haystack_Match_ReturnsLastBytePosition()
    {
        // Arrange — exercises Encoding == Utf16 fast path
        var haystack = Text.From("abcabc");

        // Act — "abc" last at char 3 = byte 6
        var result = haystack.LastByteIndexOf("abc");

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task LastByteIndexOf_String_Utf8Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange — forces cross-encoding path
        var haystack = Text.FromUtf8("abcabc"u8);

        // Act
        var result = haystack.LastByteIndexOf("xyz");

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastByteIndexOf_String_NullOrEmpty_ReturnsByteLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(haystack.LastByteIndexOf("")).IsEqualTo(5);
    }

    // LastByteIndexOf(ReadOnlySpan<byte>) paths

    [Test]
    public async Task LastByteIndexOf_SpanByte_SameEncoding_Match_ReturnsLastBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);

        // Act
        var result = haystack.LastByteIndexOf("abc"u8);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_SpanByte_SameEncoding_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);

        // Act
        var result = haystack.LastByteIndexOf("xyz"u8);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastByteIndexOf_SpanByte_CrossEncoding_Match_ReturnsLastBytePosition()
    {
        // Arrange
        var haystack = Text.FromUtf8("abcabc"u8);
        var needleBytes = TestHelpers.Encode("abc", TextEncoding.Utf16);

        // Act
        var result = haystack.LastByteIndexOf(needleBytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_SpanByte_Empty_ReturnsByteLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.LastByteIndexOf(ReadOnlySpan<byte>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    // LastByteIndexOf(ReadOnlySpan<char>) paths

    [Test]
    public async Task LastByteIndexOf_SpanChar_Utf16Haystack_Match_ReturnsLastBytePosition()
    {
        // Arrange
        var haystack = Text.From("abcabc");

        // Act — "abc" last at char 3 = byte 6
        var result = haystack.LastByteIndexOf("abc".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task LastByteIndexOf_SpanChar_Utf16Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystack = Text.From("abcabc");

        // Act
        var result = haystack.LastByteIndexOf("xyz".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastByteIndexOf_SpanChar_Utf8Haystack_Match_ReturnsLastBytePosition()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("abcabc"u8);

        // Act
        var result = haystack.LastByteIndexOf("abc".AsSpan());

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_SpanChar_Empty_ReturnsByteLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.LastByteIndexOf(ReadOnlySpan<char>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    // LastByteIndexOf(ReadOnlySpan<int>) paths

    [Test]
    public async Task LastByteIndexOf_SpanInt_Utf32Haystack_Match_ReturnsLastBytePosition()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("abcabc", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("abc", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act — "abc" last at rune 3 = byte 12
        var result = haystack.LastByteIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(12);
    }

    [Test]
    public async Task LastByteIndexOf_SpanInt_Utf32Haystack_Miss_ReturnsNegativeOne()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("abcabc", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);
        var needleBytes = TestHelpers.Encode("xyz", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.LastByteIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(-1);
    }

    [Test]
    public async Task LastByteIndexOf_SpanInt_Utf8Haystack_Match_ReturnsLastBytePosition()
    {
        // Arrange — cross-encoding
        var haystack = Text.FromUtf8("abcabc"u8);
        var needleBytes = TestHelpers.Encode("abc", TextEncoding.Utf32);
        var needleInts = MemoryMarshal.Cast<byte, int>(needleBytes);

        // Act
        var result = haystack.LastByteIndexOf(needleInts);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_SpanInt_Empty_ReturnsByteLength()
    {
        // Arrange
        var haystack = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.LastByteIndexOf(ReadOnlySpan<int>.Empty);

        // Assert
        await Assert.That(result).IsEqualTo(5);
    }

    // ──────────────────────────────────────────────
    //  Cross-encoding combos: UTF-16 haystack + UTF-8 needle
    // ──────────────────────────────────────────────

    [Test]
    public async Task Contains_Utf16Haystack_Utf8Needle_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.From("Hello World");
        var needle = Text.FromUtf8("World"u8);

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_Utf16Haystack_Utf8Needle_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.From("Hello World");
        var needle = Text.FromUtf8("Hello"u8);

        // Act
        var result = haystack.StartsWith(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_Utf16Haystack_Utf8Needle_Match_ReturnsTrue()
    {
        // Arrange
        var haystack = Text.From("Hello World");
        var needle = Text.FromUtf8("World"u8);

        // Act
        var result = haystack.EndsWith(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task RuneIndexOf_Utf16Haystack_Utf8Needle_Match_ReturnsPosition()
    {
        // Arrange
        var haystack = Text.From("Hello World");
        var needle = Text.FromUtf8("World"u8);

        // Act
        var result = haystack.RuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_Utf16Haystack_Utf8Needle_Match_ReturnsBytePosition()
    {
        // Arrange — UTF-16 haystack, UTF-8 needle; byte position in UTF-16 bytes
        var haystack = Text.From("Hello World");
        var needle = Text.FromUtf8("World"u8);

        // Act — "World" at char 6 = byte 12 in UTF-16
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(12);
    }

    [Test]
    public async Task LastByteIndexOf_Utf16Haystack_Utf8Needle_Match_ReturnsLastBytePosition()
    {
        // Arrange
        var haystack = Text.From("abcabc");
        var needle = Text.FromUtf8("abc"u8);

        // Act — "abc" last at char 3 = byte 6 in UTF-16
        var result = haystack.LastByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    // ──────────────────────────────────────────────
    //  Cross-encoding combos: UTF-32 haystack + string needle
    // ──────────────────────────────────────────────

    [Test]
    public async Task Contains_Utf32Haystack_StringNeedle_Match_ReturnsTrue()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);

        // Act
        var result = haystack.Contains("World");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task StartsWith_Utf32Haystack_StringNeedle_Match_ReturnsTrue()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);

        // Act
        var result = haystack.StartsWith("Hello");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EndsWith_Utf32Haystack_StringNeedle_Match_ReturnsTrue()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);

        // Act
        var result = haystack.EndsWith("World");

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task RuneIndexOf_Utf32Haystack_StringNeedle_Match_ReturnsPosition()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("Hello World", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);

        // Act
        var result = haystack.RuneIndexOf("World");

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task LastRuneIndexOf_Utf32Haystack_StringNeedle_Match_ReturnsLastPosition()
    {
        // Arrange
        var haystackBytes = TestHelpers.Encode("abcabc", TextEncoding.Utf32);
        var haystack = Text.FromBytes(haystackBytes, TextEncoding.Utf32);

        // Act
        var result = haystack.LastRuneIndexOf("abc");

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    // ──────────────────────────────────────────────
    //  Multi-byte / emoji cross-encoding verification
    // ──────────────────────────────────────────────

    [Test]
    public async Task Contains_CrossEncoding_Emoji_ReturnsTrue()
    {
        // Arrange — emoji is multi-byte in UTF-8 (4 bytes), 2 code units in UTF-16
        var haystack = Text.FromUtf8("Hello 🎉 World"u8);
        var needle = Text.From("🎉");

        // Act
        var result = haystack.Contains(needle);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task RuneIndexOf_CrossEncoding_Emoji_ReturnsRunePosition()
    {
        // Arrange — "Hello " = 6 runes, then emoji at rune 6
        var haystack = Text.FromUtf8("Hello 🎉 World"u8);
        var needle = Text.From("🎉");

        // Act
        var result = haystack.RuneIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_CrossEncoding_Emoji_ReturnsBytePosition()
    {
        // Arrange — "Hello " = 6 bytes in UTF-8, emoji starts at byte 6
        var haystack = Text.FromUtf8("Hello 🎉 World"u8);
        var needle = Text.From("🎉");

        // Act
        var result = haystack.ByteIndexOf(needle);

        // Assert
        await Assert.That(result).IsEqualTo(6);
    }
}
