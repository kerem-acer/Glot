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
