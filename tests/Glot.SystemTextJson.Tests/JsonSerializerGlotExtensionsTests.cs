using System.Text.Json;

namespace Glot.SystemTextJson.Tests;

public class JsonSerializerGlotExtensionsTests
{
    [Test]
    public async Task SerializeToUtf8OwnedText_SimpleObject_ContainsValidJson()
    {
        // Arrange
        var value = new { Name = "test" };

        // Act
        using var owned = JsonSerializer.SerializeToUtf8OwnedText(value);

        // Assert
        var json = owned.Text.ToString();
        await Assert.That(json).IsEqualTo("""{"Name":"test"}""");
    }

    [Test]
    [NotInParallel]
    public async Task SerializeToUtf8OwnedText_AfterDispose_IsEmpty()
    {
        // Arrange
        var value = new { Name = "test" };
        var owned = JsonSerializer.SerializeToUtf8OwnedText(value);

        // Act
        owned.Dispose();

        // Assert — reads pool-returned instance's reset state; NotInParallel prevents a concurrent rent from repopulating it
        await Assert.That(owned.IsEmpty).IsTrue();
    }

    [Test]
    public async Task SerializeToUtf8Text_SimpleObject_ContainsValidJson()
    {
        // Arrange
        var value = new { Name = "test" };

        // Act
        var text = JsonSerializer.SerializeToUtf8Text(value);

        // Assert
        var json = text.ToString();
        await Assert.That(json).IsEqualTo("""{"Name":"test"}""");
    }

    [Test]
    public async Task SerializeToUtf8Text_Encoding_IsUtf8()
    {
        // Arrange
        var value = new { Id = 1 };

        // Act
        var text = JsonSerializer.SerializeToUtf8Text(value);

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    [Test]
    public async Task SerializeToUtf8OwnedText_WithIndentedOptions_ProducesIndentedJson()
    {
        // Arrange
        var value = new { A = 1 };
        var options = new JsonSerializerOptions { WriteIndented = true };

        // Act
        using var owned = JsonSerializer.SerializeToUtf8OwnedText(value, options);

        // Assert
        var json = owned.Text.ToString();
        await Assert.That(json).IsEqualTo("""
            {
              "A": 1
            }
            """.ReplaceLineEndings("\n"));
    }

    [Test]
    public async Task SerializeToUtf8OwnedText_NullValue_ProducesNullJson()
    {
        // Arrange
        string? value = null;

        // Act
        using var owned = JsonSerializer.SerializeToUtf8OwnedText(value);

        // Assert
        var json = owned.Text.ToString();
        await Assert.That(json).IsEqualTo("null");
    }

    [Test]
    public async Task SerializeToUtf8Text_RoundTrip_PreservesValue()
    {
        // Arrange
        const string original = "hello world";

        // Act
        var text = JsonSerializer.SerializeToUtf8Text(original);
        var deserialized = JsonSerializer.Deserialize<string>(text.ToString());

        // Assert
        await Assert.That(deserialized).IsEqualTo(original);
    }
}
