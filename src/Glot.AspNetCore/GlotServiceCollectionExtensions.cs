using Glot.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;

namespace Glot.AspNetCore;

/// <summary>
/// Extension methods for registering Glot converters with ASP.NET Core services.
/// </summary>
public static class GlotServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="TextJsonConverter"/> and <see cref="OwnedTextJsonConverter"/>
    /// with the default HTTP JSON serializer options.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
    /// <remarks>Registers <see cref="Glot.SystemTextJson.TextJsonConverter"/> and <see cref="Glot.SystemTextJson.OwnedTextJsonConverter"/> with the default HTTP JSON options.</remarks>
    /// <example>
    /// <code>
    /// builder.Services.AddGlot();
    /// </code>
    /// </example>
    public static IServiceCollection AddGlot(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new TextJsonConverter());
            options.SerializerOptions.Converters.Add(new OwnedTextJsonConverter());
        });

        return services;
    }
}
