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

        // Assert -- owned is disposed after execution
        await Assert.That(owned.IsDisposed).IsTrue();
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
        await Assert.That(extra.IsEmpty).IsTrue();
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
