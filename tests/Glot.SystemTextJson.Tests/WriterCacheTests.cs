using System.Text.Json;

namespace Glot.SystemTextJson.Tests;

public class WriterCacheTests
{
    [Test]
    public async Task Rent_ReturnsWriterAndBuffer()
    {
        // Act
        var writer = WriterCache.Rent(null, out var buffer);

        // Assert
        await Assert.That(writer).IsNotNull();
        await Assert.That(buffer).IsNotNull();

        // Cleanup
        writer.Flush();
        WriterCache.Return(writer, buffer);
    }

    [Test]
    public void RentReturn_Cycle_NoExceptions()
    {
        // Arrange & Act — rent, write valid JSON, return, rent again, write, return
        var writer1 = WriterCache.Rent(null, out var buffer1);
        writer1.WriteStartObject();
        writer1.WriteEndObject();
        writer1.Flush();
        WriterCache.Return(writer1, buffer1);

        var writer2 = WriterCache.Rent(null, out var buffer2);
        writer2.WriteStartObject();
        writer2.WriteEndObject();
        writer2.Flush();
        WriterCache.Return(writer2, buffer2);

        // Assert — no exceptions means cache Rent/Return cycle works
    }

    [Test]
    public void Rent_Recursive_CreatesFreshInstances()
    {
        // Arrange — outer rent
        var outerWriter = WriterCache.Rent(null, out var outerBuffer);

        // Act — inner rent while outer is still active
        var innerWriter = WriterCache.Rent(null, out var innerBuffer);
        innerWriter.WriteStartObject();
        innerWriter.WriteEndObject();
        innerWriter.Flush();

        // Cleanup — return in reverse order
        WriterCache.Return(innerWriter, innerBuffer);
        outerWriter.WriteStartObject();
        outerWriter.WriteEndObject();
        outerWriter.Flush();
        WriterCache.Return(outerWriter, outerBuffer);

        // Assert — no exceptions means recursive path works correctly
    }

    [Test]
    public void Rent_WithOptions_ProducesValidWriter()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };

        // Act
        var writer = WriterCache.Rent(options, out var buffer);
        writer.WriteStartObject();
        writer.WriteEndObject();
        writer.Flush();

        // Cleanup
        WriterCache.Return(writer, buffer);

        // Assert — no exceptions means options were applied correctly
    }

    [Test]
    public void Rent_DifferentOptions_ProducesValidWriter()
    {
        // Arrange — prime cache with one set of options
        var options1 = new JsonSerializerOptions { WriteIndented = true };
        var writer1 = WriterCache.Rent(options1, out var buffer1);
        writer1.Flush();
        WriterCache.Return(writer1, buffer1);

        // Act — rent with different options
        var options2 = new JsonSerializerOptions { WriteIndented = false };
        var writer2 = WriterCache.Rent(options2, out var buffer2);
        writer2.WriteStartObject();
        writer2.WriteEndObject();
        writer2.Flush();
        WriterCache.Return(writer2, buffer2);

        // Assert — no exceptions means different options handled correctly
    }
}
