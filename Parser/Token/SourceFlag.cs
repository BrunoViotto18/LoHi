namespace Parser.Token;


public enum SourceFlag : byte
{
    Code,
    SingleQuoteString,
    DoubleQuoteString,
    MultiLineComment,
    SingleLineComment
}
