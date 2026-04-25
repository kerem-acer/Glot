namespace Glot.Tests;

public partial class OwnedTextTests
{
    // Factory — From(string)

    [Test]
    public Task FromString_Ascii_StoresUtf16()
    {
        // Arrange & Act
        using var owned = OwnedText.From("Hello");

        // Assert
        return Verify(new { owned.Encoding, owned.RuneLength, owned.ByteLength, owned.IsEmpty });
    }

    [Test]
    public Task FromString_MultiByte_CountsRunesCorrectly()
    {
        // Arrange & Act — c(1) a(1) f(1) é(1) 🎉(2 chars / 1 rune)
        using var owned = OwnedText.From("café🎉");

        // Assert — 5 runes, 6 chars × 2 bytes
        return Verify(new { owned.Encoding, owned.RuneLength, owned.ByteLength });
    }

    [Test]
    public async Task FromString_Empty_ReturnsSingletonEmpty()
    {
        // Act
        var owned = OwnedText.From(string.Empty);

        // Assert
        await Assert.That(owned).IsSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task FromString_Null_ReturnsSingletonEmpty()
    {
        // Act
        var owned = OwnedText.From(null!);

        // Assert
        await Assert.That(owned).IsSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task FromString_NonEmpty_ReturnsNewInstance()
    {
        // Act
        using var owned = OwnedText.From("Hello");

        // Assert
        await Assert.That(owned).IsNotSameReferenceAs(OwnedText.Empty);
    }

    [Test]
    public async Task FromString_Encoding_IsUtf16()
    {
        // Act
        using var owned = OwnedText.From("Hello");

        // Assert
        await Assert.That(owned.Encoding).IsEqualTo(TextEncoding.Utf16);
    }

    [Test]
    public async Task FromString_ByteLength_IsTwoPerChar()
    {
        // Arrange
        const string source = "Hello";
        const int expectedByteLength = 10;

        // Act
        using var owned = OwnedText.From(source);

        // Assert — UTF-16 stores 2 bytes per BMP char
        await Assert.That(owned.ByteLength).IsEqualTo(expectedByteLength);
    }

    [Test]
    public async Task FromString_RuneLength_MatchesScalarCount()
    {
        // Arrange — 5 scalar values: c, a, f, é, 🎉
        const int expectedRunes = 5;

        // Act
        using var owned = OwnedText.From("café🎉");

        // Assert
        await Assert.That(owned.RuneLength).IsEqualTo(expectedRunes);
    }

    [Test]
    public async Task FromString_CountRunesFalse_DefersAndComputesLazily()
    {
        // Arrange
        const int expectedRunes = 5;

        // Act — countRunes: false skips counting at construction; access lazily computes it
        using var owned = OwnedText.From("café🎉", countRunes: false);
        var lazy = owned.RuneLength;

        // Assert
        await Assert.That(lazy).IsEqualTo(expectedRunes);
    }

    [Test]
    public async Task FromString_TextView_RoundTripsContent()
    {
        // Arrange
        const string source = "Hello, world!";

        // Act
        using var owned = OwnedText.From(source);
        var roundTripped = owned.Text.ToString();

        // Assert
        await Assert.That(roundTripped).IsEqualTo(source);
    }

    [Test]
    public async Task FromString_TextView_EqualsTextFromSameString()
    {
        // Arrange
        const string source = "Hello";
        var direct = Text.From(source);

        // Act
        using var owned = OwnedText.From(source);
        var eq = owned.Text.Equals(direct);

        // Assert — same content via Text.From and OwnedText.From should compare equal
        await Assert.That(eq).IsTrue();
    }

    [Test]
    public async Task FromString_DoesNotCopy_ExposesStringBackedMemory()
    {
        // Arrange
        const string source = "Hello";

        // Act — no-copy: Text wraps the string directly with BackingType.String
        using var owned = OwnedText.From(source);
        var got = owned.Text.TryGetUtf16Memory(out var memory);

        // Assert — TryGetUtf16Memory only succeeds for string- or char-array-backed Text
        await Assert.That(got).IsTrue();
        await Assert.That(memory.ToString()).IsEqualTo(source);
    }

    [Test]
    public async Task FromString_DoesNotCopy_LengthsMatchTextFromString()
    {
        // Arrange
        const string source = "Hello, world!";

        // Act — no-copy paths share backing semantics with Text.From(string)
        using var owned = OwnedText.From(source);
        var direct = Text.From(source);

        // Assert
        await Assert.That(owned.Text.ByteLength).IsEqualTo(direct.ByteLength);
        await Assert.That(owned.Text.RuneLength).IsEqualTo(direct.RuneLength);
        await Assert.That(owned.Text.Encoding).IsEqualTo(direct.Encoding);
    }

    [Test]
    public async Task FromString_Surrogates_PreservedExactly()
    {
        // Arrange — 𝄞 (U+1D11E musical G clef) is encoded as a surrogate pair in UTF-16
        const string source = "𝄞";
        const int expectedRunes = 1;
        const int expectedByteLength = 4; // two UTF-16 code units

        // Act
        using var owned = OwnedText.From(source);

        // Assert
        await Assert.That(owned.RuneLength).IsEqualTo(expectedRunes);
        await Assert.That(owned.ByteLength).IsEqualTo(expectedByteLength);
        await Assert.That(owned.Text.ToString()).IsEqualTo(source);
    }

    [Test]
    public async Task FromString_DisposeMarksDisposed()
    {
        // Arrange
        var owned = OwnedText.From("Hello");

        // Act
        owned.Dispose();

        // Assert — no-copy view: dispose marks the wrapper but doesn't touch the source string
        await Assert.That(owned.IsDisposed).IsTrue();
    }

    [Test]
    public async Task FromString_PoolStress_ManyAllocations_NoCrash()
    {
        // Act
        var lastRuneLength = 0;
        for (var i = 0; i < 10_000; i++)
        {
            using var owned = OwnedText.From("Hello world café 🎉");
            lastRuneLength = owned.RuneLength;
        }

        // Assert
        await Assert.That(lastRuneLength).IsGreaterThan(0);
    }
}
