namespace Glot.Tests;

public partial class OwnedLinkedTextUtf8Tests
{
    [Test]
    public async Task Create_Copy_CopiesOwnedTextData()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("copied"u8)!;

        // Act -- Copy mode: data is copied into the linked text
        using var linked = OwnedLinkedTextUtf8.Create(OwnedTextHandling.Copy, $"{owned}");
        var content = linked.AsSpan().ToString();

        // Assert -- OwnedText is still usable after copy
        await Assert.That(content).IsEqualTo("copied");
        await Assert.That(owned.IsEmpty).IsFalse();
        await Assert.That(owned.Text.ToString()).IsEqualTo("copied");

        owned.Dispose();
    }

    [Test]
    public async Task Create_TakeOwnership_DetachesBuffer()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("taken"u8)!;

        // Act -- TakeOwnership mode: buffer is detached from OwnedText
        using var linked = OwnedLinkedTextUtf8.Create(OwnedTextHandling.TakeOwnership, $"{owned}");
        var content = linked.AsSpan().ToString();

        // Assert -- OwnedText is empty after take, linked text has content
        await Assert.That(content).IsEqualTo("taken");
        await Assert.That(owned.IsEmpty).IsTrue();

        // Dispose is safe -- it's a no-op for the data buffer
        owned.Dispose();
    }

    [Test]
    public async Task Create_TakeOwnership_Utf16OwnedText_TranscodesAndReturnsBuffer()
    {
        // Arrange -- create UTF-16 backed OwnedText
        var utf16Owned = OwnedText.FromChars("transcoded".AsSpan())!;

        // Act -- TakeOwnership with encoding mismatch: transcodes into format buffer
        using var linked = OwnedLinkedTextUtf8.Create(OwnedTextHandling.TakeOwnership, $"{utf16Owned}");
        var content = linked.AsSpan().ToString();

        // Assert -- content is transcoded correctly, OwnedText is emptied
        await Assert.That(content).IsEqualTo("transcoded");
        await Assert.That(utf16Owned.IsEmpty).IsTrue();

        utf16Owned.Dispose();
    }

    [Test]
    public async Task Create_Borrow_ReferencesBuffer()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("borrowed"u8)!;

        // Act -- Borrow mode: references the buffer, no copy
        using var linked = OwnedLinkedTextUtf8.Create(OwnedTextHandling.Borrow, $"{owned.Text}");
        var content = linked.AsSpan().ToString();

        // Assert -- OwnedText still usable, no detach
        await Assert.That(content).IsEqualTo("borrowed");
        await Assert.That(owned.IsEmpty).IsFalse();

        // Caller must keep OwnedText alive until linked text is disposed
        owned.Dispose();
    }

    [Test]
    public async Task Create_DefaultMode_IsCopy()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("default"u8)!;

        // Act -- default mode (no explicit handling) uses Copy
        using var linked = OwnedLinkedTextUtf8.Create($"{owned}");
        var content = linked.AsSpan().ToString();

        // Assert -- OwnedText is still usable (Copy is safe default)
        await Assert.That(content).IsEqualTo("default");
        await Assert.That(owned.IsEmpty).IsFalse();

        owned.Dispose();
    }
}
