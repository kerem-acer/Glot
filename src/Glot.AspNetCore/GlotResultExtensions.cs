#if NET10_0_OR_GREATER
using Microsoft.AspNetCore.Http;

namespace Glot.AspNetCore;

/// <summary>
/// Extends <see cref="Results"/> with Glot text overloads so that
/// <c>Results.Text(text)</c>, <c>Results.Json(owned)</c>, etc. are available.
/// </summary>
public static class GlotResultExtensions
{
    extension(Results)
    {
        // ── Text ────────────────────────────────────────────────────────

        /// <inheritdoc cref="GlotResults.Text(Text, string, int?)"/>
        public static IResult Text(Text text, string contentType = "text/plain; charset=utf-8", int? statusCode = null)
            => GlotResults.Text(text, contentType, statusCode);

        /// <inheritdoc cref="GlotResults.Text(OwnedText, string, int?)"/>
        public static IResult Text(OwnedText owned, string contentType = "text/plain; charset=utf-8", int? statusCode = null)
            => GlotResults.Text(owned, contentType, statusCode);

        /// <inheritdoc cref="GlotResults.Text(LinkedTextUtf8, OwnedText[])"/>
        public static IResult Text(LinkedTextUtf8 linked, params OwnedText[] extras)
            => GlotResults.Text(linked, extras);

        /// <inheritdoc cref="GlotResults.Text(OwnedLinkedTextUtf8, OwnedText[])"/>
        public static IResult Text(OwnedLinkedTextUtf8 owned, params OwnedText[] extras)
            => GlotResults.Text(owned, extras);

        /// <inheritdoc cref="GlotResults.Text(LinkedTextUtf16, OwnedText[])"/>
        public static IResult Text(LinkedTextUtf16 linked, params OwnedText[] extras)
            => GlotResults.Text(linked, extras);

        /// <inheritdoc cref="GlotResults.Text(OwnedLinkedTextUtf16, OwnedText[])"/>
        public static IResult Text(OwnedLinkedTextUtf16 owned, params OwnedText[] extras)
            => GlotResults.Text(owned, extras);

        // ── Json ────────────────────────────────────────────────────────

        /// <inheritdoc cref="GlotResults.Json(Text, int?)"/>
        public static IResult Json(Text text, int? statusCode = null)
            => GlotResults.Json(text, statusCode);

        /// <inheritdoc cref="GlotResults.Json(OwnedText, int?)"/>
        public static IResult Json(OwnedText owned, int? statusCode = null)
            => GlotResults.Json(owned, statusCode);

        /// <inheritdoc cref="GlotResults.Json(LinkedTextUtf8, OwnedText[])"/>
        public static IResult Json(LinkedTextUtf8 linked, params OwnedText[] extras)
            => GlotResults.Json(linked, extras);

        /// <inheritdoc cref="GlotResults.Json(OwnedLinkedTextUtf8, OwnedText[])"/>
        public static IResult Json(OwnedLinkedTextUtf8 owned, params OwnedText[] extras)
            => GlotResults.Json(owned, extras);

        /// <inheritdoc cref="GlotResults.Json(LinkedTextUtf16, OwnedText[])"/>
        public static IResult Json(LinkedTextUtf16 linked, params OwnedText[] extras)
            => GlotResults.Json(linked, extras);

        /// <inheritdoc cref="GlotResults.Json(OwnedLinkedTextUtf16, OwnedText[])"/>
        public static IResult Json(OwnedLinkedTextUtf16 owned, params OwnedText[] extras)
            => GlotResults.Json(owned, extras);
    }
}
#endif
