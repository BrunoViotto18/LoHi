using Ardalis.SmartEnum;
using System.Runtime.CompilerServices;
using System.Text;

namespace Parser.Token;


public class CustomTokenManager
{
    public readonly List<IToken> Tokens = new();


    public unsafe IToken CreateCodeToken(string source, int start, int length)
    {
        var slice = source.AsSpan().Slice(start, length);

        fixed (char* ptr = slice)
        {
            var evil = ptr;
            if (Tokens.Count(token => token.Text.AsSpan() == new Span<char>(evil, length)) > 0)
                throw new TokenAlreadyExistsException(slice);
        }

        var token = new CustomToken(slice.ToString(), Tokens.Count, SourceFlag.Code);
        Tokens.Add(token);

        return token;
    }

    public IToken CreateStringToken()
    {

    }


    public static IToken FromText(Rune[] text)
    {

    }


    private class CustomToken : SmartEnum<CustomToken>, IToken
    {
        public string Text { get; init; }
        public SourceFlag Flag { get; init; }


        public CustomToken(string text, int index, SourceFlag flag) : base(text, index)
        {
            Text = text;
            Flag = flag;
        }
    }
}
