using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Returns a zero-allocation enumerator over the runes in this span.</summary>
    public RuneEnumerator EnumerateRunes() => new(Bytes, Encoding);

    /// <summary>A zero-allocation enumerator that yields each <see cref="Rune"/> in a <see cref="TextSpan"/>.</summary>
    public ref struct RuneEnumerator
    {
        ReadOnlySpan<byte> _remaining;
        readonly TextEncoding _encoding;
        Rune _current;

        internal RuneEnumerator(ReadOnlySpan<byte> bytes, TextEncoding encoding)
        {
            _remaining = bytes;
            _encoding = encoding;
            _current = default;
        }

        /// <summary>Gets the rune at the current position.</summary>
        public readonly Rune Current => _current;

        /// <summary>Advances to the next rune. Returns <c>false</c> when no runes remain.</summary>
        public bool MoveNext()
        {
            var remaining = _remaining;
            if (remaining.IsEmpty)
            {
                return false;
            }

            int consumed;
            switch (_encoding)
            {
                case TextEncoding.Utf8:
                {
                    var status = Rune.DecodeFromUtf8(remaining, out _current, out consumed);
                    if (status != OperationStatus.Done)
                    {
                        _current = Rune.ReplacementChar;
                        if (consumed == 0)
                        {
                            consumed = 1;
                        }
                    }
                    break;
                }

                case TextEncoding.Utf16:
                {
                    var chars = MemoryMarshal.Cast<byte, char>(remaining);
                    var status = Rune.DecodeFromUtf16(chars, out _current, out var charsConsumed);
                    if (status != OperationStatus.Done)
                    {
                        _current = Rune.ReplacementChar;
                        if (charsConsumed == 0)
                        {
                            charsConsumed = 1;
                        }
                    }
                    consumed = charsConsumed * 2;
                    break;
                }

                case TextEncoding.Utf32:
                {
                    if (remaining.Length < 4)
                    {
                        _current = Rune.ReplacementChar;
                        consumed = remaining.Length;
                    }
                    else
                    {
                        var value = MemoryMarshal.Read<int>(remaining);
                        _current = Rune.IsValid(value) ? new Rune(value) : Rune.ReplacementChar;
                        consumed = 4;
                    }
                    break;
                }

                default:
                    throw new InvalidEncodingException(_encoding);
            }

            _remaining = remaining[consumed..];
            return true;
        }

        /// <summary>Returns this enumerator (enables <c>foreach</c>).</summary>
        public readonly RuneEnumerator GetEnumerator() => this;
    }
}
