namespace Glot.Tests;

public partial class TextTests
{
    // Replace

    [Test]
    public async Task Replace_SingleMatch_Replaces()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        var result = text.Replace("World", "Glot");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello Glot");
    }

    [Test]
    public async Task Replace_MultipleMatches_ReplacesAll()
    {
        // Arrange
        var text = Text.FromUtf8("aXbXcX"u8);

        // Act
        var result = text.Replace(Text.FromUtf8("X"u8), Text.FromUtf8("Y"u8));

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("aYbYcY");
    }

    [Test]
    public async Task Replace_NoMatch_ReturnsSame()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        var result = text.Replace("xyz", "abc");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task Replace_CrossEncoding_Works()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);

        // Act
        var result = text.Replace(Text.From("World"), Text.From("Glot"));

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello Glot");
    }

    [Test]
    public async Task Replace_MultiByte_Works()
    {
        // Arrange
        var text = Text.FromUtf8("café latte"u8);

        // Act
        var result = text.Replace(Text.FromUtf8("café"u8), Text.FromUtf8("tea"u8));

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("tea latte");
    }

    // ReplacePooled

    [Test]
    public async Task ReplacePooled_Match_ReturnsOwnedText()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        using var result = text.ReplacePooled("World", "Glot")!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("Hello Glot");
    }

    [Test]
    public async Task ReplacePooled_NoMatch_ReturnsCopy()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        using var result = text.ReplacePooled("xyz", "abc")!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("Hello");
    }

    // Insert

    [Test]
    public async Task Insert_AtStart_Prepends()
    {
        // Arrange
        var text = Text.From("World");

        // Act
        var result = text.Insert(0, "Hello ");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Insert_AtEnd_Appends()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        var result = text.Insert(5, " World");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Insert_AtMiddle_Inserts()
    {
        // Arrange
        var text = Text.From("HWorld");

        // Act
        var result = text.Insert(1, "ello ");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Insert_EmptyValue_ReturnsSame()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        var result = text.Insert(2, Text.Empty);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    // InsertPooled

    [Test]
    public async Task InsertPooled_ReturnsOwnedText()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        using var result = text.InsertPooled(5, " World")!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task InsertPooled_EmptyValue_ReturnsCopy()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        using var result = text.InsertPooled(2, Text.Empty)!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("Hello");
    }

    // Remove

    [Test]
    public async Task Remove_FromStart_UsesSlice()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        var result = text.Remove(0, 6);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("World");
    }

    [Test]
    public async Task Remove_FromEnd_UsesSlice()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        var result = text.Remove(5, 6);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task Remove_FromMiddle_UsesBuilder()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        var result = text.Remove(5, 1); // remove the space

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("HelloWorld");
    }

    [Test]
    public async Task Remove_ZeroCount_ReturnsSame()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        var result = text.Remove(2, 0);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task Remove_EntireContent_ReturnsEmpty()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        var result = text.Remove(0, 5);

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    // RemovePooled

    [Test]
    public async Task RemovePooled_FromMiddle_ReturnsOwnedText()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        using var result = text.RemovePooled(5, 1)!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("HelloWorld");
    }

    [Test]
    public async Task RemovePooled_ZeroCount_ReturnsCopy()
    {
        // Arrange
        var text = Text.From("Hello");

        // Act
        using var result = text.RemovePooled(2, 0)!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("Hello");
    }

    // ToUpperInvariant

    [Test]
    public async Task ToUpperInvariant_Ascii_Uppercases()
    {
        // Arrange
        var text = Text.From("hello");

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("HELLO");
    }

    [Test]
    public async Task ToUpperInvariant_MultiByte_Uppercases()
    {
        // Arrange
        var text = Text.FromUtf8("café"u8);

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("CAFÉ");
    }

    [Test]
    public async Task ToUpperInvariant_AlreadyUpper_ReturnsSame()
    {
        // Arrange
        var text = Text.From("HELLO");

        // Act
        var result = text.ToUpperInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("HELLO");
    }

    [Test]
    public async Task ToUpperInvariant_Empty_ReturnsSame()
    {
        // Act
        var result = Text.Empty.ToUpperInvariant();

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    // ToLowerInvariant

    [Test]
    public async Task ToLowerInvariant_Ascii_Lowercases()
    {
        // Arrange
        var text = Text.From("HELLO");

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("hello");
    }

    [Test]
    public async Task ToLowerInvariant_MultiByte_Lowercases()
    {
        // Arrange
        var text = Text.FromUtf8("CAFÉ"u8);

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("café");
    }

    [Test]
    public async Task ToLowerInvariant_AlreadyLower_ReturnsSame()
    {
        // Arrange
        var text = Text.From("hello");

        // Act
        var result = text.ToLowerInvariant();

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("hello");
    }

    // Pooled case variants

    [Test]
    public async Task ToUpperInvariantPooled_ReturnsOwnedText()
    {
        // Arrange
        var text = Text.From("hello");

        // Act
        using var result = text.ToUpperInvariantPooled()!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("HELLO");
    }

    [Test]
    public async Task ToLowerInvariantPooled_ReturnsOwnedText()
    {
        // Arrange
        var text = Text.From("HELLO");

        // Act
        using var result = text.ToLowerInvariantPooled()!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("hello");
    }

    [Test]
    public async Task ToUpperInvariantPooled_NoChange_ReturnsCopy()
    {
        // Arrange
        var text = Text.From("HELLO");

        // Act
        using var result = text.ToUpperInvariantPooled()!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("HELLO");
    }

    // Round-trip

    [Test]
    public Task RoundTrip_UpperLower_Ascii()
    {
        // Arrange
        var text = Text.From("Hello World");

        // Act
        var upper = text.ToUpperInvariant();
        var lower = upper.ToLowerInvariant();

        // Assert
        return Verify(new { upper = upper.ToString(), lower = lower.ToString() });
    }

    // Concat

    [Test]
    public async Task Concat_TwoTexts_Concatenates()
    {
        // Act
        var result = Text.Concat(Text.From("Hello"), Text.From(" World"));

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task Concat_FirstEmpty_ReturnsSecond()
    {
        // Arrange
        Text first = Text.Empty;
        Text second = Text.From("Hello");

        // Act
        var result = Text.Concat(first, second);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task Concat_Single_ReturnsSame()
    {
        // Arrange
        Text single = Text.From("Hello");

        // Act
        var result = Text.Concat(single);

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task Concat_ThreeTexts_Concatenates()
    {
        // Act
        var result = Text.Concat(Text.From("A"), Text.From("B"), Text.From("C"));

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("ABC");
    }

    [Test]
    public async Task Concat_ManyTexts_Concatenates()
    {
        // Act
        var result = Text.Concat(
            Text.From("A"), Text.FromUtf8("B"u8), Text.From("C"), Text.From("D"));

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("ABCD");
    }

    [Test]
    public async Task Concat_Empty_ReturnsEmpty()
    {
        // Act
        var result = Text.Concat();

        // Assert
        await Assert.That(result.IsEmpty).IsTrue();
    }

    [Test]
    public Task Concat_CrossEncoding_UsesFirstEncoding()
    {
        // Act
        var result = Text.Concat(Text.FromUtf8("Hello"u8), Text.From(" World"));

        // Assert
        return Verify(new { text = result.ToString(), result.Encoding });
    }

    [Test]
    public async Task ConcatPooled_ReturnsOwnedText()
    {
        // Act
        using var result = Text.ConcatPooled(Text.From("Hello"), Text.From(" World"))!;

        // Assert
        await Assert.That(result.Text.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task ConcatPooled_Empty_ReturnsNull()
    {
        // Act
        var result = Text.ConcatPooled();

        // Assert
        await Assert.That(result).IsNull();
    }

    // Operator +

    [Test]
    public async Task OperatorPlus_Concatenates()
    {
        // Act
        var result = Text.From("Hello") + Text.From(" World");

        // Assert
        await Assert.That(result.ToString()).IsEqualTo("Hello World");
    }

    // Utf8-backed mutations

    [Test]
    public Task Replace_Utf8Backed_PreservesEncoding()
    {
        // Arrange
        var text = Text.FromUtf8("Hello World"u8);

        // Act
        var result = text.Replace(Text.FromUtf8("World"u8), Text.FromUtf8("Glot"u8));

        // Assert
        return Verify(new { text = result.ToString(), result.Encoding });
    }

    [Test]
    public Task Insert_Utf8Backed_PreservesEncoding()
    {
        // Arrange
        var text = Text.FromUtf8("HelloWorld"u8);

        // Act
        var result = text.Insert(5, Text.FromUtf8(" "u8));

        // Assert
        return Verify(new { text = result.ToString(), result.Encoding });
    }
}
