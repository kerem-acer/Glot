using System.Buffers;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization;

namespace Glot.VYaml;

/// <summary>
/// Formats <see cref="OwnedText"/> as a YAML scalar string value.
/// Zero-string on both read and write.
/// The caller is responsible for disposing deserialized <see cref="OwnedText"/> values.
/// </summary>
public sealed class OwnedTextYamlFormatter : IYamlFormatter<OwnedText>
{

    /// <summary>Singleton instance.</summary>
    public static readonly OwnedTextYamlFormatter Instance = new();

    /// <inheritdoc/>
    public void Serialize(ref Utf8YamlEmitter emitter, OwnedText value, YamlSerializationContext context)
    {
        if (value is null)
        {
            emitter.WriteNull();
            return;
        }

        var text = value.Text;
        if (text.IsEmpty)
        {
            emitter.WriteString([]);
            return;
        }

        switch (text.Encoding)
        {
            case TextEncoding.Utf8:
                emitter.WriteScalar(text.Bytes);
                break;

            case TextEncoding.Utf16:
                emitter.WriteString(text.Chars);
                break;

            default:
                SerializeTranscoded(ref emitter, text);
                break;
        }
    }

    static void SerializeTranscoded(ref Utf8YamlEmitter emitter, Text text)
    {
        var maxBytes = text.RuneLength * 4;
        var rented = ArrayPool<byte>.Shared.Rent(Math.Max(maxBytes, 1));
        try
        {
            var written = text.EncodeToUtf8(rented);
            emitter.WriteScalar(rented.AsSpan(0, written));
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

    /// <inheritdoc/>
    public OwnedText Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
        if (parser.IsNullScalar())
        {
            parser.Read();
            return null!;
        }

        parser.TryGetScalarAsSpan(out var span);
        var owned = OwnedText.FromBytes(span, TextEncoding.Utf8);
        parser.ReadWithVerify(ParseEventType.Scalar);
        return owned!;
    }
}
