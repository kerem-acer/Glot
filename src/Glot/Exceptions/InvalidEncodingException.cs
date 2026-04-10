namespace Glot;

sealed class InvalidEncodingException(TextEncoding encoding)
    : ArgumentOutOfRangeException(nameof(encoding), encoding, $"Unsupported encoding: {encoding}");
