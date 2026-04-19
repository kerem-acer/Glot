namespace Glot;

public readonly partial struct Text
{
    delegate TResult BuilderFinisher<out TResult>(ref TextBuilder builder);

    static Text FinishAsText(ref TextBuilder b) => b.ToText();
    static OwnedText FinishAsOwnedText(ref TextBuilder b) => b.ToOwnedText();
}
