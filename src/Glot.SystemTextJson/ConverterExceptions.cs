using System.Text.Json;

namespace Glot.SystemTextJson;

/// <summary>
/// Exceptions shared by the converters, so they report a bad token the same way.
/// </summary>
static class ConverterExceptions
{
    /// <summary>Creates the exception for a token that isn't a JSON string where <paramref name="type"/> was expected.</summary>
    public static JsonException NotAString(JsonTokenType tokenType, Type type)
        => new($"The JSON value could not be converted to {type}. Expected a string, but the token is {tokenType}.");
}
