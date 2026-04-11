namespace Glot;

public readonly partial struct LinkedTextUtf8Span
{
    /// <summary>Returns a zero-allocation enumerator over the visible segments.</summary>
    public SegmentEnumerator EnumerateSegments() => new(this);

    /// <summary>
    /// Enumerates the visible <see cref="ReadOnlyMemory{T}"/> segments of a
    /// <see cref="LinkedTextUtf8Span"/>.
    /// </summary>
    public struct SegmentEnumerator
    {
        readonly LinkedTextUtf8? _data;
        readonly int _startSegment;
        readonly int _startIndex;
        readonly int _endSegment;
        readonly int _endIndex;
        int _currentSegment;
        bool _started;

        internal SegmentEnumerator(LinkedTextUtf8Span span)
        {
            _data = span._data;
            _startSegment = span._startSegment;
            _startIndex = span._startIndex;
            _endSegment = span._endSegment;
            _endIndex = span._endIndex;
            _currentSegment = span._startSegment;
            Current = default;
            _started = false;
        }

        /// <summary>Gets the current segment.</summary>
        public ReadOnlyMemory<byte> Current { get; private set; }

        /// <summary>Advances to the next segment.</summary>
        public bool MoveNext()
        {
            if (_data is null)
            {
                return false;
            }

            while (true)
            {
                if (!_started)
                {
                    _started = true;
                    _currentSegment = _startSegment;
                }
                else
                {
                    _currentSegment++;
                }

                if (_currentSegment > _endSegment)
                {
                    return false;
                }

                var segMem = _data.GetSegment(_currentSegment);
                var sliceStart = _currentSegment == _startSegment ? _startIndex : 0;
                var sliceEnd = _currentSegment == _endSegment ? _endIndex : segMem.Length;

                if (sliceStart == sliceEnd)
                {
                    continue;
                }

                Current = segMem.Slice(sliceStart, sliceEnd - sliceStart);
                return true;
            }
        }

        /// <summary>Returns this enumerator (enables <c>foreach</c>).</summary>
        public readonly SegmentEnumerator GetEnumerator() => this;
    }
}
