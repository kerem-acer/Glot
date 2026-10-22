using System.Text.Json.Serialization;

namespace Glot.SystemTextJson.Tests;

public sealed record TextRecord(Text Name);

/// <summary>A source-generated context that reaches <see cref="Text"/>.</summary>
/// <remarks>
/// The generator raises SYSLIB1225 for <see cref="Text"/>'s span properties unless the package's suppressor handles it.
/// Warnings are errors here, so this project stops building if the suppressor stops working.
/// </remarks>
[JsonSourceGenerationOptions(Converters = [typeof(TextJsonConverter)])]
[JsonSerializable(typeof(TextRecord))]
public partial class TextJsonContext : JsonSerializerContext;
