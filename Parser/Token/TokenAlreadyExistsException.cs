namespace Parser.Token;


public class TokenAlreadyExistsException : Exception
{
    public override string Message => $"O token com campo texto \"{Text}\" já existe.";
    public string Text { get; private set; }


    public TokenAlreadyExistsException(string text)
    {
        Text = text;
    }
    public TokenAlreadyExistsException(Span<char> text)
    {
        Text = text.ToString();
    }
    public TokenAlreadyExistsException(ReadOnlySpan<char> text)
    {
        Text = text.ToString();
    }
}
