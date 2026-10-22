using System.Text;
using System.Text.Json;

namespace Glot.SystemTextJson.Tests;

public class Utf8JsonReaderGlotExtensionsTests
{
    [Test]
    public async Task GetText_String_ReturnsValue()
    {
        // Arrange
        const string value = "hello";

        // Act
        var text = ReadFirstToken($"\"{value}\"");

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(value);
    }

    [Test]
    public async Task GetText_String_KeepsUtf8()
    {
        // Act
        var text = ReadFirstToken("\"hello\"");

        // Assert
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    [Test]
    public async Task GetText_PropertyName_ReturnsName()
    {
        // Arrange
        const string name = "tr";
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes($$"""{"{{name}}":1}"""));
        reader.Read();
        reader.Read();

        // Act
        var text = reader.GetText();

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(name);
    }

    [Test]
    public async Task GetText_EscapedString_Unescapes()
    {
        // Arrange — the serializer escapes each quote
        const string value = "say \"hi\"";

        // Act
        var text = ReadFirstToken(JsonSerializer.Serialize(value));

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(value);
    }

    [Test]
    public async Task GetText_LongEscapedString_Unescapes()
    {
        // Arrange — escaped quotes make the token longer than the stack buffer, so unescaping rents one
        var value = new string('"', 300);

        // Act
        var text = ReadFirstToken(JsonSerializer.Serialize(value));

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(value);
    }

    [Test]
    [Arguments("null", "Null")]
    [Arguments("1.2", "Number")]
    [Arguments("true", "True")]
    [Arguments("false", "False")]
    [Arguments("{}", "StartObject")]
    [Arguments("[]", "StartArray")]
    public async Task GetText_NonStringToken_Throws(string json, string token)
    {
        // Arrange
        var expected = $"Cannot get the value of a token type '{token}' as Glot.Text.";

        // Act & Assert
        await Assert.That(() => ReadFirstToken(json))
            .Throws<InvalidOperationException>()
            .WithMessage(expected);
    }

    static Text ReadFirstToken(string json)
    {
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        return reader.GetText();
    }
}
