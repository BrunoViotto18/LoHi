using System.Text;

namespace Parser.Token;


public interface IToken
{
    public string Text { get; init; }
    public SourceFlag Flag { get; init; }
}
