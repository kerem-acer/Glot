using System.Buffers;
using System.Text.Json;

namespace Glot.SystemTextJson;

/// <summary>
/// Extends <see cref="Utf8JsonReader"/> with reading a <see cref="Text"/>. No <see cref="JsonSerializerOptions"/> are needed.
/// </summary>
public static class Utf8JsonReaderGlotExtensions
{
    const int StackAllocThreshold = 512;

    /// <summary>
    /// Reads the current JSON string or property name as an unescaped <see cref="Text"/>.
    /// </summary>
    /// <param name="reader">The reader, positioned on a <see cref="JsonTokenType.String"/> or <see cref="JsonTokenType.PropertyName"/> token.</param>
    /// <returns>The token's value, unescaped, as UTF-8 text.</returns>
    /// <exception cref="InvalidOperationException">The current token is not a string or a property name.</exception>
    /// <remarks>
    /// Like <see cref="Utf8JsonReader.GetString"/>, but decodes the UTF-8 bytes straight into a <see cref="Text"/> without creating a <see cref="string"/>.
    /// An escaped value is unescaped through a stack- or pool-allocated buffer.
    /// Unlike <see cref="Utf8JsonReader.GetString"/>, a <c>null</c> token throws, because a <see cref="Text"/> has no null value.
    /// </remarks>
    /// <example>
    /// <code>
    /// Text name = reader.GetText();
    /// </code>
    /// </example>
    public static Text GetText(this ref Utf8JsonReader reader)
    {
        if (reader.TokenType is not (JsonTokenType.String or JsonTokenType.PropertyName))
        {
            throw new InvalidOperationException($"Cannot get the value of a token type '{reader.TokenType}' as {typeof(Text)}.");
        }

        if (!reader.ValueIsEscaped)
        {
            return reader.HasValueSequence
                ? Text.FromUtf8(reader.ValueSequence)
                : Text.FromUtf8(reader.ValueSpan);
        }

        long length = reader.HasValueSequence
            ? reader.ValueSequence.Length
            : reader.ValueSpan.Length;
        byte[]? rented = null;
        var buffer = length <= StackAllocThreshold
            ? stackalloc byte[StackAllocThreshold]
            : (rented = ArrayPool<byte>.Shared.Rent((int)length));
        try
        {
            var written = reader.CopyString(buffer);
            return Text.FromUtf8(buffer.Slice(0, written));
        }
        finally
        {
            if (rented is not null)
            {
                ArrayPool<byte>.Shared.Return(rented);
            }
        }
    }
}
