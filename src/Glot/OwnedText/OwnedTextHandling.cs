namespace Glot;

/// <summary>
/// Controls how <see cref="OwnedText"/> values are handled during linked text interpolation.
/// </summary>
public enum OwnedTextHandling
{
    /// <summary>
    /// Default. Copies the <see cref="OwnedText"/> data into the linked text's format buffer.
    /// Safe — no lifetime coupling. The caller can dispose the <see cref="OwnedText"/> immediately after.
    /// </summary>
    Copy,

    /// <summary>
    /// Zero-copy. Takes ownership of the <see cref="OwnedText"/>'s pooled buffer by detaching it.
    /// The linked text returns the buffer to the pool on dispose.
    /// The <see cref="OwnedText"/> becomes empty after this — disposing it is a no-op for the data buffer.
    /// </summary>
    TakeOwnership,

    /// <summary>
    /// Zero-copy. References the <see cref="OwnedText"/>'s buffer without taking ownership.
    /// The caller must keep the <see cref="OwnedText"/> alive until the linked text is disposed.
    /// </summary>
    Borrow,
}
