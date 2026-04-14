using System.Buffers;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Glot.AspNetCore;

/// <summary>
/// An <see cref="IResult"/> that writes <see cref="Text"/>, <see cref="LinkedTextUtf8"/>,
/// or <see cref="LinkedTextUtf16"/> directly as UTF-8 bytes to the response body.
/// Zero-copy when the content is already UTF-8 backed.
/// Disposes any owned resources after the response is written — zero boxing.
/// </summary>
public sealed class Utf8TextResult : IResult
{
    readonly Text _text;
    readonly LinkedTextUtf8? _linkedUtf8;
    readonly LinkedTextUtf16? _linkedUtf16;
    readonly string _contentType;
    readonly int? _statusCode;

    OwnedLinkedTextUtf8? _ownedLinkedUtf8;
    OwnedLinkedTextUtf16? _ownedLinkedUtf16;
    OwnedText? _owned0, _owned1, _owned2, _owned3;
    int _ownedTextCount;

    internal Utf8TextResult(Text text, string contentType, int? statusCode)
    {
        _text = text;
        _contentType = contentType;
        _statusCode = statusCode;
    }

    internal Utf8TextResult(LinkedTextUtf8 linked, string contentType, int? statusCode)
    {
        _linkedUtf8 = linked;
        _contentType = contentType;
        _statusCode = statusCode;
    }

    internal Utf8TextResult(LinkedTextUtf16 linked, string contentType, int? statusCode)
    {
        _linkedUtf16 = linked;
        _contentType = contentType;
        _statusCode = statusCode;
    }

    internal void SetOwnedLinkedUtf8(OwnedLinkedTextUtf8 owned) => _ownedLinkedUtf8 = owned;

    internal void SetOwnedLinkedUtf16(OwnedLinkedTextUtf16 owned) => _ownedLinkedUtf16 = owned;

    internal void AddOwnedText(OwnedText owned)
    {
        switch (_ownedTextCount)
        {
            case 0: _owned0 = owned; break;
            case 1: _owned1 = owned; break;
            case 2: _owned2 = owned; break;
            case 3: _owned3 = owned; break;
            default:
                // > 4 OwnedText extras is practically unreachable;
                // use the last slot as a safety net.
                _owned3 = owned;
                return;
        }

        _ownedTextCount++;
    }

    internal void AddOwnedTexts(OwnedText[] extras)
    {
        foreach (var owned in extras)
        {
            AddOwnedText(owned);
        }
    }

    /// <inheritdoc/>
    public async Task ExecuteAsync(HttpContext httpContext)
    {
        try
        {
            if (_statusCode.HasValue)
            {
                httpContext.Response.StatusCode = _statusCode.Value;
            }

            httpContext.Response.ContentType = _contentType;

            if (_linkedUtf8 is not null)
            {
                await WriteLinkedUtf8Async(httpContext);
                return;
            }

            if (_linkedUtf16 is not null)
            {
                await WriteLinkedUtf16Async(httpContext);
                return;
            }

            if (_text.IsEmpty)
            {
                return;
            }

            // Fast path: UTF-8 backed text — write the backing memory directly, zero copy.
            if (_text.TryGetUtf8Memory(out var memory))
            {
                httpContext.Response.ContentLength = memory.Length;
                await httpContext.Response.Body.WriteAsync(memory);
                return;
            }

            // Other encodings: transcode to a pooled UTF-8 buffer, then write.
            var maxBytes = _text.RuneLength * 4;
            var buffer = ArrayPool<byte>.Shared.Rent(maxBytes);
            try
            {
                var written = _text.EncodeToUtf8(buffer);
                httpContext.Response.ContentLength = written;
                await httpContext.Response.Body.WriteAsync(buffer.AsMemory(0, written));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
        finally
        {
            DisposeAll();
        }
    }

    async Task WriteLinkedUtf8Async(HttpContext httpContext)
    {
        httpContext.Response.ContentLength = _linkedUtf8!.Length;

        foreach (var segment in _linkedUtf8.EnumerateSegments())
        {
            await httpContext.Response.Body.WriteAsync(segment);
        }
    }

    async Task WriteLinkedUtf16Async(HttpContext httpContext)
    {
        var body = httpContext.Response.Body;

        foreach (var segment in _linkedUtf16!.EnumerateSegments())
        {
            var chars = segment.Span;
            var maxBytes = Encoding.UTF8.GetMaxByteCount(chars.Length);
            var buffer = ArrayPool<byte>.Shared.Rent(maxBytes);
            try
            {
                var written = Encoding.UTF8.GetBytes(chars, buffer);
                await body.WriteAsync(buffer.AsMemory(0, written));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }

    void DisposeAll()
    {
        _ownedLinkedUtf8?.Dispose();
        _ownedLinkedUtf16?.Dispose();

        _owned0?.Dispose();
        _owned1?.Dispose();
        _owned2?.Dispose();
        _owned3?.Dispose();
    }
}
