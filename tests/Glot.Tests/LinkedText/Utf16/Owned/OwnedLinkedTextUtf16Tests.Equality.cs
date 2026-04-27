namespace Glot.Tests;

public partial class OwnedLinkedTextUtf16Tests
{
    [Test]
    public async Task Equals_SameContent_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello";
        using var a = OwnedLinkedTextUtf16.Create(content);
        using var b = OwnedLinkedTextUtf16.Create("Hel", "lo");

        // Act & Assert
        await Assert.That(a.Equals(b)).IsTrue();
        await Assert.That(a == b).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstLinkedTextUtf16_DelegatesToData()
    {
        // Arrange
        const string content = "Hello";
        using var owned = OwnedLinkedTextUtf16.Create(content);
        var bare = LinkedTextUtf16.Create(content);

        // Act & Assert
        await Assert.That(owned.Equals(bare)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstText_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        const string content = "café";
        using var owned = OwnedLinkedTextUtf16.Create(content);
        var text = Text.FromUtf8("café"u8);

        // Act & Assert
        await Assert.That(owned.Equals(text)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstString_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello";
        using var owned = OwnedLinkedTextUtf16.Create(content);

        // Act & Assert
        await Assert.That(owned.Equals(content)).IsTrue();
    }

    [Test]
    public async Task CompareTo_SameType_OrdersByContent()
    {
        // Arrange
        using var a = OwnedLinkedTextUtf16.Create("Apple");
        using var b = OwnedLinkedTextUtf16.Create("Apricot");

        // Act
        var result = a.CompareTo(b);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task Operators_NullSafe()
    {
        // Arrange
        using var a = OwnedLinkedTextUtf16.Create("Hi");
        OwnedLinkedTextUtf16? nullA = null;
        OwnedLinkedTextUtf16? nullB = null;

        // Act & Assert
        await Assert.That(a == nullA).IsFalse();
        await Assert.That(nullA == nullB).IsTrue();
    }
}
