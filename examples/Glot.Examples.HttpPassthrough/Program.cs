using System.Text.Json;
using Glot;
using Glot.AspNetCore;
using Glot.SystemTextJson;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGlot();
var app = builder.Build();

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    Converters = { new TextJsonConverter() }
};

var events = new List<WebhookEvent>();

app.MapPost("/events", (WebhookEvent e) =>
{
    if (!e.Source.Contains("."))
    {
        return Results.BadRequest(new { error = "Source must be qualified (e.g. stripe.payment)" });
    }

    events.Add(e);
    return Results.Ok(e);
});

app.MapGet("/events/search", (Text? q, Text? source, Text? level) =>
{
    var results = events.AsEnumerable();

    if (q is { IsEmpty: false } query)
    {
        results = results.Where(e =>
            e.Source.Contains(query) ||
            e.Message.Contains(query) ||
            e.Tags.Contains(query));
    }

    if (source is { IsEmpty: false } src)
    {
        results = results.Where(e => e.Source.StartsWith(src));
    }

    if (level is { IsEmpty: false } lvl)
    {
        results = results.Where(e => e.Level == lvl);
    }

    return Results.Ok(results);
});

app.MapGet("/events/by-tag/{tag}", (Text tag) =>
{
    var matches = new List<WebhookEvent>();

    foreach (var e in events)
    {
        foreach (var segment in e.Tags.Split(","))
        {
            if (segment.Trim() == tag.AsSpan())
            {
                matches.Add(e);
                break;
            }
        }
    }

    return Results.Ok(matches);
});

app.MapPost("/events/{id}/sanitize", (int id) =>
{
    if (id < 0 || id >= events.Count)
    {
        return Results.NotFound();
    }

    var e = events[id];
    using var sanitized = e.Message.ReplacePooled("@", "[at]")!;

    var result = OwnedLinkedTextUtf8.Create(
        OwnedTextHandling.TakeOwnership,
        $"[{e.Level}] {e.Source}: {sanitized}");

    return GlotResults.Text(result);
});

app.MapGet("/events/{id}", (int id) =>
{
    if (id < 0 || id >= events.Count)
    {
        return Results.NotFound();
    }

    var e = events[id];

    var summary = OwnedText.Create(TextEncoding.Utf8,
        $"[{e.Level}] {e.Source}: {e.Message} (tags: {e.Tags}, {e.Message.RuneLength} runes)")!;

    return GlotResults.Text(summary);
});

app.MapGet("/events/export", async (HttpContext ctx) =>
{
    ctx.Response.ContentType = "application/x-ndjson; charset=utf-8";
    var newline = "\n"u8.ToArray();

    foreach (var e in events)
    {
        using var json = JsonSerializer.SerializeToUtf8OwnedText(e, jsonOptions);
        await json.Text.CopyToAsync(ctx.Response.Body);
        await ctx.Response.Body.WriteAsync(newline);
    }
});

app.Run();

record WebhookEvent(Text Source, Text Level, Text Message, Text Tags);
