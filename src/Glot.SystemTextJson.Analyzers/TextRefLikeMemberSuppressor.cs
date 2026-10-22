using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Glot.SystemTextJson.Analyzers;

/// <summary>
/// Suppresses SYSLIB1225 for the span properties of <c>Glot.Text</c>: <c>Bytes</c>, <c>Chars</c> and <c>Ints</c>.
/// </summary>
/// <remarks>
/// The System.Text.Json source generator warns about every public ref-like property of every type a
/// <c>JsonSerializerContext</c> reaches, even when a converter serializes the type. <c>Text</c> is always
/// serialized by a converter, so these properties are never read.
/// </remarks>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class TextRefLikeMemberSuppressor : DiagnosticSuppressor
{
    static readonly SuppressionDescriptor Rule = new(
        id: "GLOTSPR0001",
        suppressedDiagnosticId: "SYSLIB1225",
        justification: "Glot.Text is serialized by a converter, so its span properties are never read.");

    // The generator's messages for Text's span properties. They are compared in the invariant culture, so localized builds match too.
    static readonly string[] SuppressedMessages =
    [
        "The type 'Text' includes the ref like property, field or constructor parameter 'Bytes'.",
        "The type 'Text' includes the ref like property, field or constructor parameter 'Chars'.",
        "The type 'Text' includes the ref like property, field or constructor parameter 'Ints'.",
    ];

    /// <inheritdoc/>
    public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions { get; } = ImmutableArray.Create(Rule);

    /// <inheritdoc/>
    public override void ReportSuppressions(SuppressionAnalysisContext context)
    {
        foreach (var diagnostic in context.ReportedDiagnostics)
        {
            if (IsTextSpanProperty(diagnostic, context.Compilation, context.CancellationToken))
            {
                context.ReportSuppression(Suppression.Create(Rule, diagnostic));
            }
        }
    }

    static bool IsTextSpanProperty(Diagnostic diagnostic, Compilation compilation, CancellationToken cancellationToken)
    {
        var message = diagnostic.GetMessage(CultureInfo.InvariantCulture);
        if (!SuppressedMessages.Any(suppressed => message.StartsWith(suppressed, StringComparison.Ordinal)))
        {
            return false;
        }

        // The generator reports a member declared in this compilation at the member itself, and a member of a
        // referenced assembly (such as Glot.Text's) at the context class. A consumer's own Text type keeps its warning.
        var path = diagnostic.Location.GetLineSpan().Path;
        var tree = compilation.SyntaxTrees.FirstOrDefault(t => t.FilePath == path);
        return tree?.GetRoot(cancellationToken).FindNode(diagnostic.Location.SourceSpan) is ClassDeclarationSyntax;
    }
}
