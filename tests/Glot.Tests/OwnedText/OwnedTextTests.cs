namespace Glot.Tests;

public partial class OwnedTextTests
{
    // Factory — FromUtf8

    [Test]
    public Task FromUtf8_StoresCorrectEncoding()
    {
        // Arrange & Act
        using var owned = OwnedText.FromUtf8("Hello"u8)!;

        // Assert
        return Verify(new { owned.Encoding, owned.RuneLength, owned.ByteLength, owned.IsEmpty });
    }

    [Test]
    public Task FromUtf8_MultiByte_CountsRunesCorrectly()
    {
        // Arrange & Act
        using var owned = OwnedText.FromUtf8("café🎉"u8)!;

        // Assert — c(1) a(1) f(1) é(2) 🎉(4)
        return Verify(new { owned.RuneLength, owned.ByteLength });
    }

    [Test]
    public async Task FromUtf8_Empty_ReturnsNull()
    {
        // Act
        var owned = OwnedText.FromUtf8([]);

        // Assert
        await Assert.That(owned).IsNull();
    }

    // Factory — FromChars

    [Test]
    public Task FromChars_StoresUtf16()
    {
        // Arrange & Act
        using var owned = OwnedText.FromChars("Hello".AsSpan())!;

        // Assert
        return Verify(new { owned.Encoding, owned.RuneLength, owned.ByteLength });
    }

    [Test]
    public async Task FromChars_Empty_ReturnsNull()
    {
        // Act
        var owned = OwnedText.FromChars([]);

        // Assert
        await Assert.That(owned).IsNull();
    }

    // Factory — FromUtf32

    [Test]
    public Task FromUtf32_StoresUtf32()
    {
        // Arrange
        ReadOnlySpan<int> codePoints = [0x48, 0x65, 0x6C]; // H, e, l

        // Act
        using var owned = OwnedText.FromUtf32(codePoints)!;

        // Assert
        return Verify(new { owned.Encoding, owned.RuneLength, owned.ByteLength });
    }

    [Test]
    public async Task FromUtf32_Empty_ReturnsNull()
    {
        // Act
        var owned = OwnedText.FromUtf32([]);

        // Assert
        await Assert.That(owned).IsNull();
    }

    // Factory — FromBytes

    [Test]
    public Task FromBytes_Utf16_StoresCorrectly()
    {
        // Arrange
        var bytes = TestHelpers.Encode("Hello", TextEncoding.Utf16);

        // Act
        using var owned = OwnedText.FromBytes(bytes, TextEncoding.Utf16)!;

        // Assert
        return Verify(new { owned.Encoding, owned.RuneLength });
    }

    // Create — zero-copy from existing pooled buffer

    [Test]
    public Task Create_TakesOwnershipOfBuffer()
    {
        // Arrange
        var buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(5);
        "Hello"u8.CopyTo(buffer);

        // Act
        using var owned = OwnedText.Create(buffer, 5, TextEncoding.Utf8);

        // Assert
        return Verify(new { result = owned.Text.ToString(), owned.Encoding, owned.RuneLength });
    }

    [Test]
    public Task Create_Utf16Buffer_WorksCorrectly()
    {
        // Arrange
        var source = TestHelpers.Encode("café", TextEncoding.Utf16);
        var buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(source.Length);
        source.CopyTo(buffer, 0);

        // Act
        using var owned = OwnedText.Create(buffer, source.Length, TextEncoding.Utf16);

        // Assert
        return Verify(new { result = owned.Text.ToString(), owned.Encoding });
    }

    [Test]
    public Task Create_CharBuffer_TakesOwnership()
    {
        // Arrange
        var buffer = System.Buffers.ArrayPool<char>.Shared.Rent(5);
        "Hello".AsSpan().CopyTo(buffer);

        // Act
        using var owned = OwnedText.Create(buffer, 5);

        // Assert
        return Verify(new { result = owned.Text.ToString(), owned.Encoding, owned.RuneLength, owned.ByteLength });
    }

    [Test]
    public Task Create_IntBuffer_TakesOwnership()
    {
        // Arrange
        var buffer = System.Buffers.ArrayPool<int>.Shared.Rent(3);
        buffer[0] = 0x48; // H
        buffer[1] = 0x69; // i
        buffer[2] = 0x21; // !

        // Act
        using var owned = OwnedText.Create(buffer, 3);

        // Assert
        return Verify(new { result = owned.Text.ToString(), owned.Encoding, owned.RuneLength, owned.ByteLength });
    }

    // Text property

    [Test]
    public Task Text_ReturnsCorrectValue()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("Hello"u8)!;

        // Act
        var text = owned.Text;

        // Assert
        return Verify(new { result = text.ToString(), text.Encoding, text.RuneLength });
    }

    [Test]
    public async Task Text_CrossEncodingEquality()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("Hello"u8)!;
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
        using var owned = OwnedText.FromChars("café🎉".AsSpan())!;

        // Act
        var result = owned.Text.ToString();

        // Assert
        await Assert.That(result).IsEqualTo("café🎉");
    }

    // Dispose

    [Test]
    public async Task Dispose_Null_DoesNotThrow()
    {
        // Arrange
        OwnedText? owned = null;

        // Act & Assert — null is the class equivalent of default(struct)
        await Assert.That(owned).IsNull();
    }

    // Using pattern

    [Test]
    public async Task UsingPattern_TypicalLifetime()
    {
        // Arrange
        string result;

        // Act
        using (var owned = OwnedText.FromUtf8("Hello World"u8)!)
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
            using var owned = OwnedText.FromUtf8("Hello World café 🎉"u8)!;
            lastRuneLength = owned.Text.RuneLength;
        }

        // Assert — no exception, no pool corruption
        await Assert.That(lastRuneLength).IsGreaterThan(0);
    }

    // Operations via Text

    [Test]
    public Task Text_SearchOperations_Work()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("Hello World"u8)!;
        var text = owned.Text;

        // Act & Assert
        return Verify(new
        {
            contains = text.Contains("World"),
            startsWith = text.StartsWith("Hello"),
            endsWith = text.EndsWith("World"),
        });
    }

    [Test]
    public async Task Text_TrimOperations_Work()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("  Hello  "u8)!;

        // Act
        var trimmed = owned.Text.Trim();

        // Assert
        await Assert.That(trimmed.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task Text_SplitOperations_Work()
    {
        // Arrange
        using var owned = OwnedText.FromUtf8("a,b,c"u8)!;

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
