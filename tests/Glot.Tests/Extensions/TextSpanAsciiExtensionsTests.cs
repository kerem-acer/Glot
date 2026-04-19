using System.Runtime.InteropServices;

namespace Glot.Tests;

public class TextSpanAsciiExtensionsTests
{
    // --- TryWriteAsciiAsChars ---

    [Test]
    public async Task TryWriteAsciiAsChars_ValidAscii_WritesCharsAndReturnsByteLength()
    {
        // Arrange
        var source = "Hello"u8.ToArray();
        var destination = new byte[source.Length * 2];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiAsChars(source, destination, out var byteLength);
        var expectedByteLength = source.Length * 2;
        var chars = MemoryMarshal.Cast<byte, char>(destination.AsSpan(0, byteLength));
        var str = new string(chars);

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(byteLength).IsEqualTo(expectedByteLength);
        await Assert.That(str).IsEqualTo("Hello");
    }

    [Test]
    public async Task TryWriteAsciiAsChars_NonAscii_ReturnsFalse()
    {
        // Arrange
        var source = new byte[] { 0x48, 0x80 };
        var destination = new byte[source.Length * 2];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiAsChars(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    [Test]
    public async Task TryWriteAsciiAsChars_TooSmallBuffer_ReturnsFalse()
    {
        // Arrange
        var source = "Hello"u8.ToArray();
        var destination = new byte[source.Length * 2 - 1];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiAsChars(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    // --- TryWriteAsciiAsInts ---

    [Test]
    public async Task TryWriteAsciiAsInts_ValidAscii_WritesIntsAndReturnsByteLength()
    {
        // Arrange
        var source = "Hi"u8.ToArray();
        var destination = new byte[source.Length * 4];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiAsInts(source, destination, out var byteLength);
        var expectedByteLength = source.Length * 4;
        var ints = MemoryMarshal.Cast<byte, int>(destination.AsSpan(0, byteLength));
        var v0 = ints[0];
        var v1 = ints[1];

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(byteLength).IsEqualTo(expectedByteLength);
        await Assert.That(v0).IsEqualTo('H');
        await Assert.That(v1).IsEqualTo('i');
    }

    [Test]
    public async Task TryWriteAsciiAsInts_NonAscii_ReturnsFalse()
    {
        // Arrange
        var source = new byte[] { 0x41, 0xFF };
        var destination = new byte[source.Length * 4];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiAsInts(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    [Test]
    public async Task TryWriteAsciiAsInts_TooSmallBuffer_ReturnsFalse()
    {
        // Arrange
        var source = "AB"u8.ToArray();
        var destination = new byte[source.Length * 4 - 1];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiAsInts(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    // --- TryWriteAsciiCharsAsBytes ---

    [Test]
    public async Task TryWriteAsciiCharsAsBytes_ValidAscii_WritesBytesAndReturnsByteLength()
    {
        // Arrange
        var source = "Hello".ToCharArray();
        var destination = new byte[source.Length];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiCharsAsBytes(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(byteLength).IsEqualTo(source.Length);
        await Assert.That(destination[0]).IsEqualTo((byte)'H');
        await Assert.That(destination[4]).IsEqualTo((byte)'o');
    }

    [Test]
    public async Task TryWriteAsciiCharsAsBytes_NonAscii_ReturnsFalse()
    {
        // Arrange
        var source = new[] { 'A', '\u00E9' };
        var destination = new byte[source.Length];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiCharsAsBytes(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    [Test]
    public async Task TryWriteAsciiCharsAsBytes_TooSmallBuffer_ReturnsFalse()
    {
        // Arrange
        var source = "Hello".ToCharArray();
        var destination = new byte[source.Length - 1];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiCharsAsBytes(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    // --- TryWriteAsciiCharsAsInts ---

    [Test]
    public async Task TryWriteAsciiCharsAsInts_ValidAscii_WritesIntsAndReturnsByteLength()
    {
        // Arrange
        var source = "AB".ToCharArray();
        var destination = new byte[source.Length * 4];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiCharsAsInts(source, destination, out var byteLength);
        var expectedByteLength = source.Length * 4;
        var ints = MemoryMarshal.Cast<byte, int>(destination.AsSpan(0, byteLength));
        var v0 = ints[0];
        var v1 = ints[1];

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(byteLength).IsEqualTo(expectedByteLength);
        await Assert.That(v0).IsEqualTo('A');
        await Assert.That(v1).IsEqualTo('B');
    }

    [Test]
    public async Task TryWriteAsciiCharsAsInts_NonAscii_ReturnsFalse()
    {
        // Arrange
        var source = new[] { 'A', '\u0100' };
        var destination = new byte[source.Length * 4];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiCharsAsInts(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    [Test]
    public async Task TryWriteAsciiCharsAsInts_TooSmallBuffer_ReturnsFalse()
    {
        // Arrange
        var source = "AB".ToCharArray();
        var destination = new byte[source.Length * 4 - 1];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiCharsAsInts(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    // --- TryWriteAsciiIntsAsBytes ---

    [Test]
    public async Task TryWriteAsciiIntsAsBytes_ValidAscii_WritesBytesAndReturnsByteLength()
    {
        // Arrange
        int[] source = ['H', 'i'];
        var destination = new byte[source.Length];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiIntsAsBytes(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(byteLength).IsEqualTo(source.Length);
        await Assert.That(destination[0]).IsEqualTo((byte)'H');
        await Assert.That(destination[1]).IsEqualTo((byte)'i');
    }

    [Test]
    public async Task TryWriteAsciiIntsAsBytes_NonAscii_ReturnsFalse()
    {
        // Arrange
        int[] source = [0x41, 0x80];
        var destination = new byte[source.Length];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiIntsAsBytes(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    [Test]
    public async Task TryWriteAsciiIntsAsBytes_TooSmallBuffer_ReturnsFalse()
    {
        // Arrange
        int[] source = ['A', 'B', 'C'];
        var destination = new byte[source.Length - 1];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiIntsAsBytes(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    // --- TryWriteAsciiIntsAsChars ---

    [Test]
    public async Task TryWriteAsciiIntsAsChars_ValidAscii_WritesCharsAndReturnsByteLength()
    {
        // Arrange
        int[] source = ['H', 'i'];
        var destination = new byte[source.Length * 2];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiIntsAsChars(source, destination, out var byteLength);
        var expectedByteLength = source.Length * 2;
        var chars = MemoryMarshal.Cast<byte, char>(destination.AsSpan(0, byteLength));
        var str = new string(chars);

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(byteLength).IsEqualTo(expectedByteLength);
        await Assert.That(str).IsEqualTo("Hi");
    }

    [Test]
    public async Task TryWriteAsciiIntsAsChars_NonAscii_ReturnsFalse()
    {
        // Arrange
        int[] source = [0x41, 0x100];
        var destination = new byte[source.Length * 2];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiIntsAsChars(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    [Test]
    public async Task TryWriteAsciiIntsAsChars_TooSmallBuffer_ReturnsFalse()
    {
        // Arrange
        int[] source = ['X', 'Y'];
        var destination = new byte[source.Length * 2 - 1];

        // Act
        var result = TextSpanAsciiExtensions.TryWriteAsciiIntsAsChars(source, destination, out var byteLength);

        // Assert
        await Assert.That(result).IsFalse();
        await Assert.That(byteLength).IsEqualTo(0);
    }

    // --- TryConvertAscii (extension on TextSpan) ---
    // TryConvertAscii is exercised indirectly via cross-encoding search operations.
    // The static TryWriteAscii* helpers above directly cover its internal implementation.
}
