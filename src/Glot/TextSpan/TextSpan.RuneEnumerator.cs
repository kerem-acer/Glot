using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>Returns a zero-allocation enumerator over the runes in this span.</summary>
    public RuneEnumerator EnumerateRunes() => new(_bytes, Encoding);

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
            if (_remaining.IsEmpty)
            {
                return false;
            }

            Rune.TryDecodeFirst(_remaining, _encoding, out _current, out var consumed);
            _remaining = _remaining[consumed..];
            return true;
        }

        /// <summary>Returns this enumerator (enables <c>foreach</c>).</summary>
        public readonly RuneEnumerator GetEnumerator() => this;
    }
}
