namespace Glot.Tests;

public partial class OwnedLinkedTextUtf16Tests
{
    [Test]
    public async Task Create_Copy_CopiesOwnedTextData()
    {
        // Arrange
        var owned = OwnedText.FromChars("copied".AsSpan())!;

        // Act -- Copy mode: data is copied into the linked text
        using var linked = OwnedLinkedTextUtf16.Create(OwnedTextHandling.Copy, $"{owned}");
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
        var owned = OwnedText.FromChars("taken".AsSpan())!;

        // Act -- TakeOwnership mode: buffer is detached from OwnedText
        using var linked = OwnedLinkedTextUtf16.Create(OwnedTextHandling.TakeOwnership, $"{owned}");
        var content = linked.AsSpan().ToString();

        // Assert -- OwnedText is empty after take, linked text has content
        await Assert.That(content).IsEqualTo("taken");
        await Assert.That(owned.IsEmpty).IsTrue();

        // Dispose is safe -- it's a no-op for the data buffer
        owned.Dispose();
    }

    [Test]
    public async Task Create_TakeOwnership_Utf8OwnedText_TranscodesAndReturnsBuffer()
    {
        // Arrange -- create UTF-8 backed OwnedText
        var utf8Owned = OwnedText.FromUtf8("transcoded"u8)!;

        // Act -- TakeOwnership with encoding mismatch: transcodes into format buffer, returns detached buffer
        using var linked = OwnedLinkedTextUtf16.Create(OwnedTextHandling.TakeOwnership, $"{utf8Owned}");
        var content = linked.AsSpan().ToString();

        // Assert -- content is transcoded correctly, OwnedText is emptied
        await Assert.That(content).IsEqualTo("transcoded");
        await Assert.That(utf8Owned.IsEmpty).IsTrue();

        utf8Owned.Dispose();
    }

    [Test]
    public async Task Create_Borrow_ReferencesBuffer()
    {
        // Arrange
        var owned = OwnedText.FromChars("borrowed".AsSpan())!;

        // Act -- Borrow mode: passes OwnedText directly, calls AppendFormatted(Text) via Borrow path
        using var linked = OwnedLinkedTextUtf16.Create(OwnedTextHandling.Borrow, $"{owned}");
        var content = linked.AsSpan().ToString();

        // Assert -- OwnedText still usable, no detach
        await Assert.That(content).IsEqualTo("borrowed");
        await Assert.That(owned.IsEmpty).IsFalse();

        // Caller must keep OwnedText alive until linked text is disposed
        owned.Dispose();
    }
}
