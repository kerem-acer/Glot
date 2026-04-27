namespace Glot.Tests;

public partial class LinkedTextUtf16Tests
{
    [Test]
    public async Task Equals_SameContent_SingleVsMultiSegment_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello, World";
        var single = LinkedTextUtf16.Create(content);
        var multi = LinkedTextUtf16.Create("Hello", ", ", "World");

        // Act
        var result = single.Equals(multi);

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(single == multi).IsTrue();
    }

    [Test]
    public async Task Equals_DifferentContent_ReturnsFalse()
    {
        // Arrange
        var a = LinkedTextUtf16.Create("Hello");
        var b = LinkedTextUtf16.Create("World");

        // Act & Assert
        await Assert.That(a.Equals(b)).IsFalse();
        await Assert.That(a != b).IsTrue();
    }

    [Test]
    public async Task Equals_NullOther_ReturnsFalse()
    {
        // Arrange
        var a = LinkedTextUtf16.Create("Hello");

        // Act & Assert
        await Assert.That(a.Equals((LinkedTextUtf16?)null)).IsFalse();
    }

    [Test]
    public async Task Equals_AgainstUtf16Text_FastPath_ReturnsTrue()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("Hello", " ", "World");
        var text = Text.From("Hello World");

        // Act & Assert
        await Assert.That(linked.Equals(text)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstUtf8Text_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("café", "🎉");
        var text = Text.FromUtf8("café🎉"u8);

        // Act & Assert
        await Assert.That(linked.Equals(text)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstString_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello, World";
        var linked = LinkedTextUtf16.Create("Hello, ", "World");

        // Act & Assert
        await Assert.That(linked.Equals(content)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstCharSpan_FastPath_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello World";
        var linked = LinkedTextUtf16.Create("Hello", " World");

        // Act
        var result = linked.Equals(content.AsSpan());

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task GetHashCode_MatchesUtf16Text()
    {
        // Arrange
        const string content = "Hello World";
        var linked = LinkedTextUtf16.Create("Hello", " World");
        var text = Text.From(content);

        // Act
        var linkedHash = linked.GetHashCode();
        var textHash = text.GetHashCode();

        // Assert — streaming XxHash3 over segment bytes must match contiguous hash
        await Assert.That(linkedHash).IsEqualTo(textHash);
    }

    [Test]
    public async Task GetHashCode_Empty_ReturnsZero()
    {
        // Arrange
        var empty = LinkedTextUtf16.Empty;

        // Act
        var hash = empty.GetHashCode();

        // Assert
        await Assert.That(hash).IsEqualTo(0);
    }

    [Test]
    public async Task CompareTo_SameType_OrdersByCharContent()
    {
        // Arrange
        var a = LinkedTextUtf16.Create("Apple");
        var b = LinkedTextUtf16.Create("Apricot");

        // Act
        var result = a.CompareTo(b);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_PrefixIsLess()
    {
        // Arrange
        var prefix = LinkedTextUtf16.Create("Hi");
        var longer = LinkedTextUtf16.Create("Hi", "!");

        // Act
        var result = prefix.CompareTo(longer);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_AgainstUtf16Text_OrdersByChars()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("Apple");
        var text = Text.From("Apricot");

        // Act
        var result = linked.CompareTo(text);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task Equals_Object_DispatchesByType()
    {
        // Arrange
        const string content = "Hi";
        var linked = LinkedTextUtf16.Create(content);

        // Act & Assert
        await Assert.That(linked.Equals((object)Text.From(content))).IsTrue();
        await Assert.That(linked.Equals((object)content)).IsTrue();
        await Assert.That(linked.Equals(new object())).IsFalse();
    }

    [Test]
    public async Task Operators_NullSafe()
    {
        // Arrange
        var a = LinkedTextUtf16.Create("Hi");
        LinkedTextUtf16? nullA = null;
        LinkedTextUtf16? nullB = null;

        // Act & Assert
        await Assert.That(a == nullA).IsFalse();
        await Assert.That(nullA == nullB).IsTrue();
    }
}
