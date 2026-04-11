namespace Glot.Tests;

public class OwnedTextTests
{
    // Factory — FromUtf8

    [Test]
    public async Task FromUtf8_StoresCorrectEncoding()
    {
        // Arrange & Act
        using var owned = OwnedText.FromUtf8("Hello"u8);

        // Assert
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(owned.RuneLength).IsEqualTo(5);
        await Assert.That(owned.ByteLength).IsEqualTo(5);
        await Assert.That(owned.IsEmpty).IsFalse();
    }

    [Test]
    public async Task FromUtf8_MultiByte_CountsRunesCorrectly()
    {
        // Arrange & Act
        using var owned = OwnedText.FromUtf8("café🎉"u8);

        // Assert
        await Assert.That(owned.RuneLength).IsEqualTo(5);
        await Assert.That(owned.ByteLength).IsEqualTo(9); // c(1) a(1) f(1) é(2) 🎉(4)
    }

    [Test]
    public async Task FromUtf8_Empty_ReturnsDefault()
    {
        // Act
        using var owned = OwnedText.FromUtf8([]);

        // Assert
        await Assert.That(owned.IsEmpty).IsTrue();
        await Assert.That(owned.ByteLength).IsEqualTo(0);
    }

    // Factory — FromChars

    [Test]
    public async Task FromChars_StoresUtf16()
    {
        // Arrange & Act
        using var owned = OwnedText.FromChars("Hello".AsSpan());

        // Assert
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(owned.RuneLength).IsEqualTo(5);
        await Assert.That(owned.ByteLength).IsEqualTo(10);
    }

    [Test]
    public async Task FromChars_Empty_ReturnsDefault()
    {
        // Act
        using var owned = OwnedText.FromChars([]);

        // Assert
        await Assert.That(owned.IsEmpty).IsTrue();
    }

    // Factory — FromUtf32

    [Test]
    public async Task FromUtf32_StoresUtf32()
    {
        // Arrange
        ReadOnlySpan<int> codePoints = [0x48, 0x65, 0x6C]; // H, e, l

        // Act
        using var owned = OwnedText.FromUtf32(codePoints);

        // Assert
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf32);
        await Assert.That(owned.RuneLength).IsEqualTo(3);
        await Assert.That(owned.ByteLength).IsEqualTo(12);
    }

    [Test]
    public async Task FromUtf32_Empty_ReturnsDefault()
    {
        // Act
        using var owned = OwnedText.FromUtf32([]);

        // Assert
        await Assert.That(owned.IsEmpty).IsTrue();
    }

    // Factory — FromBytes

    [Test]
    public async Task FromBytes_Utf16_StoresCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);

        // Act
        using var owned = OwnedText.FromBytes(bytes, TextEncoding.Utf16);

        // Assert
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(owned.RuneLength).IsEqualTo(5);
    }

    // Create — zero-copy from existing pooled buffer

    [Test]
    public async Task Create_TakesOwnershipOfBuffer()
    {
        // Arrange
        var buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(5);
        "Hello"u8.CopyTo(buffer);

        // Act
        using var owned = OwnedText.Create(buffer, 5, TextEncoding.Utf8);

        // Assert
        await Assert.That(owned.Text.ToString()).IsEqualTo("Hello");
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(owned.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task Create_Utf16Buffer_WorksCorrectly()
    {
        // Arrange
        var source = TestHelpers.Encode("café", TextEncoding.Utf16);
        var buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(source.Length);
        source.CopyTo(buffer, 0);

        // Act
        using var owned = OwnedText.Create(buffer, source.Length, TextEncoding.Utf16);

        // Assert
        await Assert.That(owned.Text.ToString()).IsEqualTo("café");
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task Create_CharBuffer_TakesOwnership()
    {
        // Arrange
        var buffer = System.Buffers.ArrayPool<char>.Shared.Rent(5);
        "Hello".AsSpan().CopyTo(buffer);

        // Act
        using var owned = OwnedText.Create(buffer, 5);

        // Assert
        await Assert.That(owned.Text.ToString()).IsEqualTo("Hello");
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf16);
        await Assert.That(owned.RuneLength).IsEqualTo(5);
        await Assert.That(owned.ByteLength).IsEqualTo(10);
    }

    [Test]
    public async Task Create_IntBuffer_TakesOwnership()
    {
        // Arrange
        var buffer = System.Buffers.ArrayPool<int>.Shared.Rent(3);
        buffer[0] = 0x48; // H
        buffer[1] = 0x69; // i
        buffer[2] = 0x21; // !

        // Act
        using var owned = OwnedText.Create(buffer, 3);

        // Assert
        await Assert.That(owned.Text.ToString()).IsEqualTo("Hi!");
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf32);
        await Assert.That(owned.RuneLength).IsEqualTo(3);
        await Assert.That(owned.ByteLength).IsEqualTo(12);
    }

    // Text property

    [Test]
    public async Task Text_ReturnsCorrectValue()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("Hello"u8);

        // Act
        var text = owned.Text;

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("Hello");
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
        await Assert.That(text.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task Text_CrossEncodingEquality()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("Hello"u8);
        var directText = Text.From("Hello");

        // Act
        var eq = owned.Text.Equals(directText);

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task Text_MultiByte_CorrectContent()
    {
        // Arrange
        using var owned = OwnedText.FromChars("café🎉".AsSpan());

        // Act
        var result = owned.Text.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("café🎉");
    }

    // Dispose

    [Test]
    public async Task Dispose_Default_DoesNotThrow()
    {
        // Arrange
        var owned = default(OwnedText);

        // Act & Assert — no exception
        owned.Dispose();
        await Assert.That(owned.IsEmpty).IsTrue();
    }

    // Using pattern

    [Test]
    public async Task UsingPattern_TypicalLifetime()
    {
        // Arrange
        string result;

        // Act
        using (var owned = OwnedText.FromUtf8("Hello World"u8))
        {
            result = owned.Text.ToString();
        }

        // Assert
        await Assert.That(result).IsEqualTo("Hello World");
    }

    // Pool stress — ensure no crash under high-volume create/dispose

    [Test]
    public async Task PoolStress_ManyAllocations_NoCrash()
    {
        // Act
        var lastRuneLength = 0;
        for (var i = 0; i < 10000; i++)
        {
            using var owned = OwnedText.FromUtf8("Hello World café 🎉"u8);
            lastRuneLength = owned.Text.RuneLength;
        }

        // Assert — no exception, no pool corruption
        await Assert.That(lastRuneLength).IsGreaterThan(0);
    }

    // Operations via Text

    [Test]
    public async Task Text_SearchOperations_Work()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("Hello World"u8);
        var text = owned.Text;

        // Act & Assert
        await Assert.That(text.Contains("World")).IsTrue();
        await Assert.That(text.StartsWith("Hello")).IsTrue();
        await Assert.That(text.EndsWith("World")).IsTrue();
    }

    [Test]
    public async Task Text_TrimOperations_Work()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("  Hello  "u8);

        // Act
        var trimmed = owned.Text.Trim();

        // Assert
        await Assert.That(trimmed.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task Text_SplitOperations_Work()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("a,b,c"u8);

        // Act
        var count = 0;
        foreach (var _ in owned.Text.Split(","))
        {
            count++;
        }

        // Assert
        await Assert.That(count).IsEqualTo(3);
    }
}
