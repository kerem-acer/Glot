#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace Glot;

public sealed partial class LinkedTextUtf16
{
    /// <summary>Creates a <see cref="LinkedTextUtf16"/> from an interpolated string. Each literal and hole becomes a segment.</summary>
    public static LinkedTextUtf16 Create(InterpolatedStringHandler handler)
        => handler._target._segmentCount == 0 ? Empty : handler._target;

    /// <summary>Creates a pooled <see cref="Owned"/> from an interpolated string.</summary>
    public static Owned CreateOwned(OwnedInterpolatedStringHandler handler)
        => new(handler._target);

    /// <summary>
    /// Interpolated string handler that builds a <see cref="LinkedTextUtf16"/> by capturing
    /// each literal and formatted value as a separate segment. Zero-copy for string values.
    /// Non-string values are formatted into a shared buffer owned by the <see cref="LinkedTextUtf16"/>.
    /// </summary>
    [InterpolatedStringHandler]
    public ref struct InterpolatedStringHandler
    {
        internal LinkedTextUtf16 _target;

        /// <summary>Creates a handler for building a new <see cref="LinkedTextUtf16"/>.</summary>
        public InterpolatedStringHandler(int literalLength, int formattedCount)
        {
            _target = new LinkedTextUtf16();
            _target.EnsureCapacity(formattedCount + 1);
        }

        /// <summary>Appends a literal string segment. Zero-copy.</summary>
        public void AppendLiteral(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _target.AddSegment(value.AsMemory());
            }
        }

        /// <summary>Appends a string value. Zero-copy.</summary>
        public void AppendFormatted(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _target.AddSegment(value.AsMemory());
            }
        }

        /// <summary>Appends a <see cref="ReadOnlyMemory{T}"/> segment. Zero-copy.</summary>
        public void AppendFormatted(ReadOnlyMemory<char> value)
        {
            if (!value.IsEmpty)
            {
                _target.AddSegment(value);
            }
        }

        /// <summary>Appends a <see cref="Text"/> value. Transcodes to UTF-16 if needed.</summary>
        public void AppendFormatted(Text value) => _target.AppendTextSpan(value.AsSpan());

        /// <summary>Appends a <see cref="TextSpan"/> value. Transcodes to UTF-16 if needed.</summary>
        public void AppendFormatted(TextSpan value) => _target.AppendTextSpan(value);

        /// <summary>Appends any formattable value into the shared format buffer. Zero-alloc for <see cref="ISpanFormattable"/>.</summary>
        public void AppendFormatted<T>(T value) => AppendFormattedCore(value, null);

        /// <summary>Appends a formattable value with format specifier.</summary>
        public void AppendFormatted<T>(T value, string? format) => AppendFormattedCore(value, format);

        void AppendFormattedCore<T>(T value, string? format)
        {
            if (value is string s)
            {
                AppendFormatted(s);
                return;
            }

            if (value is ISpanFormattable spanFormattable)
            {
                _target.EnsureFormatBuffer(256);
                var dest = _target._formatBuffer.AsSpan(_target._formatPosition);
                if (spanFormattable.TryFormat(dest, out var written, format, null))
                {
                    _target.AddFormattedSegment(written);
                    return;
                }

                // Didn't fit — grow and retry
                _target.EnsureFormatBuffer(dest.Length * 2);
                dest = _target._formatBuffer.AsSpan(_target._formatPosition);
                if (spanFormattable.TryFormat(dest, out written, format, null))
                {
                    _target.AddFormattedSegment(written);
                    return;
                }
            }

            // Fallback — ToString
            var str = value is IFormattable f
                ? f.ToString(format, null)
                : value?.ToString();

            if (!string.IsNullOrEmpty(str))
            {
                _target.AddSegment(str.AsMemory());
            }
        }
    }

    /// <summary>
    /// Interpolated string handler that builds a pooled <see cref="LinkedTextUtf16"/>.
    /// The instance is rented from the internal pool and returned on <see cref="Owned.Dispose"/>.
    /// </summary>
    [InterpolatedStringHandler]
    public ref struct OwnedInterpolatedStringHandler
    {
        internal LinkedTextUtf16 _target;

        /// <summary>Creates a handler using a pooled <see cref="LinkedTextUtf16"/> instance.</summary>
        public OwnedInterpolatedStringHandler(int literalLength, int formattedCount)
        {
            _target = Rent();
            _target.EnsureCapacity(formattedCount + 1);
        }

        /// <summary>Appends a literal string segment. Zero-copy.</summary>
        public void AppendLiteral(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _target.AddSegment(value.AsMemory());
            }
        }

        /// <summary>Appends a string value. Zero-copy.</summary>
        public void AppendFormatted(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _target.AddSegment(value.AsMemory());
            }
        }

        /// <summary>Appends a <see cref="ReadOnlyMemory{T}"/> segment. Zero-copy.</summary>
        public void AppendFormatted(ReadOnlyMemory<char> value)
        {
            if (!value.IsEmpty)
            {
                _target.AddSegment(value);
            }
        }

        /// <summary>Appends a <see cref="Text"/> value. Transcodes to UTF-16 if needed.</summary>
        public void AppendFormatted(Text value) => _target.AppendTextSpan(value.AsSpan());

        /// <summary>Appends a <see cref="TextSpan"/> value. Transcodes to UTF-16 if needed.</summary>
        public void AppendFormatted(TextSpan value) => _target.AppendTextSpan(value);

        /// <summary>Appends any formattable value into the shared format buffer. Zero-alloc for <see cref="ISpanFormattable"/>.</summary>
        public void AppendFormatted<T>(T value) => AppendFormattedCore(value, null);

        /// <summary>Appends a formattable value with format specifier.</summary>
        public void AppendFormatted<T>(T value, string? format) => AppendFormattedCore(value, format);

        void AppendFormattedCore<T>(T value, string? format)
        {
            if (value is string s)
            {
                AppendFormatted(s);
                return;
            }

            if (value is ISpanFormattable spanFormattable)
            {
                _target.EnsureFormatBuffer(256);
                var dest = _target._formatBuffer.AsSpan(_target._formatPosition);
                if (spanFormattable.TryFormat(dest, out var written, format, null))
                {
                    _target.AddFormattedSegment(written);
                    return;
                }

                _target.EnsureFormatBuffer(dest.Length * 2);
                dest = _target._formatBuffer.AsSpan(_target._formatPosition);
                if (spanFormattable.TryFormat(dest, out written, format, null))
                {
                    _target.AddFormattedSegment(written);
                    return;
                }
            }

            var str = value is IFormattable f
                ? f.ToString(format, null)
                : value?.ToString();

            if (!string.IsNullOrEmpty(str))
            {
                _target.AddSegment(str.AsMemory());
            }
        }
    }
}
#endif
