using System.Text.Json;
using Glot.SystemTextJson;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Glot.AspNetCore.Tests;

public class GlotServiceCollectionExtensionsTests
{
    [Test]
    public async Task AddGlot_RegistersTextJsonConverter()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddGlot();
        var provider = services.BuildServiceProvider();

        // Act
        var options = provider.GetRequiredService<IOptions<JsonOptions>>().Value;
        var converters = options.SerializerOptions.Converters;

        // Assert
        await Assert.That(converters.OfType<TextJsonConverter>().Count()).IsEqualTo(1);
    }

    [Test]
    public async Task AddGlot_RegistersOwnedTextJsonConverter()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddGlot();
        var provider = services.BuildServiceProvider();

        // Act
        var options = provider.GetRequiredService<IOptions<JsonOptions>>().Value;
        var converters = options.SerializerOptions.Converters;

        // Assert
        await Assert.That(converters.OfType<OwnedTextJsonConverter>().Count()).IsEqualTo(1);
    }

    [Test]
    public async Task AddGlot_CanSerializeText()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddGlot();
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<JsonOptions>>().Value;
        var text = Text.FromUtf8("hello"u8);

        // Act
        var json = JsonSerializer.Serialize(text, options.SerializerOptions);

        // Assert
        await Assert.That(json).IsEqualTo("\"hello\"");
    }

    [Test]
    public async Task AddGlot_CanDeserializeText()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddGlot();
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<JsonOptions>>().Value;

        // Act
        var text = JsonSerializer.Deserialize<Text>("\"hello\"", options.SerializerOptions);

        // Assert
        await Assert.That(text.ToString()).IsEqualTo("hello");
        await Assert.That(text.Encoding).IsEqualTo(TextEncoding.Utf8);
    }

    [Test]
    public async Task AddGlot_ReturnsSameServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddGlot();

        // Assert
        await Assert.That(result).IsSameReferenceAs(services);
    }
}
