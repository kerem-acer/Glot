using System.Text.Json;

namespace Glot.SystemTextJson.Tests;

public partial class TextJsonConverterTests
{
    readonly JsonSerializerOptions _nullNotAllowed = new()
    {
        Converters = { new TextJsonConverter(allowNull: false) },
    };

    readonly JsonSerializerOptions _numbersAndBooleansNotAllowed = new()
    {
        Converters = { new TextJsonConverter(allowNumbersAndBooleans: false) },
    };

    [Test]
    [Arguments("1.2")]
    [Arguments("true")]
    [Arguments("false")]
    public async Task Deserialize_NumberOrBoolean_ReadsRawJson(string json)
    {
        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _options);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(json);
    }

    [Test]
    [Arguments("{}", "StartObject")]
    [Arguments("[]", "StartArray")]
    public async Task Deserialize_ObjectOrArray_ThrowsNamingToken(string json, string token)
    {
        // Arrange
        var expected = $"The JSON value could not be converted to Glot.Text. Expected a string, but the token is {token}.";

        // Act & Assert
        await Assert.That(() => JsonSerializer.Deserialize<Text>(json, _options))
            .Throws<JsonException>()
            .WithMessage(expected);
    }

    [Test]
    public async Task Deserialize_NullWhenNullNotAllowed_ThrowsNamingToken()
    {
        // Arrange
        const string json = "null";
        const string expected = "The JSON value could not be converted to Glot.Text. Expected a string, but the token is Null.";

        // Act & Assert
        await Assert.That(() => JsonSerializer.Deserialize<Text>(json, _nullNotAllowed))
            .Throws<JsonException>()
            .WithMessage(expected);
    }

    [Test]
    public async Task Deserialize_NullableNullWhenNullNotAllowed_ReturnsNull()
    {
        // Arrange — the serializer handles null for Text? before the converter runs
        const string json = "null";

        // Act
        var text = JsonSerializer.Deserialize<Text?>(json, _nullNotAllowed);

        // Assert
        await Assert.That(text).IsNull();
    }

    [Test]
    [Arguments("1.2")]
    [Arguments("true")]
    public async Task Deserialize_NumberOrBooleanWhenNullNotAllowed_ReadsRawJson(string json)
    {
        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _nullNotAllowed);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo(json);
    }

    [Test]
    [Arguments("1.2", "Number")]
    [Arguments("true", "True")]
    [Arguments("false", "False")]
    public async Task Deserialize_NumberOrBooleanWhenNotAllowed_ThrowsNamingToken(string json, string token)
    {
        // Arrange
        var expected = $"The JSON value could not be converted to Glot.Text. Expected a string, but the token is {token}.";

        // Act & Assert
        await Assert.That(() => JsonSerializer.Deserialize<Text>(json, _numbersAndBooleansNotAllowed))
            .Throws<JsonException>()
            .WithMessage(expected);
    }

    [Test]
    public async Task Deserialize_NullWhenNumbersAndBooleansNotAllowed_ReturnsEmpty()
    {
        // Arrange
        const string json = "null";

        // Act
        var text = JsonSerializer.Deserialize<Text>(json, _numbersAndBooleansNotAllowed);

        // Assert
        await Assert.That(text.IsEmpty).IsTrue();
    }
}
