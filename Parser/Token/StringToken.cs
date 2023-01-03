using Ardalis.GuardClauses;

using Extensions.GuardClauses;
using System.Text;

namespace Parser.Token;


internal class StringToken : IToken
{
    private static readonly SourceFlag[] _allowedFlags = new SourceFlag[]
    {
        SourceFlag.SingleQuoteString,
        SourceFlag.DoubleQuoteString
    };

    public Rune[] Text { get; init; }
    public SourceFlag Flag { get; init; }


    public StringToken(Rune[] text, SourceFlag flag)
    {
        Guard.Against.NullOrWhiteSpace(text);
        Guard.Against.EnumValues(_allowedFlags, flag);

        Text = text;
        Flag = flag;
    }
}
