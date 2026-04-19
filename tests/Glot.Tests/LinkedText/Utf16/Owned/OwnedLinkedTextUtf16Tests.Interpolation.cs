namespace Glot.Tests;

public partial class OwnedLinkedTextUtf16Tests
{
    // Struct handler: OwnedLinkedTextUtf16.Create($"...") routes through LinkedTextUtf16InterpolatedStringHandler

    [Test]
    public async Task Interpolation_Int_FormatsViaStructHandler()
    {
        // Arrange
        const int value = 42;

        // Act
        using var owned = OwnedLinkedTextUtf16.Create($"count={value}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "count=42";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_String_FormatsViaStructHandler()
    {
        // Arrange
        var name = "world";

        // Act
        using var owned = OwnedLinkedTextUtf16.Create($"hello {name}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "hello world";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_NullString_SkippedViaStructHandler()
    {
        // Arrange
        string? nullStr = null;

        // Act
        using var owned = OwnedLinkedTextUtf16.Create($"a{nullStr}b");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "ab";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_Text_FormatsViaStructHandler()
    {
        // Arrange
        var text = Text.FromUtf8("utf8"u8);

        // Act
        using var owned = OwnedLinkedTextUtf16.Create($"val:{text}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "val:utf8";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_ReadOnlyMemoryChar_FormatsViaStructHandler()
    {
        // Arrange
        var mem = "mem".AsMemory();

        // Act
        using var owned = OwnedLinkedTextUtf16.Create($"data:{mem}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "data:mem";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_FormatSpecifier_FormatsViaStructHandler()
    {
        // Arrange
        const int value = 7;

        // Act
        using var owned = OwnedLinkedTextUtf16.Create($"id={value:D3}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "id=007";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_Multiple_FormatsViaStructHandler()
    {
        // Arrange
        const int a = 1;
        const int b = 2;

        // Act
        using var owned = OwnedLinkedTextUtf16.Create($"{a}+{b}={a + b}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "1+2=3";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_EmptyText_SkippedViaStructHandler()
    {
        // Act
        using var owned = OwnedLinkedTextUtf16.Create($"a{Text.Empty}b");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "ab";
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Interpolation_TextSpan_FormatsViaStructHandler()
    {
        // Arrange
        var text = Text.From("span");
        var span = text.AsSpan();

        // Act
        using var owned = OwnedLinkedTextUtf16.Create($"v:{span}");
        var result = owned.AsSpan().ToString();

        // Assert
        const string expected = "v:span";
        await Assert.That(result).IsEqualTo(expected);
    }
}
