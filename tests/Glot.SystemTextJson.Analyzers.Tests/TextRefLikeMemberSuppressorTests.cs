using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Glot.SystemTextJson.Analyzers.Tests;

public class TextRefLikeMemberSuppressorTests
{
    static readonly ISourceGenerator JsonSourceGenerator = LoadJsonSourceGenerator();

    static readonly MetadataReference[] References =
    [
        .. ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
            .Split(Path.PathSeparator)
            .Where(path => Path.GetDirectoryName(path) == Path.GetDirectoryName(typeof(object).Assembly.Location))
            .Select(path => MetadataReference.CreateFromFile(path)),
        MetadataReference.CreateFromFile(typeof(Text).Assembly.Location),
    ];

    [Test]
    public async Task ReportSuppressions_ContextReachesText_SuppressesSpanProperties()
    {
        // Arrange
        const string source = """
            using System.Text.Json.Serialization;
            using Glot;

            public sealed record Person(Text Name);

            [JsonSerializable(typeof(Person))]
            public partial class PersonContext : JsonSerializerContext;
            """;

        // Act
        var diagnostics = await GenerateAndSuppress(source);

        // Assert
        await Verify(diagnostics);
    }

    [Test]
    public async Task ReportSuppressions_OwnTypeNamedText_KeepsWarning()
    {
        // Arrange — the consumer's own Text type, declared in the compilation, is not Glot's
        const string source = """
            using System;
            using System.Text.Json.Serialization;

            public sealed class Text
            {
                public ReadOnlySpan<byte> Bytes => default;
            }

            public sealed record Person(Text Name);

            [JsonSerializable(typeof(Person))]
            public partial class PersonContext : JsonSerializerContext;
            """;

        // Act
        var diagnostics = await GenerateAndSuppress(source);

        // Assert
        await Verify(diagnostics);
    }

    [Test]
    public async Task ReportSuppressions_OtherTypeSpanProperty_KeepsWarning()
    {
        // Arrange
        const string source = """
            using System;
            using System.Text.Json.Serialization;

            public sealed class Blob
            {
                public ReadOnlySpan<byte> Data => default;
            }

            [JsonSerializable(typeof(Blob))]
            public partial class BlobContext : JsonSerializerContext;
            """;

        // Act
        var diagnostics = await GenerateAndSuppress(source);

        // Assert
        await Verify(diagnostics);
    }

    /// <summary>Runs the System.Text.Json source generator over <paramref name="source"/>, then the suppressor over its SYSLIB1225 warnings.</summary>
    static async Task<SuppressionResult[]> GenerateAndSuppress(string source)
    {
        var compilation = CSharpCompilation.Create(
            "Consumer",
            [CSharpSyntaxTree.ParseText(source, path: "Consumer.cs")],
            References,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        CSharpGeneratorDriver.Create(JsonSourceGenerator)
            .RunGeneratorsAndUpdateCompilation(compilation, out var generated, out var generatorDiagnostics);

        // The compiler runs suppressors over generator warnings; replaying them from an analyzer does the same in memory.
        var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(new ReplayAnalyzer(generatorDiagnostics), new TextRefLikeMemberSuppressor());
        var options = new CompilationWithAnalyzersOptions(
            new AnalyzerOptions([]),
            onAnalyzerException: null,
            concurrentAnalysis: false,
            logAnalyzerExecutionTime: false,
            reportSuppressedDiagnostics: true);
        var diagnostics = await generated.WithAnalyzers(analyzers, options).GetAnalyzerDiagnosticsAsync();

        return
        [
            .. diagnostics
                .Where(diagnostic => diagnostic.Id == "SYSLIB1225")
                .Select(diagnostic => new SuppressionResult(diagnostic.GetMessage(CultureInfo.InvariantCulture), diagnostic.IsSuppressed))
                .OrderBy(result => result.Message, StringComparer.Ordinal),
        ];
    }

    static ISourceGenerator LoadJsonSourceGenerator()
    {
        var path = typeof(TextRefLikeMemberSuppressorTests).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .Single(attribute => attribute.Key == "JsonSourceGeneratorPath")
            .Value!;
        var type = System.Reflection.Assembly.LoadFrom(path).GetType("System.Text.Json.SourceGeneration.JsonSourceGenerator", throwOnError: true)!;
        return ((IIncrementalGenerator)Activator.CreateInstance(type)!).AsSourceGenerator();
    }

    sealed record SuppressionResult(string Message, bool IsSuppressed);

    /// <summary>Reports the given diagnostics again, so <see cref="CompilationWithAnalyzers"/> hands them to the suppressor.</summary>
    sealed class ReplayAnalyzer(ImmutableArray<Diagnostic> diagnostics) : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [.. diagnostics.Select(diagnostic => diagnostic.Descriptor).Distinct()];

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterCompilationAction(compilation =>
            {
                foreach (var diagnostic in diagnostics)
                {
                    compilation.ReportDiagnostic(diagnostic);
                }
            });
        }
    }
}
