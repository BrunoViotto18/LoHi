using Ardalis.GuardClauses;

using Extensions.GuardClauses;
using System.Text;

namespace Parser.Token;


public class CommentToken : IToken
{
    private static readonly SourceFlag[] _allowedFlags = new SourceFlag[]
    {
        SourceFlag.SingleLineComment,
        SourceFlag.MultiLineComment
    };

    public Rune[] Text { get; init; }
    public SourceFlag Flag { get; init; }


    public CommentToken(Rune[] text, SourceFlag flag)
    {
        Guard.Against.NullOrWhiteSpace(text);
        Guard.Against.EnumValues(_allowedFlags, flag);

        Text = text;
        Flag = flag;
    }
}
