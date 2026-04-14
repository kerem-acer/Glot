using Microsoft.AspNetCore.Http;

namespace Glot.AspNetCore.Tests;

public class Utf8TextResultTests
{
    [Test]
    public async Task ExecuteAsync_Utf8Text_WritesExactBytes()
    {
        // Arrange
        var text = Text.FromUtf8("hello"u8);
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("hello"u8.ToArray());
    }

    [Test]
    public async Task ExecuteAsync_Utf16Text_TranscodesToUtf8()
    {
        // Arrange
        var text = Text.From("hello");
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("hello"u8.ToArray());
    }

    [Test]
    public async Task ExecuteAsync_Utf32Text_TranscodesToUtf8()
    {
        // Arrange
        var text = Text.FromUtf32(['h', 'i']);
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("hi"u8.ToArray());
    }

    [Test]
    public async Task ExecuteAsync_MultiByte_WritesCorrectUtf8()
    {
        // Arrange — "café" in UTF-8 is 5 bytes (c=1, a=1, f=1, é=2)
        const string value = "café";
        var text = Text.FromUtf8(System.Text.Encoding.UTF8.GetBytes(value));
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        var expected = System.Text.Encoding.UTF8.GetBytes(value);
        await Assert.That(body).IsEquivalentTo(expected);
    }

    [Test]
    public async Task ExecuteAsync_Utf16MultiByte_TranscodesCorrectly()
    {
        // Arrange — "café" from string (UTF-16) should transcode to valid UTF-8
        const string value = "café";
        var text = Text.From(value);
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        var expected = System.Text.Encoding.UTF8.GetBytes(value);
        await Assert.That(body).IsEquivalentTo(expected);
    }

    [Test]
    public async Task ExecuteAsync_EmptyText_WritesNothing()
    {
        // Arrange
        var text = Text.Empty;
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEmpty();
    }

    [Test]
    public async Task ExecuteAsync_SetsContentType()
    {
        // Arrange
        var text = Text.FromUtf8("test"u8);
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentType).IsEqualTo("text/plain; charset=utf-8");
    }

    [Test]
    public async Task ExecuteAsync_CustomContentType_SetsCorrectly()
    {
        // Arrange
        const string contentType = "application/xml; charset=utf-8";
        var text = Text.FromUtf8("<root/>"u8);
        var result = GlotResults.Text(text, contentType);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentType).IsEqualTo(contentType);
    }

    [Test]
    public async Task ExecuteAsync_CustomStatusCode_SetsCorrectly()
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

    [Test]
    public async Task ExecuteAsync_NoStatusCode_DoesNotOverrideDefault()
    {
        // Arrange
        var text = Text.FromUtf8("ok"u8);
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert — default is 200
        await Assert.That(httpContext.Response.StatusCode).IsEqualTo(200);
    }

    [Test]
    public async Task ExecuteAsync_Utf8Text_SetsContentLength()
    {
        // Arrange
        var text = Text.FromUtf8("hello"u8);
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentLength).IsEqualTo(5);
    }

    [Test]
    public async Task ExecuteAsync_Utf16Text_SetsContentLengthToUtf8Size()
    {
        // Arrange — "café" is 4 UTF-16 chars but 5 UTF-8 bytes
        var text = Text.From("café");
        var result = GlotResults.Text(text);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentLength).IsEqualTo(5);
    }

    [Test]
    public async Task ExecuteAsync_EmptyText_DoesNotSetContentLength()
    {
        // Arrange
        var result = GlotResults.Text(Text.Empty);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentLength).IsNull();
    }

    [Test]
    public async Task ExecuteAsync_OwnedText_WritesAndDisposes()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("hello"u8)!;
        var result = GlotResults.Text(owned);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("hello"u8.ToArray());
    }

    [Test]
    public async Task ExecuteAsync_TextWithExtraDisposable_WritesAndDisposes()
    {
        // Arrange
        var owned = OwnedText.FromUtf8("hello"u8)!;
        var result = GlotResults.Text(owned.Text, owned);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("hello"u8.ToArray());
    }

    [Test]
    public async Task ExecuteAsync_LinkedTextUtf8_WritesAllSegments()
    {
        // Arrange
        var seg1 = Text.FromUtf8("hello"u8);
        var seg2 = Text.FromUtf8(" world"u8);
        var linked = LinkedTextUtf8.Create(seg1, seg2);
        var result = GlotResults.Text(linked);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("hello world"u8.ToArray());
    }

    [Test]
    public async Task ExecuteAsync_LinkedTextUtf8_SetsContentLength()
    {
        // Arrange
        const int expectedLength = 11;
        var linked = LinkedTextUtf8.Create(Text.FromUtf8("hello"u8), Text.FromUtf8(" world"u8));
        var result = GlotResults.Text(linked);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        await Assert.That(httpContext.Response.ContentLength).IsEqualTo(expectedLength);
    }

    [Test]
    public async Task ExecuteAsync_OwnedLinkedTextUtf8_WritesAndDisposes()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Text.FromUtf8("ab"u8), Text.FromUtf8("cd"u8));
        var result = GlotResults.Text(owned);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("abcd"u8.ToArray());
    }

    [Test]
    public async Task ExecuteAsync_LinkedTextWithExtraDisposables_DisposesAll()
    {
        // Arrange
        var extra = OwnedText.FromUtf8("buf"u8)!;
        var linked = OwnedLinkedTextUtf8.Create(Text.FromUtf8("ok"u8), extra.Text);
        var result = GlotResults.Text(linked, extra);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("okbuf"u8.ToArray());
    }

    [Test]
    public async Task ExecuteAsync_LinkedTextUtf16_TranscodesAndWrites()
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
    public async Task ExecuteAsync_OwnedLinkedTextUtf8_DisposesAfterWrite()
    {
        // Arrange
        var owned = OwnedLinkedTextUtf8.Create(Text.FromUtf8("disposed"u8));
        var result = GlotResults.Text(owned);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert -- content written correctly
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("disposed"u8.ToArray());

        // Assert -- owned is disposed after execution
        await Assert.That(owned.IsDisposed).IsTrue();
    }

    [Test]
    public async Task ExecuteAsync_AddOwnedText_FourExtras_AllDisposed()
    {
        // Arrange
        var text = Text.FromUtf8("main"u8);
        var extra0 = OwnedText.FromUtf8("a"u8)!;
        var extra1 = OwnedText.FromUtf8("b"u8)!;
        var extra2 = OwnedText.FromUtf8("c"u8)!;
        var extra3 = OwnedText.FromUtf8("d"u8)!;
        var result = GlotResults.Text(text, extra0, extra1, extra2, extra3);
        var httpContext = CreateHttpContext();

        // Act
        await result.ExecuteAsync(httpContext);

        // Assert -- main content written
        var body = GetResponseBytes(httpContext);
        await Assert.That(body).IsEquivalentTo("main"u8.ToArray());

        // Assert -- all extras are disposed (IsEmpty after dispose)
        await Assert.That(extra0.IsEmpty).IsTrue();
        await Assert.That(extra1.IsEmpty).IsTrue();
        await Assert.That(extra2.IsEmpty).IsTrue();
        await Assert.That(extra3.IsEmpty).IsTrue();
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
