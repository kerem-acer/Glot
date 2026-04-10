using System.Buffers;
using System.Text;

namespace Glot;

/// <summary>
/// Extends primitive types with <c>Parse</c> and <c>TryParse</c> overloads
/// that accept <see cref="Text"/> directly. For netstandard and net6.0 only —
/// on .NET 7+ the generic <c>ParseExtensions</c> covers all <c>ISpanParsable&lt;T&gt;</c> types.
/// </summary>
public static class ByteParseExtension
{
    extension(byte)
    {
        public static byte Parse(Text text) => PrimitiveParseHelper.Parse<byte>(text, byte.TryParse, byte.TryParse);

        public static bool TryParse(Text text, out byte result) => PrimitiveParseHelper.TryParse(
            text,
            byte.TryParse,
            byte.TryParse,
            out result);
    }
}

public static class SByteParseExtension
{
    extension(sbyte)
    {
        public static sbyte Parse(Text text) => PrimitiveParseHelper.Parse<sbyte>(text, sbyte.TryParse, sbyte.TryParse);

        public static bool TryParse(Text text, out sbyte result) => PrimitiveParseHelper.TryParse(
            text,
            sbyte.TryParse,
            sbyte.TryParse,
            out result);
    }
}

public static class Int16ParseExtension
{
    extension(short)
    {
        public static short Parse(Text text) => PrimitiveParseHelper.Parse<short>(text, short.TryParse, short.TryParse);

        public static bool TryParse(Text text, out short result) => PrimitiveParseHelper.TryParse(
            text,
            short.TryParse,
            short.TryParse,
            out result);
    }
}

public static class UInt16ParseExtension
{
    extension(ushort)
    {
        public static ushort Parse(Text text) => PrimitiveParseHelper.Parse<ushort>(text, ushort.TryParse, ushort.TryParse);

        public static bool TryParse(Text text, out ushort result) => PrimitiveParseHelper.TryParse(
            text,
            ushort.TryParse,
            ushort.TryParse,
            out result);
    }
}

public static class Int32ParseExtension
{
    extension(int)
    {
        public static int Parse(Text text) => PrimitiveParseHelper.Parse<int>(text, int.TryParse, int.TryParse);

        public static bool TryParse(Text text, out int result) => PrimitiveParseHelper.TryParse(
            text,
            int.TryParse,
            int.TryParse,
            out result);
    }
}

public static class UInt32ParseExtension
{
    extension(uint)
    {
        public static uint Parse(Text text) => PrimitiveParseHelper.Parse<uint>(text, uint.TryParse, uint.TryParse);

        public static bool TryParse(Text text, out uint result) => PrimitiveParseHelper.TryParse(
            text,
            uint.TryParse,
            uint.TryParse,
            out result);
    }
}

public static class Int64ParseExtension
{
    extension(long)
    {
        public static long Parse(Text text) => PrimitiveParseHelper.Parse<long>(text, long.TryParse, long.TryParse);

        public static bool TryParse(Text text, out long result) => PrimitiveParseHelper.TryParse(
            text,
            long.TryParse,
            long.TryParse,
            out result);
    }
}

public static class UInt64ParseExtension
{
    extension(ulong)
    {
        public static ulong Parse(Text text) => PrimitiveParseHelper.Parse<ulong>(text, ulong.TryParse, ulong.TryParse);

        public static bool TryParse(Text text, out ulong result) => PrimitiveParseHelper.TryParse(
            text,
            ulong.TryParse,
            ulong.TryParse,
            out result);
    }
}

public static class SingleParseExtension
{
    extension(float)
    {
        public static float Parse(Text text) => PrimitiveParseHelper.Parse<float>(text, float.TryParse, float.TryParse);

        public static bool TryParse(Text text, out float result) => PrimitiveParseHelper.TryParse(
            text,
            float.TryParse,
            float.TryParse,
            out result);
    }
}

public static class DoubleParseExtension
{
    extension(double)
    {
        public static double Parse(Text text) => PrimitiveParseHelper.Parse<double>(text, double.TryParse, double.TryParse);

        public static bool TryParse(Text text, out double result) => PrimitiveParseHelper.TryParse(
            text,
            double.TryParse,
            double.TryParse,
            out result);
    }
}

public static class DecimalParseExtension
{
    extension(decimal)
    {
        public static decimal Parse(Text text) => PrimitiveParseHelper.Parse<decimal>(text, decimal.TryParse, decimal.TryParse);

        public static bool TryParse(Text text, out decimal result) => PrimitiveParseHelper.TryParse(
            text,
            decimal.TryParse,
            decimal.TryParse,
            out result);
    }
}

public static class BoolParseExtension
{
    extension(bool)
    {
        public static bool Parse(Text text) => PrimitiveParseHelper.Parse<bool>(text, PrimitiveParseHelper.BoolTryParseChars, PrimitiveParseHelper.BoolTryParseBytes);

        public static bool TryParse(Text text, out bool result) => PrimitiveParseHelper.TryParse(
            text,
            PrimitiveParseHelper.BoolTryParseChars,
            PrimitiveParseHelper.BoolTryParseBytes,
            out result);
    }
}

public static class GuidParseExtension
{
    extension(Guid)
    {
        public static Guid Parse(Text text) => PrimitiveParseHelper.Parse<Guid>(text, Guid.TryParse, Guid.TryParse);

        public static bool TryParse(Text text, out Guid result) => PrimitiveParseHelper.TryParse(
            text,
            Guid.TryParse,
            Guid.TryParse,
            out result);
    }
}

public static class DateTimeParseExtension
{
    extension(DateTime)
    {
        public static DateTime Parse(Text text) => PrimitiveParseHelper.Parse<DateTime>(text, DateTime.TryParse, PrimitiveParseHelper.DateTimeTryParseBytes);

        public static bool TryParse(Text text, out DateTime result) => PrimitiveParseHelper.TryParse(
            text,
            DateTime.TryParse,
            PrimitiveParseHelper.DateTimeTryParseBytes,
            out result);
    }
}

public static class DateTimeOffsetParseExtension
{
    extension(DateTimeOffset)
    {
        public static DateTimeOffset Parse(Text text) => PrimitiveParseHelper.Parse<DateTimeOffset>(text, DateTimeOffset.TryParse, PrimitiveParseHelper.DateTimeOffsetTryParseBytes);

        public static bool TryParse(Text text, out DateTimeOffset result) => PrimitiveParseHelper.TryParse(
            text,
            DateTimeOffset.TryParse,
            PrimitiveParseHelper.DateTimeOffsetTryParseBytes,
            out result);
    }
}

static class PrimitiveParseHelper
{
    const int StackAllocThreshold = 512;

    internal delegate bool CharTryParse<T>(ReadOnlySpan<char> s, out T result);

    internal delegate bool ByteTryParse<T>(ReadOnlySpan<byte> s, out T result);

    internal static T Parse<T>(Text text, CharTryParse<T> charParse, ByteTryParse<T> byteParse)
    {
        return !TryParse(
            text,
            charParse,
            byteParse,
            out var result) ?
            throw new FormatException($"'{text}' is not a valid {typeof(T).Name}.") :
            result;
    }

    // bool, DateTime, DateTimeOffset lack TryParse(ReadOnlySpan<byte>) — shim via string
    internal static bool BoolTryParseChars(ReadOnlySpan<char> s, out bool result) => bool.TryParse(s.ToString(), out result);
    internal static bool BoolTryParseBytes(ReadOnlySpan<byte> s, out bool result) => bool.TryParse(Encoding.UTF8.GetString(s), out result);
    internal static bool DateTimeTryParseBytes(ReadOnlySpan<byte> s, out DateTime result) => DateTime.TryParse(Encoding.UTF8.GetString(s), out result);
    internal static bool DateTimeOffsetTryParseBytes(ReadOnlySpan<byte> s, out DateTimeOffset result) => DateTimeOffset.TryParse(Encoding.UTF8.GetString(s), out result);

    internal static bool TryParse<T>(Text text,
        CharTryParse<T> charParse,
        ByteTryParse<T> byteParse,
        out T result)
    {
        if (text.Encoding == TextEncoding.Utf16)
        {
            return charParse(text.Chars, out result);
        }

        if (text.Encoding == TextEncoding.Utf8)
        {
            return byteParse(text.Bytes, out result);
        }

        // UTF-32: transcode to UTF-8, then parse via byte overload
        var codePoints = text.Ints;
        var maxUtf8 = text.ByteLength;
        byte[]? rented = null;
        Span<byte> utf8 = maxUtf8 <= StackAllocThreshold
            ? stackalloc byte[maxUtf8]
            : (rented = ArrayPool<byte>.Shared.Rent(maxUtf8));
        try
        {
            var written = 0;
            foreach (var cp in codePoints)
            {
                new Rune(cp).TryEncodeToUtf8(utf8[written..], out var n);
                written += n;
            }

            return byteParse(utf8[..written], out result);
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
