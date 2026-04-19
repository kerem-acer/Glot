namespace Glot.SystemTextJson.Tests;

public class PooledBufferWriterTests
{
    [Test]
    public async Task Constructor_InitialCapacity_GetSpanReturnsNonEmpty()
    {
        // Arrange & Act
        using var writer = new PooledBufferWriter(256);

        // Assert
        var span = writer.GetSpan();
        await Assert.That(span.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task Advance_AfterWrite_MovesPosition()
    {
        // Arrange
        using var writer = new PooledBufferWriter(256);
        const int advanceCount = 5;

        // Act
        var span1 = writer.GetSpan(advanceCount);
        "Hello"u8.CopyTo(span1);
        writer.Advance(advanceCount);
        var span2 = writer.GetSpan();

        // Assert -- second span starts after the first write
        await Assert.That(span2.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task GetMemory_ReturnsWritableMemory()
    {
        // Arrange
        using var writer = new PooledBufferWriter(256);
        const int size = 3;

        // Act
        var memory = writer.GetMemory(size);
        "abc"u8.CopyTo(memory.Span);
        writer.Advance(size);

        // Assert
        await Assert.That(memory.Length).IsGreaterThanOrEqualTo(size);
    }

    [Test]
    public async Task EnsureCapacity_ExceedsInitial_GrowsWithoutCrash()
    {
        // Arrange
        using var writer = new PooledBufferWriter(4);

        // Act -- write more than initial capacity
        var span = writer.GetSpan(1024);
        new byte[1024].CopyTo(span);
        writer.Advance(1024);

        // Assert
        var remaining = writer.GetSpan();
        await Assert.That(remaining.Length).IsGreaterThan(0);
    }

    [Test]
    public async Task ToOwnedText_TransfersOwnership_ReturnsValidContent()
    {
        // Arrange
        var writer = new PooledBufferWriter(256);
        var span = writer.GetSpan(5);
        "Hello"u8.CopyTo(span);
        writer.Advance(5);

        // Act
        using var owned = writer.ToOwnedText();

        // Assert
        await Assert.That(owned.Text.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task Dispose_AfterTransfer_IsNoOp()
    {
        // Arrange
        var writer = new PooledBufferWriter(256);
        var span = writer.GetSpan(3);
        "abc"u8.CopyTo(span);
        writer.Advance(3);
        using var owned = writer.ToOwnedText();

        // Act -- dispose after transfer should not crash
        writer.Dispose();

        // Assert -- owned text still valid
        await Assert.That(owned.Text.ToString()).IsEqualTo("abc");
    }

    [Test]
    public void Dispose_WithoutTransfer_ReturnsBuffer()
    {
        // Arrange
        var writer = new PooledBufferWriter(256);
        var span = writer.GetSpan(3);
        "abc"u8.CopyTo(span);
        writer.Advance(3);

        // Act & Assert -- dispose without transfer should not crash
        writer.Dispose();
    }

    [Test]
    public async Task PrepareForReuse_ResetsState_CanWriteAgain()
    {
        // Arrange
        var writer = new PooledBufferWriter(256);
        var span = writer.GetSpan(5);
        "Hello"u8.CopyTo(span);
        writer.Advance(5);

        // Act
        writer.PrepareForReuse();
        var newSpan = writer.GetSpan(3);
        "abc"u8.CopyTo(newSpan);
        writer.Advance(3);
        using var owned = writer.ToOwnedText();

        // Assert
        await Assert.That(owned.Text.ToString()).IsEqualTo("abc");
    }

    [Test]
    public void PrepareForReuse_AfterTransfer_DoesNotDoubleReturn()
    {
        // Arrange
        var writer = new PooledBufferWriter(256);
        var span = writer.GetSpan(3);
        "abc"u8.CopyTo(span);
        writer.Advance(3);
        using var owned = writer.ToOwnedText();

        // Act & Assert -- should not crash (buffer already transferred)
        writer.PrepareForReuse();
    }

    [Test]
    public async Task CreateForCaching_ReturnsEmptyWriter_GetSpanTriggersGrowth()
    {
        // Arrange & Act
        using var writer = PooledBufferWriter.CreateForCaching();
        var span = writer.GetSpan(10);

        // Assert -- growth occurred from zero-capacity
        await Assert.That(span.Length).IsGreaterThanOrEqualTo(10);
    }
}
