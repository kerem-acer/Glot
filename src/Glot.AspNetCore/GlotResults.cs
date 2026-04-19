using Microsoft.AspNetCore.Http;

namespace Glot.AspNetCore;

/// <summary>
/// Factory methods for creating Glot-specific <see cref="IResult"/> instances.
/// </summary>
public static class GlotResults
{
    const string TextContentType = "text/plain; charset=utf-8";
    const string JsonContentType = "application/json; charset=utf-8";

    // ── Text ────────────────────────────────────────────────────────────────

    /// <summary>Writes <paramref name="text"/> as UTF-8 to the response body.</summary>
    /// <param name="text">The text content to write.</param>
    /// <param name="contentType">The response content type.</param>
    /// <param name="statusCode">The HTTP status code, or <c>null</c> to use the default.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 text.</returns>
    /// <example>
    /// <code>
    /// app.MapGet("/hello", () => GlotResults.Text(Text.FromUtf8("hello"u8)));
    /// </code>
    /// </example>
    public static IResult Text(Text text, string contentType = TextContentType, int? statusCode = null)
        => new Utf8TextResult(text, contentType, statusCode);

    /// <summary>Writes <paramref name="owned"/> as UTF-8 and disposes the pooled buffer after writing.</summary>
    /// <param name="owned">The owned text content to write.</param>
    /// <param name="contentType">The response content type.</param>
    /// <param name="statusCode">The HTTP status code, or <c>null</c> to use the default.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 text.</returns>
    /// <example>
    /// <code>
    /// app.MapGet("/data", () => GlotResults.Text(OwnedText.FromUtf8("hello"u8)));
    /// </code>
    /// </example>
    public static IResult Text(OwnedText owned, string contentType = TextContentType, int? statusCode = null)
    {
        var result = new Utf8TextResult(owned.Text, contentType, statusCode);
        result.AddOwnedText(owned);
        return result;
    }

    /// <summary>Writes <paramref name="text"/> as UTF-8 and disposes <paramref name="extras"/> after writing.</summary>
    /// <param name="text">The text content to write.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 text.</returns>
    public static IResult Text(Text text, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(text, TextContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="linked"/> segments as UTF-8 and disposes <paramref name="extras"/> after writing.</summary>
    /// <param name="linked">The linked UTF-8 text to write.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 text.</returns>
    public static IResult Text(LinkedTextUtf8 linked, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(linked, TextContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="owned"/> segments as UTF-8 and disposes all after writing.</summary>
    /// <param name="owned">The owned linked UTF-8 text to write and dispose.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 text.</returns>
    public static IResult Text(OwnedLinkedTextUtf8 owned, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(owned.Data ?? throw new ObjectDisposedException(nameof(owned)), TextContentType, null);
        result.SetOwnedLinkedUtf8(owned);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="linked"/> segments (transcoded to UTF-8) and disposes <paramref name="extras"/> after writing.</summary>
    /// <param name="linked">The linked UTF-16 text to transcode and write.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 text.</returns>
    public static IResult Text(LinkedTextUtf16 linked, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(linked, TextContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="owned"/> segments (transcoded to UTF-8) and disposes all after writing.</summary>
    /// <param name="owned">The owned linked UTF-16 text to transcode, write, and dispose.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 text.</returns>
    public static IResult Text(OwnedLinkedTextUtf16 owned, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(owned.Data ?? throw new ObjectDisposedException(nameof(owned)), TextContentType, null);
        result.SetOwnedLinkedUtf16(owned);
        result.AddOwnedTexts(extras);
        return result;
    }

    // ── Json ────────────────────────────────────────────────────────────────

    /// <summary>Writes <paramref name="text"/> as UTF-8 JSON to the response body.</summary>
    /// <param name="text">The text content to write as JSON.</param>
    /// <param name="statusCode">The HTTP status code, or <c>null</c> to use the default.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 JSON.</returns>
    /// <example>
    /// <code>
    /// app.MapGet("/data", () => GlotResults.Json(jsonText));
    /// </code>
    /// </example>
    public static IResult Json(Text text, int? statusCode = null)
        => new Utf8TextResult(text, JsonContentType, statusCode);

    /// <summary>Writes <paramref name="owned"/> as UTF-8 JSON and disposes the pooled buffer after writing.</summary>
    /// <param name="owned">The owned text content to write as JSON.</param>
    /// <param name="statusCode">The HTTP status code, or <c>null</c> to use the default.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 JSON.</returns>
    public static IResult Json(OwnedText owned, int? statusCode = null)
    {
        var result = new Utf8TextResult(owned.Text, JsonContentType, statusCode);
        result.AddOwnedText(owned);
        return result;
    }

    /// <summary>Writes <paramref name="text"/> as UTF-8 JSON and disposes <paramref name="extras"/> after writing.</summary>
    /// <param name="text">The text content to write as JSON.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 JSON.</returns>
    public static IResult Json(Text text, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(text, JsonContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="linked"/> segments as UTF-8 JSON and disposes <paramref name="extras"/> after writing.</summary>
    /// <param name="linked">The linked UTF-8 text to write as JSON.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 JSON.</returns>
    public static IResult Json(LinkedTextUtf8 linked, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(linked, JsonContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="owned"/> segments as UTF-8 JSON and disposes all after writing.</summary>
    /// <param name="owned">The owned linked UTF-8 text to write as JSON and dispose.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 JSON.</returns>
    public static IResult Json(OwnedLinkedTextUtf8 owned, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(owned.Data ?? throw new ObjectDisposedException(nameof(owned)), JsonContentType, null);
        result.SetOwnedLinkedUtf8(owned);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="linked"/> segments (transcoded to UTF-8) as JSON and disposes <paramref name="extras"/> after writing.</summary>
    /// <param name="linked">The linked UTF-16 text to transcode and write as JSON.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 JSON.</returns>
    public static IResult Json(LinkedTextUtf16 linked, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(linked, JsonContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="owned"/> segments (transcoded to UTF-8) as JSON and disposes all after writing.</summary>
    /// <param name="owned">The owned linked UTF-16 text to transcode, write as JSON, and dispose.</param>
    /// <param name="extras">Owned text values to dispose after writing.</param>
    /// <returns>An <see cref="IResult"/> that writes the content as UTF-8 JSON.</returns>
    public static IResult Json(OwnedLinkedTextUtf16 owned, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(owned.Data ?? throw new ObjectDisposedException(nameof(owned)), JsonContentType, null);
        result.SetOwnedLinkedUtf16(owned);
        result.AddOwnedTexts(extras);
        return result;
    }
}
