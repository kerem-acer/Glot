using System.Buffers;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization;

namespace Glot.VYaml;

/// <summary>
/// Formats <see cref="Text"/> as a YAML scalar string value.
/// Zero-string on both read and write.
/// </summary>
public sealed class TextYamlFormatter : IYamlFormatter<Text>
{

    /// <summary>Singleton instance.</summary>
    public static readonly TextYamlFormatter Instance = new();

    /// <inheritdoc/>
    public void Serialize(ref Utf8YamlEmitter emitter, Text value, YamlSerializationContext context)
    {
        if (value.IsEmpty)
        {
            emitter.WriteString([]);
            return;
        }

        switch (value.Encoding)
        {
            case TextEncoding.Utf8:
                emitter.WriteScalar(value.Bytes);
                break;
            case TextEncoding.Utf16:
                emitter.WriteString(value.Chars);
                break;
            default:
                SerializeTranscoded(ref emitter, value);
                break;
        }
    }

    static void SerializeTranscoded(ref Utf8YamlEmitter emitter, Text value)
    {
        var maxBytes = value.RuneLength * 4;
        var rented = ArrayPool<byte>.Shared.Rent(Math.Max(maxBytes, 1));
        try
        {
            var written = value.EncodeToUtf8(rented);
            emitter.WriteScalar(rented.AsSpan(0, written));
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

    /// <inheritdoc/>
    public Text Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
        if (parser.IsNullScalar())
        {
            parser.Read();
            return Text.Empty;
        }

        parser.TryGetScalarAsSpan(out var span);
        var text = Text.FromUtf8(span);
        parser.ReadWithVerify(ParseEventType.Scalar);
        return text;
    }
}
