using System.Text;

using Extensions.String;

namespace Parser.Token;


public class Tokenizer
{
    private static readonly Rune[] _ignorableRunes = "\n\t ".ToRuneArray();

    public static readonly Dictionary<char, Func<string, int, IToken>> StringCommentTokenizer = new()
    {
        ['"'] = (source, start) =>
        {
            var slice = Marker(source, start, 1,
                (r, i) => r[i] == '"' && r[i - 1] != '\\'
            );

            var token = new StringToken(slice, SourceFlag.DoubleQuoteString);

            return token;
        },
        ['\''] = (source, start) =>
        {
            var slice = Marker(source, start, 1,
                (r, i) => r[i] == '\'' && r[i - 1] != '\\'
            );

            var token = new StringToken(slice, SourceFlag.SingleQuoteString);

            return token;
        },
        ['/'] = (source, start) =>
        {
            var nextChar = source[start + 1];

            if (nextChar != '*' && nextChar != '/')
                return SpecialToken.DIVISION;

            var marker = Comment![nextChar];
            var token = marker(source, start);

            return token;
        },
    };

    public static readonly Dictionary<char, Func<string, int, IToken>> Comment = new()
    {
        ['/'] = (source, start) =>
        {
            var slice = Marker(source, start, 2,
                (r, i) => r[i] == '\n'
            );

            var token = new CommentToken(slice, SourceFlag.SingleLineComment);

            return token;
        },
        ['*'] = (source, start) =>
        {
            var slice = Marker(source, start, 2,
                (r, i) => source[i] == '/' && source[i - 1] == '*'
            );

            var token = new CommentToken(slice, SourceFlag.MultiLineComment);

            return token;
        }
    };

    private static string Marker(string source, int start, int offset, Func<string, int, bool> forBreakCondition)
    {
        var length = offset;

        for (int i = start + offset; i < source.Length; i++)
        {
            length++;
            if (forBreakCondition(source, i))
                break;
        }

        var end = start + length;
        var slice = source[start..end];

        return slice;
    }


    public SourceTokens Tokenize(Rune[] markdown)
    {
        var tokens = new SourceTokens();

        for (int i = 0; i < markdown.Length; i++)
        {
            var currentChar = markdown[i];

            Func<Rune[], int, IToken>? tokenizer;
            if (StringCommentTokenizer.TryGetValue(currentChar, out tokenizer))
            {
                var token = tokenizer(markdown, i);
                tokens.Add(token);
            }


        }
        
        return tokens;
    }
}
