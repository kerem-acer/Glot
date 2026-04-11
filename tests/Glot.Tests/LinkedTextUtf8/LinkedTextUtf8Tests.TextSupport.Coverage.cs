namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    // Owned creation from Text values exercises the pooled AppendTextSpan path

    [Test]
    public async Task CreateOwned_FromUtf16Text_TranscodesToUtf8()
    {
        // Arrange
        var utf16 = Text.From("Hello");

        // Act
        using var owned = LinkedTextUtf8.CreateOwned(utf16);
        var result = owned.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello");
    }

    [Test]
    public async Task CreateOwned_FromMixedEncodingTexts_Works()
    {
        // Arrange
        var utf16 = Text.From("Hello");
        var utf8 = Text.FromUtf8(" World"u8);

        // Act
        using var owned = LinkedTextUtf8.CreateOwned(utf16, utf8);
        var result = owned.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("Hello World");
    }

    // CreateOwned from ReadOnlyMemory<byte> segments

    [Test]
    public async Task CreateOwned_FromMemorySegments_Works()
    {
        // Arrange
        var bytes1 = "hello"u8.ToArray().AsMemory();
        var bytes2 = " world"u8.ToArray().AsMemory();

        // Act
        using var owned = LinkedTextUtf8.CreateOwned(bytes1, bytes2);
        var result = owned.AsSpan().ToString();

        // Assert
        await Assert.That(result).IsEqualTo("hello world");
    }
}
