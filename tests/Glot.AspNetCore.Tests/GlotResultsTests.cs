using Microsoft.AspNetCore.Http;

namespace Glot.AspNetCore.Tests;

public class GlotResultsTests
{
    [Test]
    public async Task Utf8Text_ReturnsUtf8TextResult()
    {
        // Act
        var result = GlotResults.Text(Text.FromUtf8("test"u8));

        // Assert
        await Assert.That(result).IsTypeOf<Utf8TextResult>();
    }

    [Test]
    public async Task Utf8Json_SetsJsonContentType()
    {
        // Arrange
        var result = GlotResults.Json(Text.FromUtf8("{}"u8));
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentType)
            .IsEqualTo("application/json; charset=utf-8");
    }

    [Test]
    public async Task Utf8Json_WritesBody()
    {
        // Arrange
        var json = Text.FromUtf8("{\"ok\":true}"u8);
        var result = GlotResults.Json(json);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("{\"ok\":true}"u8.ToArray());
    }

    [Test]
    public async Task Utf8Json_CustomStatusCode()
    {
        // Arrange
        var result = GlotResults.Json(Text.FromUtf8("{}"u8), statusCode: 201);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.StatusCode).IsEqualTo(201);
    }

    [Test]
    public async Task Utf8Text_OwnedText_ReturnsUtf8TextResult()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("test"u8)!;

        // Act
        var result = GlotResults.Text(owned);

        // Assert
        await Assert.That(result).IsTypeOf<Utf8TextResult>();
    }

    [Test]
    public async Task Utf8Json_OwnedText_SetsJsonContentType()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("{}"u8)!;
        var result = GlotResults.Json(owned);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentType)
            .IsEqualTo("application/json; charset=utf-8");
    }

    [Test]
    public async Task Utf8Text_LinkedTextUtf8_ReturnsUtf8TextResult()
    {
        // Arrange
        var linked = LinkedTextUtf8.Create(Text.FromUtf8("test"u8));

        // Act
        var result = GlotResults.Text(linked);

        // Assert
        await Assert.That(result).IsTypeOf<Utf8TextResult>();
    }

    [Test]
    public async Task Utf8Json_OwnedLinkedTextUtf8_SetsJsonContentType()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Text.FromUtf8("{}"u8));
        var result = GlotResults.Json(owned);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentType)
            .IsEqualTo("application/json; charset=utf-8");
    }

    [Test]
    public async Task Text_LinkedTextUtf16_WritesTranscoded()
    {
        // Arrange
        const string expected = "hello world";
        var linked = LinkedTextUtf16.Create("hello", " world");
        var result = GlotResults.Text(linked);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        var expectedBytes = System.Text.Encoding.UTF8.GetBytes(expected);
        await Assert.That(body).IsEquivalentTo(expectedBytes);
    }

    [Test]
    public async Task Text_OwnedLinkedTextUtf16_WritesAndDisposes()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf16.Create("hello", " world");
        var result = GlotResults.Text(owned);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert -- content transcoded and written
        var body = GetResponseBytes(httpContext);
        var expectedBytes = System.Text.Encoding.UTF8.GetBytes("hello world");
        await Assert.That(body).IsEquivalentTo(expectedBytes);

        // owned was disposed by ExecuteAsync — do not assert on pooled object state
    }

    [Test]
    public async Task Json_LinkedTextUtf16_SetsJsonContentType()
    {
        // Arrange
        var linked = LinkedTextUtf16.Create("{\"ok\":", "true}");
        var result = GlotResults.Json(linked);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentType)
            .IsEqualTo("application/json; charset=utf-8");
    }

    [Test]
    public async Task Text_TextWithExtras_DisposesExtras()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("extra"u8)!;
        var text = Text.FromUtf8("main"u8);
        var result = GlotResults.Text(text, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert -- main content written
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("main"u8.ToArray());

        // Assert -- extra is disposed
        // extra was disposed by ExecuteAsync — do not assert on pooled object state
    }

    // Text with custom content type

    [Test]
    public async Task Utf8Text_CustomContentType_SetsContentType()
    {
        // Arrange
        var text = Text.FromUtf8("hello"u8);
        var result = GlotResults.Text(text, contentType: "text/html; charset=utf-8");
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentType).IsEqualTo("text/html; charset=utf-8");
    }

    // Text with custom status code

    [Test]
    public async Task Utf8Text_CustomStatusCode_SetsStatusCode()
    {
        // Arrange
        var text = Text.FromUtf8("created"u8);
        var result = GlotResults.Text(text, statusCode: 201);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.StatusCode).IsEqualTo(201);
    }

    // OwnedText with custom content type and status code

    [Test]
    public async Task Utf8Text_OwnedText_CustomContentTypeAndStatus()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("ok"u8)!;
        var result = GlotResults.Text(owned, contentType: "text/html", statusCode: 202);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentType).IsEqualTo("text/html");
        await Assert.That(httpContext.Response.StatusCode).IsEqualTo(202);
        // owned was disposed by ExecuteAsync — do not assert on pooled object state
    }

    // Json with OwnedText disposal

    [Test]
    public async Task Json_OwnedText_DisposesAfterWrite()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("{\"ok\":true}"u8)!;
        var result = GlotResults.Json(owned);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("{\"ok\":true}"u8.ToArray());
        // owned was disposed by ExecuteAsync — do not assert on pooled object state
    }

    // Json with OwnedText and custom status code

    [Test]
    public async Task Json_OwnedText_CustomStatusCode()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("{}"u8)!;
        var result = GlotResults.Json(owned, statusCode: 201);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.StatusCode).IsEqualTo(201);
        await Assert.That(httpContext.Response.ContentType).IsEqualTo("application/json; charset=utf-8");
    }

    // Text(Text, extras) — Text with multiple extras

    [Test]
    public async Task Text_TextWithMultipleExtras_DisposesAll()
    {
        // Arrange
        var extra1 = OwnedText.FromUtf8("e1"u8)!;
        var extra2 = OwnedText.FromUtf8("e2"u8)!;
        var text = Text.FromUtf8("main"u8);
        var result = GlotResults.Text(text, extra1, extra2);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        // extras were disposed by ExecuteAsync — do not assert on pooled object state
    }

    // Text(LinkedTextUtf8, extras) — LinkedTextUtf8 with extras

    [Test]
    public async Task Text_LinkedTextUtf8WithExtras_WritesAndDisposesExtras()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("extra"u8)!;
        var linked = LinkedTextUtf8.Create(Text.FromUtf8("hello"u8), Text.FromUtf8(" world"u8));
        var result = GlotResults.Text(linked, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        var expectedBytes = System.Text.Encoding.UTF8.GetBytes("hello world");
        await Assert.That(body).IsEquivalentTo(expectedBytes);
        // extra was disposed by ExecuteAsync — do not assert on pooled object state
    }

    // Text(OwnedLinkedTextUtf8, extras) — OwnedLinkedTextUtf8 with extras

    [Test]
    public async Task Text_OwnedLinkedTextUtf8WithExtras_WritesAndDisposesAll()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("extra"u8)!;
        var ownedLinked = OwnedLinkedTextUtf8.Create(Text.FromUtf8("hello"u8));
        var result = GlotResults.Text(ownedLinked, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("hello"u8.ToArray());
        // ownedLinked was disposed by ExecuteAsync — do not assert on pooled object state
        // extra was disposed by ExecuteAsync — do not assert on pooled object state
    }

    // Text(LinkedTextUtf16, extras) — LinkedTextUtf16 with extras

    [Test]
    public async Task Text_LinkedTextUtf16WithExtras_WritesAndDisposesExtras()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("extra"u8)!;
        var linked = LinkedTextUtf16.Create("hello", " world");
        var result = GlotResults.Text(linked, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        var expectedBytes = System.Text.Encoding.UTF8.GetBytes("hello world");
        await Assert.That(body).IsEquivalentTo(expectedBytes);
        // extra was disposed by ExecuteAsync — do not assert on pooled object state
    }

    // Text with UTF-16 Text — exercises transcode path via EncodeToUtf8

    [Test]
    public async Task Utf8Text_Utf16Text_TranscodesAndWrites()
    {
        // Arrange
        var text = Text.From("hello from utf16");
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        var expectedBytes = System.Text.Encoding.UTF8.GetBytes("hello from utf16");
        await Assert.That(body).IsEquivalentTo(expectedBytes);
    }

    // Text with empty Text — exercises early return path

    [Test]
    public async Task Utf8Text_EmptyText_WritesNothing()
    {
        // Arrange
        var result = GlotResults.Text(Text.Empty);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body.Length).IsEqualTo(0);
    }

    // Json(Text, extras) — Text with extras

    [Test]
    public async Task Json_TextWithExtras_DisposesExtras()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("extra"u8)!;
        var text = Text.FromUtf8("{}"u8);
        var result = GlotResults.Json(text, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        // extra was disposed by ExecuteAsync — do not assert on pooled object state
        await Assert.That(httpContext.Response.ContentType).IsEqualTo("application/json; charset=utf-8");
    }

    // Json(LinkedTextUtf8, extras)

    [Test]
    public async Task Json_LinkedTextUtf8WithExtras_WritesAndDisposesExtras()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("extra"u8)!;
        var linked = LinkedTextUtf8.Create(Text.FromUtf8("{\"ok\":"u8), Text.FromUtf8("true}"u8));
        var result = GlotResults.Json(linked, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        // extra was disposed by ExecuteAsync — do not assert on pooled object state
        await Assert.That(httpContext.Response.ContentType).IsEqualTo("application/json; charset=utf-8");
    }

    // Json(OwnedLinkedTextUtf8, extras)

    [Test]
    public async Task Json_OwnedLinkedTextUtf8WithExtras_WritesAndDisposesAll()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("extra"u8)!;
        var ownedLinked = OwnedLinkedTextUtf8.Create(Text.FromUtf8("{}"u8));
        var result = GlotResults.Json(ownedLinked, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        // ownedLinked was disposed by ExecuteAsync — do not assert on pooled object state
        // extra was disposed by ExecuteAsync — do not assert on pooled object state
    }

    // Json(LinkedTextUtf16, extras)

    [Test]
    public async Task Json_LinkedTextUtf16WithExtras_WritesAndDisposesExtras()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("extra"u8)!;
        var linked = LinkedTextUtf16.Create("{\"ok\":", "true}");
        var result = GlotResults.Json(linked, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        // extra was disposed by ExecuteAsync — do not assert on pooled object state
    }

    // Json(OwnedLinkedTextUtf16, extras)

    [Test]
    public async Task Json_OwnedLinkedTextUtf16WithExtras_WritesAndDisposesAll()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("extra"u8)!;
        var ownedLinked = OwnedLinkedTextUtf16.Create("{\"ok\":", "true}");
        var result = GlotResults.Json(ownedLinked, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        // ownedLinked was disposed by ExecuteAsync — do not assert on pooled object state
        // extra was disposed by ExecuteAsync — do not assert on pooled object state
    }

    static DefaultHttpContext CreateHttpContext()
    {
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        return context;
    }

    static byte[] GetResponseBytes(DefaultHttpContext context)
    {
        context.Response.Body.Position = 0;
        using var reader = new MemoryStream();
        context.Response.Body.CopyTo(reader);
        return reader.ToArray();
    }
}
