using Parser.ParserModel;
using Parser.Token;
using System.Text;

namespace Parser.Markdown;


public class MarkdownSource
{
    public (Rune character, SourceFlag flag)[] Source { get; }


    public MarkdownSource((Rune, SourceFlag)[] source)
    {
        Source = source;
    }

    public MarkdownSource(int maxLength)
    {
        Source = new (Rune, SourceFlag)[maxLength];
    }
}
