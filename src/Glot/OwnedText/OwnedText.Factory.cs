using System.Buffers;
using System.Runtime.InteropServices;

namespace Glot;

public sealed partial class OwnedText
{
    /// <summary>Creates an <see cref="OwnedText"/> that wraps an existing <see cref="Text"/> without copying.</summary>
    /// <param name="value">The source <see cref="Text"/> to wrap.</param>
    /// <returns>A new <see cref="OwnedText"/> wrapping the provided data, or <see cref="OwnedText.Empty"/> if the input is empty.</returns>
    /// <remarks>
    /// Zero-copy: the resulting <see cref="OwnedText"/> shares its backing memory with the source <see cref="Text"/>.
    /// <see cref="Dispose"/> returns the wrapper to its pool but does not affect the source backing.
    /// The caller is responsible for ensuring the source remains valid for the lifetime of the returned instance.
    /// </remarks>
    /// <example>
    /// <code>
    /// Text view = Text.From("hello world");
    /// using var owned = OwnedText.From(view);
    /// </code>
    /// </example>
    public static OwnedText From(Text value)
    {
        if (value.IsEmpty)
        {
            return Empty;
        }

        var owned = GetFromPool();
        owned.InitializeView(value);
        return owned;
    }

    /// <summary>Creates a UTF-16 <see cref="OwnedText"/> that wraps a <see cref="string"/> without copying.</summary>
    /// <param name="value">The source string.</param>
    /// <param name="countRunes">Whether to count runes during construction.</param>
    /// <returns>A new <see cref="OwnedText"/> wrapping the provided data, or <see cref="OwnedText.Empty"/> if the input is null or empty.</returns>
    /// <remarks>
    /// Zero-copy: the resulting <see cref="OwnedText"/> references the source <see cref="string"/> directly.
    /// <see cref="Dispose"/> returns the wrapper to its pool but does not affect the string.
    /// </remarks>
    /// <example>
    /// <code>
    /// using var owned = OwnedText.From("hello world");
    /// Text text = owned.Text; // valid until disposed
    /// </code>
    /// </example>
    public static OwnedText From(string value, bool countRunes = true)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Empty;
        }

        var owned = GetFromPool();
        owned.InitializeView(Text.From(value, countRunes));
        return owned;
    }

    /// <summary>Creates a UTF-8 <see cref="OwnedText"/> by copying the bytes.</summary>
    /// <param name="value">The UTF-8 bytes to copy.</param>
    /// <param name="countRunes">Whether to count runes during construction.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the provided data, or <see cref="OwnedText.Empty"/> if the input is empty.</returns>
    /// <remarks>Copies the bytes into a buffer rented from <see cref="System.Buffers.ArrayPool{T}"/>.</remarks>
    /// <example>
    /// <code>
    /// using var owned = OwnedText.FromUtf8("hello"u8);
    /// Text text = owned.Text; // valid until disposed
    /// </code>
    /// </example>
    public static OwnedText FromUtf8(ReadOnlySpan<byte> value, bool countRunes = true)
        => FromBytes(value, TextEncoding.Utf8, countRunes);

    /// <summary>Creates a UTF-8 <see cref="OwnedText"/> from a byte sequence.</summary>
    /// <param name="value">The UTF-8 byte sequence to copy.</param>
    /// <param name="countRunes">Whether to count runes during construction.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the provided data, or <see cref="OwnedText.Empty"/> if the input is empty.</returns>
    /// <remarks>Copies all segments into a single buffer rented from <see cref="System.Buffers.ArrayPool{T}"/>.</remarks>
    public static OwnedText FromUtf8(ReadOnlySequence<byte> value, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return Empty;
        }

        if (value.IsSingleSegment)
        {
            return FromUtf8(value.First.Span, countRunes);
        }

        var length = checked((int)value.Length);
        var buffer = ArrayPool<byte>.Shared.Rent(length);
        value.CopyTo(buffer);
        var runeLength = countRunes ? RuneCount.Count(buffer.AsSpan(0, length), TextEncoding.Utf8) : 0;

        var owned = GetFromPool();
        owned.Initialize(buffer, length, TextEncoding.Utf8, runeLength, BackingType.ByteArray);
        return owned;
    }

    /// <summary>Creates a UTF-8 <see cref="OwnedText"/> from ASCII bytes.</summary>
    /// <param name="value">The ASCII bytes to copy.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the provided data, or <see cref="OwnedText.Empty"/> if the input is empty.</returns>
    /// <remarks>Copies the bytes into a buffer rented from <see cref="System.Buffers.ArrayPool{T}"/>. Skips rune counting — the rune count equals the byte count for ASCII.</remarks>
    public static OwnedText FromAscii(ReadOnlySpan<byte> value)
    {
        if (value.IsEmpty)
        {
            return Empty;
        }

        var buffer = ArrayPool<byte>.Shared.Rent(value.Length);
        value.CopyTo(buffer);

        var owned = GetFromPool();
        owned.Initialize(buffer, value.Length, TextEncoding.Utf8, value.Length, BackingType.ByteArray);
        return owned;
    }

    /// <summary>Creates a UTF-16 <see cref="OwnedText"/> by copying the chars.</summary>
    /// <param name="value">The UTF-16 characters to copy.</param>
    /// <param name="countRunes">Whether to count runes during construction.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the provided data, or <see cref="OwnedText.Empty"/> if the input is empty.</returns>
    /// <remarks>Copies the chars (as bytes) into a buffer rented from <see cref="System.Buffers.ArrayPool{T}"/>.</remarks>
    public static OwnedText FromChars(ReadOnlySpan<char> value, bool countRunes = true)
        => FromBytes(MemoryMarshal.AsBytes(value), TextEncoding.Utf16, countRunes);

    /// <summary>Creates a UTF-32 <see cref="OwnedText"/> by copying the code points.</summary>
    /// <param name="value">The UTF-32 code points to copy.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the provided data, or <see cref="OwnedText.Empty"/> if the input is empty.</returns>
    /// <remarks>Copies the code points (as bytes) into a buffer rented from <see cref="System.Buffers.ArrayPool{T}"/>.</remarks>
    public static OwnedText FromUtf32(ReadOnlySpan<int> value)
        => FromBytes(MemoryMarshal.AsBytes(value), TextEncoding.Utf32, countRunes: true);

    /// <summary>Creates an <see cref="OwnedText"/> by copying raw bytes.</summary>
    /// <param name="value">The bytes to copy.</param>
    /// <param name="encoding">The encoding of the bytes.</param>
    /// <param name="countRunes">Whether to count runes during construction.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the provided data, or <see cref="OwnedText.Empty"/> if the input is empty.</returns>
    /// <remarks>Copies the bytes into a buffer rented from <see cref="System.Buffers.ArrayPool{T}"/>.</remarks>
    public static OwnedText FromBytes(ReadOnlySpan<byte> value, TextEncoding encoding, bool countRunes = true)
    {
        if (value.IsEmpty)
        {
            return Empty;
        }

        var buffer = ArrayPool<byte>.Shared.Rent(value.Length);
        value.CopyTo(buffer);
        var runeLength = countRunes ? RuneCount.Count(value, encoding) : 0;

        var owned = GetFromPool();
        owned.Initialize(buffer, value.Length, encoding, runeLength, BackingType.ByteArray);
        return owned;
    }

    /// <summary>Creates an <see cref="OwnedText"/> that takes ownership of a buffer.</summary>
    /// <param name="buffer">The pooled byte array to take ownership of.</param>
    /// <param name="byteLength">The number of valid bytes in the buffer.</param>
    /// <param name="encoding">The encoding of the bytes.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the provided data.</returns>
    /// <remarks>Takes ownership of the buffer — the caller must not use or return it after this call. The buffer will be returned to <see cref="System.Buffers.ArrayPool{T}"/> when the <see cref="OwnedText"/> is disposed.</remarks>
    /// <example>
    /// <code>
    /// var buffer = ArrayPool&lt;byte&gt;.Shared.Rent(1024);
    /// // ... fill buffer ...
    /// using var owned = OwnedText.Create(buffer, bytesWritten, TextEncoding.Utf8);
    /// </code>
    /// </example>
    public static OwnedText Create(byte[] buffer, int byteLength, TextEncoding encoding)
    {
        var runeLength = RuneCount.Count(buffer.AsSpan(0, byteLength), encoding);
        var owned = GetFromPool();
        owned.Initialize(buffer, byteLength, encoding, runeLength, BackingType.ByteArray);
        return owned;
    }

    /// <summary>
    /// Takes ownership of an existing pooled <c>byte[]</c> buffer with a pre-computed rune length. Zero-copy.
    /// The caller must not use the buffer after this call.
    /// </summary>
    internal static OwnedText Create(byte[] buffer, int byteLength, TextEncoding encoding, int runeLength)
    {
        var owned = GetFromPool();
        owned.Initialize(buffer, byteLength, encoding, runeLength, BackingType.ByteArray);
        return owned;
    }

    /// <summary>Creates an <see cref="OwnedText"/> that takes ownership of a UTF-16 buffer.</summary>
    /// <param name="buffer">The pooled char array to take ownership of.</param>
    /// <param name="charLength">The number of valid characters in the buffer.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the provided data.</returns>
    /// <remarks>Takes ownership of the buffer — the caller must not use or return it after this call.</remarks>
    public static OwnedText Create(char[] buffer, int charLength)
    {
        var bytes = MemoryMarshal.AsBytes(buffer.AsSpan(0, charLength));
        var runeLength = RuneCount.Count(bytes, TextEncoding.Utf16);
        var owned = GetFromPool();
        owned.Initialize(buffer, charLength * 2, TextEncoding.Utf16, runeLength, BackingType.CharArray);
        return owned;
    }

    /// <summary>Creates an <see cref="OwnedText"/> that takes ownership of a UTF-32 buffer.</summary>
    /// <param name="buffer">The pooled int array to take ownership of.</param>
    /// <param name="intLength">The number of valid integers in the buffer.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the provided data.</returns>
    /// <remarks>Takes ownership of the buffer — the caller must not use or return it after this call.</remarks>
    public static OwnedText Create(int[] buffer, int intLength)
    {
        var bytes = MemoryMarshal.AsBytes(buffer.AsSpan(0, intLength));
        var runeLength = RuneCount.Count(bytes, TextEncoding.Utf32);
        var owned = GetFromPool();
        owned.Initialize(buffer, intLength * 4, TextEncoding.Utf32, runeLength, BackingType.IntArray);
        return owned;
    }

#if NET6_0_OR_GREATER
    /// <summary>Creates an <see cref="OwnedText"/> from an interpolated string.</summary>
    /// <param name="handler">The interpolated string handler.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the interpolated content.</returns>
    /// <remarks>Uses a pooled <see cref="TextBuilder"/> internally. The caller must dispose the result.</remarks>
    /// <example>
    /// <code>
    /// using var owned = OwnedText.Create($"count: {items.Length}");
    /// </code>
    /// </example>
    public static OwnedText Create(TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();

    /// <summary>Creates an <see cref="OwnedText"/> in the specified encoding from an interpolated string.</summary>
    /// <param name="encoding">The target encoding.</param>
    /// <param name="handler">The interpolated string handler.</param>
    /// <returns>A new <see cref="OwnedText"/> containing the interpolated content.</returns>
    public static OwnedText Create(
        TextEncoding encoding,
        [System.Runtime.CompilerServices.InterpolatedStringHandlerArgument("encoding")]
        TextInterpolatedStringHandler handler)
        => handler.ToOwnedText();
#endif
}
