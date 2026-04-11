using System.Text;

namespace Glot;

public readonly ref partial struct TextSpan
{
    /// <summary>
    /// A zero-allocation enumerator that yields <see cref="TextSpan"/> segments separated by a delimiter.
    /// An empty separator yields the entire span as a single segment (no splitting).
    /// </summary>
    public ref struct SplitEnumerator
    {
        ReadOnlySpan<byte> _remaining;
        readonly TextEncoding _encoding;
        readonly ReadOnlySpan<byte> _separatorBytes;
        readonly TextEncoding _separatorEncoding;
        ReadOnlySpan<byte> _currentBytes;
        int _currentRuneCount;
        bool _done;

        internal SplitEnumerator(TextSpan text, TextSpan separator)
        {
            _remaining = text.Bytes;
            _encoding = text.Encoding;
            _separatorBytes = separator.Bytes;
            _separatorEncoding = separator.Encoding;
            _currentBytes = default;
            _currentRuneCount = 0;
            _done = false;
        }

        /// <summary>Gets the current segment.</summary>
        public readonly TextSpan Current => new(_currentBytes, _encoding, _currentRuneCount);

        /// <summary>Advances to the next segment. Returns <c>false</c> when no segments remain.</summary>
        public bool MoveNext()
        {
            if (_done)
            {
                return false;
            }

            if (_separatorBytes.IsEmpty)
            {
                _currentBytes = _remaining;
                _currentRuneCount = RuneCount.Count(_remaining, _encoding);
                _remaining = default;
                _done = true;
                return true;
            }

            var scan = _remaining;
            var byteOffset = 0;
            var runeCount = 0;

            while (!scan.IsEmpty)
            {
                if (RunePrefix.TryMatch(
                        scan, _encoding,
                        _separatorBytes, _separatorEncoding,
                        out var sepLen))
                {
                    _currentBytes = _remaining[..byteOffset];
                    _currentRuneCount = runeCount;
                    _remaining = scan[sepLen..];
                    return true;
                }

                Rune.TryDecodeFirst(scan, _encoding, out _, out var consumed);
                runeCount++;
                byteOffset += consumed;
                scan = scan[consumed..];
            }

            _currentBytes = _remaining;
            _currentRuneCount = runeCount;
            _remaining = default;
            _done = true;
            return true;
        }

        /// <summary>Returns this enumerator (enables <c>foreach</c>).</summary>
        public readonly SplitEnumerator GetEnumerator() => this;
    }
}
