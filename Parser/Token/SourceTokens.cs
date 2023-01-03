using Parser.Token;

namespace Parser.Token;


public class SourceTokens
{
    private readonly List<IToken> _tokens = new();

    public int Count => _tokens.Count;
    public IToken this[int index]
    {
        get => _tokens[index];
        set => _tokens[index] = value;
    }


    public void Add(IToken token)
        => _tokens.Add(token);

    public void Insert(int index, IToken token)
        => _tokens.Insert(index, token);

    public void RemoveFirst()
        => _tokens.Remove(_tokens.First());

    public void RemoveLast()
        => _tokens.Remove(_tokens.Last());

    public void Remove(IToken token)
        => _tokens.Remove(token);

    public void RemoveAt(int index)
        => _tokens.RemoveAt(index);

    public void SubstituteAt(int index, IToken[] tokens)
    {
        _tokens.RemoveAt(index);
        _tokens.InsertRange(index, tokens);
    }
}
