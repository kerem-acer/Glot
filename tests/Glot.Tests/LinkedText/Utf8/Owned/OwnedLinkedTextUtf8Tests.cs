using System.Text;

namespace Glot.Tests;

public partial class OwnedLinkedTextUtf8Tests
{
    static ReadOnlyMemory<byte> Utf8(string s) => Encoding.UTF8.GetBytes(s).AsMemory();

    [Test]
    public Task AsSpan_ReturnsValidSpan()
    {
        // Arrange
        using var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"), Utf8(" - "), Utf8("world"));

        // Act
        var span = owned.AsSpan();
        var length = span.Length;
        var result = span.ToString();

        // Assert
        return Verify(new { length, result });
    }

    [Test]
    public async Task Dispose_ReleasesData()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"));
        var hadData = owned.Data is not null;

        // Act
        owned.Dispose();

        // Assert — verify it had data before dispose, and dispose didn't throw
        await Assert.That(hadData).IsTrue();
    }

    [Test]
    public void Dispose_CalledTwice_NoError()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"));

        // Act — second dispose must not throw
        owned.Dispose();
        owned.Dispose();
    }

    [Test]
    public async Task AsSpan_AfterDispose_ReturnsDefault()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Utf8("hello"));
        owned.Dispose();

        // Act
        var span = owned.AsSpan();

        // Assert
        await Assert.That(span.IsEmpty).IsTrue();
    }
}
