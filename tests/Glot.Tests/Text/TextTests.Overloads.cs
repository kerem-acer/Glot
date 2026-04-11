using System.Runtime.InteropServices;

namespace Glot.Tests;

public partial class TextTests
{
    // Contains overloads

    [Test]
    public async Task Contains_Text_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello World");
        var search = Text.FromUtf8("World"u8);

        // Act & Assert
        await Assert.That(text.Contains(search)).IsTrue();
    }

    [Test]
    public async Task Contains_ReadOnlySpanChar_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act & Assert
        await Assert.That(text.Contains("World".AsSpan())).IsTrue();
    }

    [Test]
    public async Task Contains_ReadOnlySpanInt_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello World");
        var searchBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var searchInts = MemoryMarshal.Cast<byte, int>(searchBytes);

        // Act
        var result = text.Contains(searchInts);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Contains_ReadOnlySpanByte_Utf16Encoding_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello World");
        var searchBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act & Assert
        await Assert.That(text.Contains(searchBytes, TextEncoding.Utf16)).IsTrue();
    }

    // StartsWith overloads

    [Test]
    public async Task StartsWith_Text_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello");
        var prefix = Text.FromUtf8("He"u8);

        // Act & Assert
        await Assert.That(text.StartsWith(prefix)).IsTrue();
    }

    [Test]
    public async Task StartsWith_ReadOnlySpanChar_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(text.StartsWith("He".AsSpan())).IsTrue();
    }

    [Test]
    public async Task StartsWith_ReadOnlySpanInt_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello");
        var prefixBytes = TestHelpers.Encode("He", TextEncoding.Utf32);
        var prefixInts = MemoryMarshal.Cast<byte, int>(prefixBytes);

        // Act & Assert
        await Assert.That(text.StartsWith(prefixInts)).IsTrue();
    }

    [Test]
    public async Task StartsWith_ReadOnlySpanByte_Utf8_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(text.StartsWith("He"u8)).IsTrue();
    }

    // EndsWith overloads

    [Test]
    public async Task EndsWith_Text_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello");
        var suffix = Text.FromUtf8("lo"u8);

        // Act & Assert
        await Assert.That(text.EndsWith(suffix)).IsTrue();
    }

    [Test]
    public async Task EndsWith_ReadOnlySpanChar_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(text.EndsWith("lo".AsSpan())).IsTrue();
    }

    [Test]
    public async Task EndsWith_ReadOnlySpanInt_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello");
        var suffixBytes = TestHelpers.Encode("lo", TextEncoding.Utf32);
        var suffixInts = MemoryMarshal.Cast<byte, int>(suffixBytes);

        // Act & Assert
        await Assert.That(text.EndsWith(suffixInts)).IsTrue();
    }

    [Test]
    public async Task EndsWith_ReadOnlySpanByte_Utf8_ReturnsTrue()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(text.EndsWith("lo"u8)).IsTrue();
    }

    // RuneIndexOf overloads

    [Test]
    public async Task RuneIndexOf_Text_ReturnsPosition()
    {
        // Arrange
        var text = Text.From("Hello World");
        var search = Text.FromUtf8("World"u8);

        // Act & Assert
        await Assert.That(text.RuneIndexOf(search)).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_ReadOnlySpanChar_ReturnsPosition()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act & Assert
        await Assert.That(text.RuneIndexOf("World".AsSpan())).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_ReadOnlySpanInt_ReturnsPosition()
    {
        // Arrange
        var text = Text.From("Hello World");
        var searchBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var searchInts = MemoryMarshal.Cast<byte, int>(searchBytes);

        // Act & Assert
        await Assert.That(text.RuneIndexOf(searchInts)).IsEqualTo(6);
    }

    [Test]
    public async Task RuneIndexOf_ReadOnlySpanByte_Utf16Encoding_ReturnsPosition()
    {
        // Arrange
        var text = Text.From("Hello World");
        var searchBytes = TestHelpers.Encode("World", TextEncoding.Utf16);

        // Act & Assert
        await Assert.That(text.RuneIndexOf(searchBytes, TextEncoding.Utf16)).IsEqualTo(6);
    }

    // LastRuneIndexOf overloads

    [Test]
    public async Task LastRuneIndexOf_Text_ReturnsLastPosition()
    {
        // Arrange
        var text = Text.From("abcabc");
        var search = Text.FromUtf8("abc"u8);

        // Act & Assert
        await Assert.That(text.LastRuneIndexOf(search)).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_String_ReturnsLastPosition()
    {
        // Arrange
        var text = Text.From("abcabc");

        // Act & Assert
        await Assert.That(text.LastRuneIndexOf("abc")).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_ReadOnlySpanChar_ReturnsLastPosition()
    {
        // Arrange
        var text = Text.From("abcabc");

        // Act & Assert
        await Assert.That(text.LastRuneIndexOf("abc".AsSpan())).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_ReadOnlySpanInt_ReturnsLastPosition()
    {
        // Arrange
        var text = Text.From("abcabc");
        var searchBytes = TestHelpers.Encode("abc", TextEncoding.Utf32);
        var searchInts = MemoryMarshal.Cast<byte, int>(searchBytes);

        // Act & Assert
        await Assert.That(text.LastRuneIndexOf(searchInts)).IsEqualTo(3);
    }

    [Test]
    public async Task LastRuneIndexOf_ReadOnlySpanByte_Utf8_ReturnsLastPosition()
    {
        // Arrange
        var text = Text.From("abcabc");

        // Act & Assert
        await Assert.That(text.LastRuneIndexOf("abc"u8)).IsEqualTo(3);
    }

    // ByteIndexOf overloads

    [Test]
    public async Task ByteIndexOf_Text_ReturnsBytePosition()
    {
        // Arrange
        var text = Text.FromUtf8("abcabc"u8);
        var search = Text.FromUtf8("abc"u8);

        // Act & Assert
        await Assert.That(text.ByteIndexOf(search)).IsEqualTo(0);
    }

    [Test]
    public async Task ByteIndexOf_String_ReturnsBytePosition()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);

        // Act & Assert
        await Assert.That(text.ByteIndexOf("World")).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_ReadOnlySpanChar_ReturnsBytePosition()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);

        // Act & Assert
        await Assert.That(text.ByteIndexOf("World".AsSpan())).IsEqualTo(6);
    }

    [Test]
    public async Task ByteIndexOf_ReadOnlySpanInt_ReturnsBytePosition()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);
        var searchBytes = TestHelpers.Encode("World", TextEncoding.Utf32);
        var searchInts = MemoryMarshal.Cast<byte, int>(searchBytes);

        // Act & Assert
        await Assert.That(text.ByteIndexOf(searchInts)).IsEqualTo(6);
    }

    // LastByteIndexOf overloads

    [Test]
    public async Task LastByteIndexOf_Text_ReturnsLastBytePosition()
    {
        // Arrange
        var text = Text.FromUtf8("abcabc"u8);
        var search = Text.FromUtf8("abc"u8);

        // Act & Assert
        await Assert.That(text.LastByteIndexOf(search)).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_String_ReturnsLastBytePosition()
    {
        // Arrange
        var text = Text.FromUtf8("abcabc"u8);

        // Act & Assert
        await Assert.That(text.LastByteIndexOf("abc")).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_ReadOnlySpanChar_ReturnsLastBytePosition()
    {
        // Arrange
        var text = Text.FromUtf8("abcabc"u8);

        // Act & Assert
        await Assert.That(text.LastByteIndexOf("abc".AsSpan())).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_ReadOnlySpanInt_ReturnsLastBytePosition()
    {
        // Arrange
        var text = Text.FromUtf8("abcabc"u8);
        var searchBytes = TestHelpers.Encode("abc", TextEncoding.Utf32);
        var searchInts = MemoryMarshal.Cast<byte, int>(searchBytes);

        // Act & Assert
        await Assert.That(text.LastByteIndexOf(searchInts)).IsEqualTo(3);
    }

    [Test]
    public async Task LastByteIndexOf_ReadOnlySpanByte_Utf8_ReturnsLastBytePosition()
    {
        // Arrange
        var text = Text.FromUtf8("abcabc"u8);

        // Act & Assert
        await Assert.That(text.LastByteIndexOf("abc"u8)).IsEqualTo(3);
    }

    // Equals overloads

    [Test]
    public async Task Equals_ReadOnlySpanByte_Utf8_ReturnsTrue()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(text.Equals("Hello"u8)).IsTrue();
    }

    [Test]
    public async Task Equals_ReadOnlySpanChar_ReturnsTrue()
    {
        // Arrange
        var text = Text.FromUtf8("Hello"u8);

        // Act & Assert
        await Assert.That(text.Equals("Hello".AsSpan())).IsTrue();
    }

    [Test]
    public async Task Equals_ReadOnlySpanInt_ReturnsTrue()
    {
        // Arrange
        var text = Text.FromUtf8("ABC"u8);
        ReadOnlySpan<int> codePoints = [0x41, 0x42, 0x43];

        // Act
        var result = text.Equals(codePoints);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Equals_Object_Text_ReturnsTrue()
    {
        // Arrange
        var a = Text.From("Hello");
        object b = Text.From("Hello");

        // Act & Assert
        await Assert.That(a.Equals(b)).IsTrue();
    }

    [Test]
    public async Task Equals_Object_NonText_ReturnsFalse()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act & Assert
        await Assert.That(text.Equals("Hello")).IsTrue();
        await Assert.That(text.Equals((object)"Hello")).IsFalse();
    }

    // Split overloads

    [Test]
    public async Task Split_Text_SplitsCorrectly()
    {
        // Arrange
        var text = Text.From("a,b,c");
        var sep = Text.FromUtf8(","u8);

        // Act
        var count = 0;
        foreach (var _ in text.Split(sep))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_ReadOnlySpanChar_SplitsCorrectly()
    {
        // Arrange
        var text = Text.From("a,b,c");

        // Act
        var count = 0;
        foreach (var _ in text.Split(",".AsSpan()))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_ReadOnlySpanInt_SplitsCorrectly()
    {
        // Arrange
        var text = Text.From("a,b,c");
        var sepBytes = TestHelpers.Encode(",", TextEncoding.Utf32);
        var sepInts = MemoryMarshal.Cast<byte, int>(sepBytes);

        // Act
        var count = 0;
        foreach (var _ in text.Split(sepInts))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    [Test]
    public async Task Split_ReadOnlySpanByte_Utf16_SplitsCorrectly()
    {
        // Arrange
        var text = Text.From("a,b,c");
        var sepBytes = TestHelpers.Encode(",", TextEncoding.Utf16);

        // Act
        var count = 0;
        foreach (var _ in text.Split(sepBytes, TextEncoding.Utf16))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }

    // char[] and int[] backing paths

    [Test]
    public Task FromChars_AsSpan_WorksCorrectly()
    {
        // Arrange
        var text = Text.FromChars("café".AsSpan());

        // Assert
        return Verify(new { result = text.ToString(), text.RuneLength });
    }

    [Test]
    public async Task FromUtf32_IntSpan_AsSpan_WorksCorrectly()
    {
        // Arrange
        ReadOnlySpan<int> codePoints = [0x48, 0x65, 0x6C, 0x6C, 0x6F]; // Hello

        // Act
        var text = Text.FromUtf32(codePoints);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public Task FromChars_Trim_NonCopying()
    {
        // Arrange
        var text = Text.FromChars("  Hi  ".AsSpan());

        // Act
        var trimmed = text.Trim();

        // Assert
        return Verify(new { result = trimmed.ToString(), trimmed.RuneLength });
    }

    [Test]
    public Task FromBytes_Utf32_WorksCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf32);

        // Act
        var text = Text.FromBytes(bytes, TextEncoding.Utf32);

        // Assert
        return Verify(new { result = text.ToString(), text.Encoding });
    }
}
