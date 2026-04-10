using System.Numerics;

namespace Glot.Tests;

public class VectorExtensionsTests
{
    [Test]
    public async Task HorizontalSum_ByteVectorAllZeros_ReturnsZero()
    {
        // Arrange
        var vec = Vector<byte>.Zero;

        // Act
        var sum = vec.HorizontalSum();

        // Assert
        await Assert.That(sum).IsEqualTo(0);
    }

    [Test]
    public async Task HorizontalSum_ByteVectorAllOnes_ReturnsCount()
    {
        // Arrange
        var vec = Vector<byte>.One;
        var expected = Vector<byte>.Count;

        // Act
        var sum = vec.HorizontalSum();

        // Assert
        await Assert.That(sum).IsEqualTo(expected);
    }

    [Test]
    public async Task HorizontalSum_ByteVectorAllMax_ReturnsCountTimesMax()
    {
        // Arrange
        var vec = new Vector<byte>(byte.MaxValue);
        var expected = Vector<byte>.Count * byte.MaxValue;

        // Act
        var sum = vec.HorizontalSum();

        // Assert
        await Assert.That(sum).IsEqualTo(expected);
    }

    [Test]
    public async Task HorizontalSum_ByteVectorMixedValues_ReturnsTotalSum()
    {
        // Arrange
        var values = new byte[Vector<byte>.Count];
        var expected = 0;
        for (var i = 0; i < values.Length; i++)
        {
            values[i] = (byte)(i % 10);
            expected += values[i];
        }

        var vec = new Vector<byte>(values);

        // Act
        var sum = vec.HorizontalSum();

        // Assert
        await Assert.That(sum).IsEqualTo(expected);
    }

    [Test]
    public async Task HorizontalSum_UshortVectorAllZeros_ReturnsZero()
    {
        // Arrange
        var vec = Vector<ushort>.Zero;

        // Act
        var sum = vec.HorizontalSum();

        // Assert
        await Assert.That(sum).IsEqualTo(0);
    }

    [Test]
    public async Task HorizontalSum_UshortVectorAllOnes_ReturnsCount()
    {
        // Arrange
        var vec = Vector<ushort>.One;
        var expected = Vector<ushort>.Count;

        // Act
        var sum = vec.HorizontalSum();

        // Assert
        await Assert.That(sum).IsEqualTo(expected);
    }

    [Test]
    public async Task HorizontalSum_UshortVectorAllMax_ReturnsCountTimesMax()
    {
        // Arrange
        var vec = new Vector<ushort>(ushort.MaxValue);
        var expected = Vector<ushort>.Count * ushort.MaxValue;

        // Act
        var sum = vec.HorizontalSum();

        // Assert
        await Assert.That(sum).IsEqualTo(expected);
    }

    [Test]
    public async Task HorizontalSum_UshortVectorMixedValues_ReturnsTotalSum()
    {
        // Arrange
        var values = new ushort[Vector<ushort>.Count];
        var expected = 0;
        for (var i = 0; i < values.Length; i++)
        {
            values[i] = (ushort)(i * 100);
            expected += values[i];
        }

        var vec = new Vector<ushort>(values);

        // Act
        var sum = vec.HorizontalSum();

        // Assert
        await Assert.That(sum).IsEqualTo(expected);
    }
}
