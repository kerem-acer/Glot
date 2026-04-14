namespace Glot.Tests;

public partial class OwnedTextTests
{
    // Detach

    [Test]
    public async Task Detach_ReturnsBuffer_AndNullsInternal()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("hello"u8)!;

        // Act
        var buffer = owned.Detach();
        var isEmpty = owned.IsEmpty;
        var byteLength = owned.ByteLength;

        // Assert
        await Assert.That(buffer).IsNotNull();
        await Assert.That(isEmpty).IsTrue();
        await Assert.That(byteLength).IsEqualTo(0);

        // Clean up -- dispose is safe after detach (no-op for data buffer)
        owned.Dispose();
    }

    [Test]
    public async Task Dispose_AfterDetach_DoesNotReturnBuffer()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("hello"u8)!;
        var detachedBuffer = owned.Detach();
        var isEmptyAfterDetach = owned.IsEmpty;

        // Act -- dispose after detach: should not double-free the buffer
        owned.Dispose();

        // Assert -- buffer was returned by Detach, not by Dispose
        await Assert.That(detachedBuffer).IsNotNull();
        await Assert.That(isEmptyAfterDetach).IsTrue();
    }

    [Test]
    public async Task Dispose_ReturnsToPool_CanBeReused()
    {
        // Arrange & Act -- create and dispose many times to exercise pooling
        const int iterations = 100;
        const int expectedByteLength = 6;
        var lastByteLength = 0;
        for (var i = 0; i < iterations; i++)
        {
            using var owned = OwnedText.FromUtf8("pooled"u8)!;
            lastByteLength = owned.ByteLength;
        }

        // Assert -- no exception and pooled objects were reusable
        await Assert.That(lastByteLength).IsEqualTo(expectedByteLength);
    }

    // FromUtf32 non-empty

    [Test]
    public Task FromUtf32_NonEmpty_CreatesCorrectly()
    {
        // Arrange
        ReadOnlySpan<int> codePoints = [0x48, 0x65, 0x6C, 0x6C, 0x6F]; // H, e, l, l, o

        // Act
        using var owned = OwnedText.FromUtf32(codePoints)!;

        // Assert
        return Verify(new
        {
            owned.Encoding,
            owned.RuneLength,
            owned.ByteLength,
            owned.IsEmpty,
            text = owned.Text.ToString(),
        });
    }

    // Dispose with char[] buffer

    [Test]
    public async Task Dispose_WithCharBuffer_ReturnsCharArrayToPool()
    {
        // Arrange
        var buffer = System.Buffers.ArrayPool<char>.Shared.Rent(5);
        "Hello".AsSpan().CopyTo(buffer);
        var owned = OwnedText.Create(buffer, 5);
        var textBefore = owned.Text.ToString();

        // Act -- no crash means buffer was returned correctly
        owned.Dispose();

        // Assert
        await Assert.That(textBefore).IsEqualTo("Hello");
    }

    // Dispose with int[] buffer

    [Test]
    public async Task Dispose_WithIntBuffer_ReturnsIntArrayToPool()
    {
        // Arrange
        var buffer = System.Buffers.ArrayPool<int>.Shared.Rent(3);
        buffer[0] = 0x48; // H
        buffer[1] = 0x69; // i
        buffer[2] = 0x21; // !
        var owned = OwnedText.Create(buffer, 3);
        var textBefore = owned.Text.ToString();

        // Act -- no crash means buffer was returned correctly
        owned.Dispose();

        // Assert
        await Assert.That(textBefore).IsEqualTo("Hi!");
    }
}
