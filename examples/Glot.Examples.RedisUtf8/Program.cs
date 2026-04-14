// Redis UTF-8 Native Cache
//
// Traditional pipeline with System.String:
//   UTF-8 JSON --> string (UTF-16) --> Encoding.GetBytes (UTF-8) --> Redis --> GetString (UTF-16) --> UTF-8 JSON
//   Two transcodes per field on write, two on read.
//
// With Glot:
//   UTF-8 JSON --> Text (UTF-8) --> SerializeToUtf8Bytes --> Redis --> Deserialize<byte[]> --> Text (UTF-8) --> UTF-8 JSON
//   Data stays UTF-8 the entire way. No UTF-16 intermediate at any point.
//
// Prerequisites:
//   docker run -d --name redis -p 6379:6379 redis:alpine
//
// Usage:
//   dotnet run
//
//   # Store a profile
//   curl -X POST http://localhost:5000/profiles/42 \
//     -H "Content-Type: application/json" \
//     -d '{"firstName":"Jane","lastName":"Doe","email":"jane@example.com","bio":"Software engineer who loves distributed systems","company":"Acme Inc","website":"https://jane.dev"}'
//
//   # Retrieve it
//   curl http://localhost:5000/profiles/42
//
//   # Search across profiles (demonstrates cross-encoding equality)
//   curl "http://localhost:5000/profiles/42/contains?q=distributed"

using System.Runtime.InteropServices;
using System.Text.Json;
using Glot;
using Glot.AspNetCore;
using Glot.SystemTextJson;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Separate options instance for manual serialization (SerializeToUtf8Bytes / Deserialize).
// AddGlot() only configures the HTTP pipeline options used by minimal API binding.
var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Converters = { new TextJsonConverter() }
};

builder.Services.AddGlot();

var redis = await ConnectionMultiplexer.ConnectAsync(
    builder.Configuration["Redis"] ?? "localhost:6379");
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

var app = builder.Build();

// Store a profile as UTF-8 bytes in Redis.
// JsonSerializer.SerializeToUtf8Bytes writes Text fields as UTF-8 directly (via TextJsonConverter).
// Redis stores the raw bytes -- no string intermediate, no UTF-16 transcode.
app.MapPost("/profiles/{id}", async (Text id, UserProfile profile, IConnectionMultiplexer mux) =>
{
    var db = mux.GetDatabase();

    // Build the Redis key as UTF-8 using the interpolated string handler.
    // "profile:" is appended as a UTF-8 literal, id (string) is transcoded once.
    // The pooled builder writes directly to a rented buffer -- no intermediate string.
    using var key = OwnedText.Create(TextEncoding.Utf8, $"profile:{id}")!;

    // Serialize the entire object to UTF-8 JSON bytes.
    // TextJsonConverter.Write outputs Text.Bytes directly for UTF-8 backed text.
    var bytes = JsonSerializer.SerializeToUtf8Bytes(profile, jsonOptions);

    // RedisValue accepts byte[] directly -- the JSON bytes go to Redis as-is.
    await db.StringSetAsync(RedisKeyHelper.ToRedisKey(key), bytes);

    return Results.Created($"/profiles/{id.ToString()}", null);
});

// Retrieve a profile -- UTF-8 bytes from Redis deserialized back to Text.
app.MapGet("/profiles/{id}", async (Text id, IConnectionMultiplexer mux) =>
{
    var db = mux.GetDatabase();

    using var key = OwnedText.Create(TextEncoding.Utf8, $"profile:{id}")!;
    RedisValue value = await db.StringGetAsync(RedisKeyHelper.ToRedisKey(key));

    if (value.IsNull)
    {
        return Results.NotFound();
    }

    // Deserialize UTF-8 bytes directly into Text fields -- no string allocation.
    var profile = JsonSerializer.Deserialize<UserProfile>((byte[])value!, jsonOptions);

    // Write the UTF-8 JSON directly to the response body via Results.Json.
    var json = Text.FromUtf8(JsonSerializer.SerializeToUtf8Bytes(profile, jsonOptions));
    return Results.Json(json);
});

// Demonstrate Text.Contains for searching within stored profiles.
// The query arrives as a UTF-16 string (from the query parameter), but Text.Contains
// handles cross-encoding comparison -- the stored UTF-8 data is searched without transcoding it.
app.MapGet("/profiles/{id}/contains", async (Text id, Text q, IConnectionMultiplexer mux) =>
{
    var db = mux.GetDatabase();

    using var key = OwnedText.Create(TextEncoding.Utf8, $"profile:{id}")!;
    RedisValue value = await db.StringGetAsync(RedisKeyHelper.ToRedisKey(key));

    if (value.IsNull)
    {
        return Results.NotFound();
    }

    var profile = JsonSerializer.Deserialize<UserProfile>((byte[])value!, jsonOptions);

    // Both profile.Bio and q are Text -- Bio is UTF-8 backed (from JSON),
    // q is UTF-16 backed (from query string via IParsable<Text>).
    // Text.Contains handles the cross-encoding search using the ASCII fast path.
    var found = profile!.Bio.Contains(q);

    return Results.Json(new { query = q.ToString(), found });
});

app.Run();

// All fields are Text -- stored as UTF-8 in memory and in Redis.
record UserProfile(
    Text FirstName,
    Text LastName,
    Text Email,
    Text Bio,
    Text Company,
    Text Website);

// Extracts the backing byte[] from a UTF-8 Text without copying when the array is exact-sized.
// Falls back to ToArray() for pooled/oversized buffers (e.g. OwnedText from ArrayPool).
static class RedisKeyHelper
{
    public static RedisKey ToRedisKey(Text text)
    {
        MemoryMarshal.TryGetArray(text.AsUtf8Memory(), out var segment);
        return segment is { Offset: 0 } && segment.Count == segment.Array!.Length
            ? (RedisKey)segment.Array
            : (RedisKey)text.AsUtf8Memory().ToArray();
    }

    public static RedisKey ToRedisKey(OwnedText owned) => ToRedisKey(owned.Text);
}
