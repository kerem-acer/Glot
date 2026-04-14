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
    public static IResult Text(Text text, string contentType = TextContentType, int? statusCode = null)
        => new Utf8TextResult(text, contentType, statusCode);

    /// <summary>Writes <paramref name="owned"/> as UTF-8 and disposes the pooled buffer after writing.</summary>
    public static IResult Text(OwnedText owned, string contentType = TextContentType, int? statusCode = null)
    {
        var result = new Utf8TextResult(owned.Text, contentType, statusCode);
        result.AddOwnedText(owned);
        return result;
    }

    /// <summary>Writes <paramref name="text"/> as UTF-8 and disposes <paramref name="extras"/> after writing.</summary>
    public static IResult Text(Text text, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(text, TextContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="linked"/> segments as UTF-8 and disposes <paramref name="extras"/> after writing.</summary>
    public static IResult Text(LinkedTextUtf8 linked, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(linked, TextContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="owned"/> segments as UTF-8 and disposes all after writing.</summary>
    public static IResult Text(OwnedLinkedTextUtf8 owned, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(owned.Data ?? throw new ObjectDisposedException(nameof(owned)), TextContentType, null);
        result.SetOwnedLinkedUtf8(owned);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="linked"/> segments (transcoded to UTF-8) and disposes <paramref name="extras"/> after writing.</summary>
    public static IResult Text(LinkedTextUtf16 linked, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(linked, TextContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="owned"/> segments (transcoded to UTF-8) and disposes all after writing.</summary>
    public static IResult Text(OwnedLinkedTextUtf16 owned, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(owned.Data ?? throw new ObjectDisposedException(nameof(owned)), TextContentType, null);
        result.SetOwnedLinkedUtf16(owned);
        result.AddOwnedTexts(extras);
        return result;
    }

    // ── Json ────────────────────────────────────────────────────────────────

    /// <summary>Writes <paramref name="text"/> as UTF-8 JSON to the response body.</summary>
    public static IResult Json(Text text, int? statusCode = null)
        => new Utf8TextResult(text, JsonContentType, statusCode);

    /// <summary>Writes <paramref name="owned"/> as UTF-8 JSON and disposes the pooled buffer after writing.</summary>
    public static IResult Json(OwnedText owned, int? statusCode = null)
    {
        var result = new Utf8TextResult(owned.Text, JsonContentType, statusCode);
        result.AddOwnedText(owned);
        return result;
    }

    /// <summary>Writes <paramref name="text"/> as UTF-8 JSON and disposes <paramref name="extras"/> after writing.</summary>
    public static IResult Json(Text text, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(text, JsonContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="linked"/> segments as UTF-8 JSON and disposes <paramref name="extras"/> after writing.</summary>
    public static IResult Json(LinkedTextUtf8 linked, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(linked, JsonContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="owned"/> segments as UTF-8 JSON and disposes all after writing.</summary>
    public static IResult Json(OwnedLinkedTextUtf8 owned, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(owned.Data ?? throw new ObjectDisposedException(nameof(owned)), JsonContentType, null);
        result.SetOwnedLinkedUtf8(owned);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="linked"/> segments (transcoded to UTF-8) as JSON and disposes <paramref name="extras"/> after writing.</summary>
    public static IResult Json(LinkedTextUtf16 linked, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(linked, JsonContentType, null);
        result.AddOwnedTexts(extras);
        return result;
    }

    /// <summary>Writes <paramref name="owned"/> segments (transcoded to UTF-8) as JSON and disposes all after writing.</summary>
    public static IResult Json(OwnedLinkedTextUtf16 owned, params OwnedText[] extras)
    {
        var result = new Utf8TextResult(owned.Data ?? throw new ObjectDisposedException(nameof(owned)), JsonContentType, null);
        result.SetOwnedLinkedUtf16(owned);
        result.AddOwnedTexts(extras);
        return result;
    }
}
