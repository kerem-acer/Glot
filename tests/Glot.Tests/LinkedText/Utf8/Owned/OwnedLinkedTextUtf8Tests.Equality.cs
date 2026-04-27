using System.Text;

namespace Glot.Tests;

public partial class OwnedLinkedTextUtf8Tests
{
    [Test]
    public async Task Equals_SameContent_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello";
        using var a = OwnedLinkedTextUtf8.Create(Utf8(content));
        using var b = OwnedLinkedTextUtf8.Create(Utf8("Hel"), Utf8("lo"));

        // Act & Assert
        await Assert.That(a.Equals(b)).IsTrue();
        await Assert.That(a == b).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstLinkedTextUtf8_DelegatesToData()
    {
        // Arrange
        using var owned = OwnedLinkedTextUtf8.Create(Utf8("Hello"));
        var bare = LinkedTextUtf8.Create(Encoding.UTF8.GetBytes("Hello").AsMemory());

        // Act & Assert
        await Assert.That(owned.Equals(bare)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstText_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        const string content = "café";
        using var owned = OwnedLinkedTextUtf8.Create(Utf8(content));
        var text = Text.From(content);

        // Act & Assert
        await Assert.That(owned.Equals(text)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstString_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello";
        using var owned = OwnedLinkedTextUtf8.Create(Utf8(content));

        // Act & Assert
        await Assert.That(owned.Equals(content)).IsTrue();
    }

    [Test]
    public async Task CompareTo_SameType_OrdersByContent()
    {
        // Arrange
        using var a = OwnedLinkedTextUtf8.Create(Utf8("Apple"));
        using var b = OwnedLinkedTextUtf8.Create(Utf8("Apricot"));

        // Act
        var result = a.CompareTo(b);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task Operators_NullSafe()
    {
        // Arrange
        using var a = OwnedLinkedTextUtf8.Create(Utf8("Hi"));
        OwnedLinkedTextUtf8? nullA = null;
        OwnedLinkedTextUtf8? nullB = null;

        // Act & Assert
        await Assert.That(a == nullA).IsFalse();
        await Assert.That(nullA == nullB).IsTrue();
    }
}
