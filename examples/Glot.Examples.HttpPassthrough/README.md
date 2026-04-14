# Event Ingestion Pipeline

A webhook event processor that demonstrates Glot's zero-transcode architecture.

## The problem

With `System.String`, every JSON field is transcoded UTF-8 to UTF-16 on deserialize, processed as UTF-16, then transcoded back to UTF-8 for the response. Every `Contains`, `Replace`, and `Split` allocates intermediate strings.

```
HTTP body (UTF-8) → string (UTF-16) → Process → string (UTF-16) → HTTP body (UTF-8)
```

## The solution

With Glot, data stays UTF-8 the entire way. Cross-encoding operations (UTF-16 query params vs UTF-8 stored data) work transparently without transcoding.

```
HTTP body (UTF-8) → Text (UTF-8) → Process → Text (UTF-8) → HTTP body (UTF-8)
```

## What each endpoint demonstrates

### `POST /events` — Zero-transcode JSON deserialization

`AddGlot()` registers `TextJsonConverter` with the HTTP JSON options. The framework deserializes `Text` fields directly from UTF-8 JSON bytes — no `System.String` intermediate. `Results.Ok(e)` serializes them back as raw UTF-8.

### `GET /events/search?q=cafe&level=error` — Cross-encoding search

Query params arrive as UTF-16 (URL percent-decoding produces `System.String`). Stored events are UTF-8 (from JSON deserialization). `Text.Contains` compares across encodings by iterating runes — no transcoding of the stored data. The `Text?` parameter type binds directly from the query string.

### `GET /events/by-tag/{tag}` — Zero-alloc Split + Trim

`Split` returns a `SplitEnumerator` (ref struct) that yields `TextSpan` slices over the original UTF-8 bytes. No `string[]` allocated, no substrings created. `Trim()` and `==` operate on the stack-only `TextSpan` without allocating.

### `POST /events/{id}/sanitize` — Linked text with ownership transfer

`ReplacePooled` returns a pooled `OwnedText`. The `OwnedLinkedTextUtf8.Create` factory with `OwnedTextHandling.TakeOwnership` detaches the buffer from the `OwnedText` into the linked text's segment — zero copy. The `using` on `sanitized` ensures the wrapper returns to the `OwnedText` pool after the buffer is transferred. `GlotResults.Text` takes disposal responsibility for the linked text.

### `GET /events/{id}` — Contiguous pooled interpolation

`OwnedText.Create` builds a contiguous UTF-8 buffer from an interpolated string. `Text` holes copy their backing bytes directly. `int` values (`RuneLength`) are formatted into the buffer via `IUtf8SpanFormattable` — no boxing, no `ToString`. `GlotResults.Text` takes disposal responsibility.

### `GET /events/export` — Streaming NDJSON

Each event is serialized to a pooled `OwnedText` via `SerializeToUtf8OwnedText`. `CopyToAsync` writes the backing `byte[]` directly to the response stream — zero copy for UTF-8 backed text. The `using` ensures each buffer returns to the pool after writing.

## Run

```bash
dotnet run
```

## Try it

```bash
# Ingest events
curl -s -X POST http://localhost:5000/events \
  -H "Content-Type: application/json" \
  -d '{"source":"stripe.payment","level":"error","message":"Card declined for cafe-rewards@merchant.com","tags":"billing, europe, cafe"}'

curl -s -X POST http://localhost:5000/events \
  -H "Content-Type: application/json" \
  -d '{"source":"stripe.payment","level":"info","message":"Payment succeeded for jose@example.com","tags":"billing, latam"}'

curl -s -X POST http://localhost:5000/events \
  -H "Content-Type: application/json" \
  -d '{"source":"github.push","level":"info","message":"Push to main by Maria Garcia","tags":"ci, deploy"}'

# Cross-encoding search
curl "http://localhost:5000/events/search?q=cafe"
curl "http://localhost:5000/events/search?source=stripe"
curl "http://localhost:5000/events/search?q=payment&level=error"

# Tag filtering
curl "http://localhost:5000/events/by-tag/europe"
curl "http://localhost:5000/events/by-tag/cafe"

# PII sanitization
curl -s -X POST http://localhost:5000/events/0/sanitize

# Enriched view
curl http://localhost:5000/events/0

# NDJSON export
curl http://localhost:5000/events/export
```
