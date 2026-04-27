namespace Glot.Tests;

public partial class LinkedTextUtf8Tests
{
    static LinkedTextUtf8 Create(params string[] segments)
    {
        var memories = new ReadOnlyMemory<byte>[segments.Length];
        for (var i = 0; i < segments.Length; i++)
        {
            memories[i] = System.Text.Encoding.UTF8.GetBytes(segments[i]);
        }
        return LinkedTextUtf8.Create(memories);
    }

    [Test]
    public async Task Equals_SameContent_SingleVsMultiSegment_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello, World";
        var single = Create(content);
        var multi = Create("Hello", ", ", "World");

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
        var a = Create("Hello");
        var b = Create("World");

        // Act & Assert
        await Assert.That(a.Equals(b)).IsFalse();
        await Assert.That(a != b).IsTrue();
    }

    [Test]
    public async Task Equals_DifferentLengths_ShortCircuitsFalse()
    {
        // Arrange
        var a = Create("Hi");
        var b = Create("Hello");

        // Act & Assert
        await Assert.That(a.Equals(b)).IsFalse();
    }

    [Test]
    public async Task Equals_NullOther_ReturnsFalse()
    {
        // Arrange
        var a = Create("Hello");

        // Act & Assert
        await Assert.That(a.Equals((LinkedTextUtf8?)null)).IsFalse();
    }

    [Test]
    public async Task Equals_AgainstUtf8Text_FastPath_ReturnsTrue()
    {
        // Arrange
        var linked = Create("Hello", " ", "World");
        var text = Text.FromUtf8("Hello World"u8);

        // Act & Assert
        await Assert.That(linked.Equals(text)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstUtf16Text_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        var linked = Create("café", "🎉");
        var text = Text.From("café🎉");

        // Act & Assert
        await Assert.That(linked.Equals(text)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstString_CrossEncoding_ReturnsTrue()
    {
        // Arrange
        const string content = "Hello, World";
        var linked = Create("Hello, ", "World");

        // Act & Assert
        await Assert.That(linked.Equals(content)).IsTrue();
    }

    [Test]
    public async Task Equals_AgainstUtf8Span_FastPath_ReturnsTrue()
    {
        // Arrange
        var linked = Create("Hello", " World");
        ReadOnlySpan<byte> bytes = "Hello World"u8;

        // Act
        var result = linked.Equals(bytes, TextEncoding.Utf8);

        // Assert
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task GetHashCode_MatchesUtf8Text()
    {
        // Arrange
        var linked = Create("Hello", " World");
        var text = Text.FromUtf8("Hello World"u8);

        // Act
        var linkedHash = linked.GetHashCode();
        var textHash = text.GetHashCode();

        // Assert — streaming XxHash3 over segments must match contiguous hash
        await Assert.That(linkedHash).IsEqualTo(textHash);
    }

    [Test]
    public async Task GetHashCode_Empty_ReturnsZero()
    {
        // Arrange
        var empty = LinkedTextUtf8.Empty;

        // Act
        var hash = empty.GetHashCode();

        // Assert
        await Assert.That(hash).IsEqualTo(0);
    }

    [Test]
    public async Task CompareTo_SameType_OrdersByByteContent()
    {
        // Arrange
        var a = Create("Apple");
        var b = Create("Apricot");

        // Act
        var result = a.CompareTo(b);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_PrefixIsLess()
    {
        // Arrange
        var prefix = Create("Hi");
        var longer = Create("Hi", "!");

        // Act
        var result = prefix.CompareTo(longer);

        // Assert
        await Assert.That(result).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_AgainstUtf8Text_OrdersByBytes()
    {
        // Arrange
        var linked = Create("Apple");
        var text = Text.FromUtf8("Apricot"u8);

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
        var linked = Create(content);

        // Act & Assert
        await Assert.That(linked.Equals((object)Text.FromUtf8("Hi"u8))).IsTrue();
        await Assert.That(linked.Equals((object)content)).IsTrue();
        await Assert.That(linked.Equals(new object())).IsFalse();
    }

    [Test]
    public async Task Operators_NullSafe()
    {
        // Arrange
        var a = Create("Hi");
        LinkedTextUtf8? nullA = null;
        LinkedTextUtf8? nullB = null;

        // Act & Assert
        await Assert.That(a == nullA).IsFalse();
        await Assert.That(nullA == nullB).IsTrue();
    }
}
