namespace Glot.Tests;

public partial class OwnedTextTests
{
    // Factory — From(Text)

    [Test]
    public Task FromText_Utf8_ByteArrayBacked_PreservesEverything()
    {
        // Arrange
        var source = Text.FromUtf8("Hello"u8);

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        return Verify(new { owned.Encoding, owned.RuneLength, owned.ByteLength, owned.IsEmpty });
    }

    [Test]
    public Task FromText_Utf16_StringBacked_PreservesEverything()
    {
        // Arrange
        var source = Text.From("Hello");

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        return Verify(new { owned.Encoding, owned.RuneLength, owned.ByteLength });
    }

    [Test]
    public Task FromText_Utf16_CharArrayBacked_PreservesEverything()
    {
        // Arrange
        var source = Text.FromChars("Hello".ToCharArray());

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        return Verify(new { owned.Encoding, owned.RuneLength, owned.ByteLength });
    }

    [Test]
    public Task FromText_Utf32_IntArrayBacked_PreservesEverything()
    {
        // Arrange — H, e, l
        ReadOnlySpan<int> codePoints = [0x48, 0x65, 0x6C];
        var source = Text.FromUtf32(codePoints);

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        return Verify(new { owned.Encoding, owned.RuneLength, owned.ByteLength });
    }

    [Test]
    public async Task FromText_EmptyText_ReturnsSingletonEmpty()
    {
        // Act
        var owned = OwnedText.From(Text.Empty);

        // Assert
        await Assert.That(owned).IsSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task FromText_DefaultText_ReturnsSingletonEmpty()
    {
        // Act — explicit type to disambiguate From(Text) from From(string)
        var owned = OwnedText.From(default(Text));

        // Assert
        await Assert.That(owned).IsSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task FromText_NonEmpty_ReturnsNewInstance()
    {
        // Act
        using var owned = OwnedText.From(Text.FromUtf8("Hello"u8));

        // Assert
        await Assert.That(owned).IsNotSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task FromText_PreservesUtf8Encoding()
    {
        // Arrange
        var source = Text.FromUtf8("Hello"u8);

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    [Test]
    public async Task FromText_PreservesUtf16Encoding()
    {
        // Arrange
        var source = Text.From("Hello");

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task FromText_PreservesUtf32Encoding()
    {
        // Arrange
        ReadOnlySpan<int> codePoints = [0x48, 0x69];
        var source = Text.FromUtf32(codePoints);

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf32);
    }

    [Test]
    public async Task FromText_PreservesByteLength()
    {
        // Arrange
        var source = Text.FromUtf8("café"u8);
        var expectedByteLength = source.ByteLength;

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        await Assert.That(owned.ByteLength).IsEqualTo(expectedByteLength);
    }

    [Test]
    public async Task FromText_PreservesRuneLength()
    {
        // Arrange — café🎉 = 5 scalar values
        var source = Text.From("café🎉");
        var expectedRunes = source.RuneLength;

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        await Assert.That(owned.RuneLength).IsEqualTo(expectedRunes);
    }

    [Test]
    public async Task FromText_RoundTripsContent_Utf8()
    {
        // Arrange
        const string expected = "Hello, world!";
        var source = Text.FromUtf8("Hello, world!"u8);

        // Act
        using var owned = OwnedText.From(source);
        var roundTripped = owned.Text.ToString();

        // Assert
        await Assert.That(roundTripped).IsEqualTo(expected);
    }

    [Test]
    public async Task FromText_RoundTripsContent_Utf16FromString()
    {
        // Arrange
        const string expected = "café🎉";
        var source = Text.From(expected);

        // Act
        using var owned = OwnedText.From(source);
        var roundTripped = owned.Text.ToString();

        // Assert
        await Assert.That(roundTripped).IsEqualTo(expected);
    }

    [Test]
    public async Task FromText_RoundTripsContent_Utf32()
    {
        // Arrange — H, i, !
        ReadOnlySpan<int> codePoints = [0x48, 0x69, 0x21];
        var source = Text.FromUtf32(codePoints);

        // Act
        using var owned = OwnedText.From(source);
        var roundTripped = owned.Text.ToString();

        // Assert
        await Assert.That(roundTripped).IsEqualTo("Hi!");
    }

    [Test]
    public async Task FromText_CrossEncodingEquality_WithSource()
    {
        // Arrange
        var utf8Source = Text.FromUtf8("Hello"u8);
        var utf16Source = Text.From("Hello");

        // Act
        using var ownedFromUtf8 = OwnedText.From(utf8Source);
        using var ownedFromUtf16 = OwnedText.From(utf16Source);
        var eq = ownedFromUtf8.Text.Equals(ownedFromUtf16.Text);

        // Assert
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task FromText_DoesNotCopy_AliasesSourceArray()
    {
        // Arrange — start with a mutable byte[]-backed Text
        byte[] sourceBytes = [0x48, 0x65, 0x6C, 0x6C, 0x6F]; // Hello
        var source = Text.FromUtf8(sourceBytes);

        // Act — wrap as a view, then mutate the source array
        using var owned = OwnedText.From(source);
        sourceBytes[0] = 0x4A; // 'J'
        var aliased = owned.Text.ToString();

        // Assert — no-copy semantics: owned shares the source array and sees the mutation
        await Assert.That(aliased).IsEqualTo("Jello");
    }

    [Test]
    public async Task FromText_Surrogates_PreservedExactly()
    {
        // Arrange — 𝄞 (U+1D11E) is a non-BMP scalar (4 UTF-8 bytes)
        const string expected = "𝄞";
        var source = Text.FromUtf8("𝄞"u8);

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        await Assert.That(owned.RuneLength).IsEqualTo(1);
        await Assert.That(owned.ByteLength).IsEqualTo(4);
        await Assert.That(owned.Text.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task FromText_DisposeMarksDisposed()
    {
        // Arrange
        var owned = OwnedText.From(Text.From("Hello"));

        // Act
        owned.Dispose();

        // Assert
        await Assert.That(owned.IsDisposed).IsTrue();
    }

    [Test]
    public async Task FromText_LazyRuneCountOnSource_StillPreservesRunes()
    {
        // Arrange — Text constructed without rune counting; access lazily computes it
        var source = Text.From("café🎉", countRunes: false);

        // Act — From(Text) reads RuneLength which forces lazy computation
        using var owned = OwnedText.From(source);

        // Assert
        await Assert.That(owned.RuneLength).IsEqualTo(5);
    }

    [Test]
    public async Task FromText_PoolStress_ManyAllocations_NoCrash()
    {
        // Arrange
        var source = Text.FromUtf8("Hello world café 🎉"u8);

        // Act
        var lastRuneLength = 0;
        for (var i = 0; i < 10_000; i++)
        {
            using var owned = OwnedText.From(source);
            lastRuneLength = owned.RuneLength;
        }

        // Assert
        await Assert.That(lastRuneLength).IsGreaterThan(0);
    }
}
