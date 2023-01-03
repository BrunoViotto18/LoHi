using Ardalis.GuardClauses;
using Ardalis.SmartEnum;
using System.Text;

using Extensions.String;

namespace Parser.Token;


public sealed class SpecialToken : SmartEnum<SpecialToken>, IToken
{
    private static int NextIndex { get; set; } = 0;

    // Namespaces
    public static readonly SpecialToken NAMESPACE = new("namespace");
    public static readonly SpecialToken USING = new("using");

    // Punctuation
    public static readonly SpecialToken EXCLAMATION_MARK = new("!");
    public static readonly SpecialToken QUESTION_MARK = new("?");
    public static readonly SpecialToken SEMICOLON = new(";");
    public static readonly SpecialToken ACCESS = new(".");
    public static readonly SpecialToken COLON = new(":");
    public static readonly SpecialToken COMMA = new(",");
    public static readonly SpecialToken AT = new("@");

    // Access modifiers
    public static readonly SpecialToken FINAL = new("final");
    public static readonly SpecialToken PUBLIC = new("public");
    public static readonly SpecialToken PRIVATE = new("private");
    public static readonly SpecialToken INTERNAL = new("internal");
    public static readonly SpecialToken PROTECTED = new("protected");

    // Abstract data types
    public static readonly SpecialToken INTERFACE = new("interface");
    public static readonly SpecialToken STRUCT = new("struct");
    public static readonly SpecialToken CLASS = new("class");
    public static readonly SpecialToken ENUM = new("enum");

    // Abstract data type modifiers
    public static readonly SpecialToken ABSTRACT = new("abstract");
    public static readonly SpecialToken UNSEALED = new("unsealed");
    public static readonly SpecialToken READONLY = new("readonly");
    public static readonly SpecialToken ASSEMBLY = new("assembly");
    public static readonly SpecialToken UNSAFE = new("unsafe");
    public static readonly SpecialToken STATIC = new("static");
    public static readonly SpecialToken CONST = new("const");

    // Properties
    public static readonly SpecialToken GET = new("get");
    public static readonly SpecialToken SET = new("set");
    public static readonly SpecialToken INIT = new("init");

    // Open-Close characters
    public static readonly SpecialToken OPEN_BRACKET = new("{");
    public static readonly SpecialToken CLOSE_BRACKET = new("}");
    public static readonly SpecialToken OPEN_SQUARE_BRACKET = new("[");
    public static readonly SpecialToken CLOSE_SQUARE_BRACKET = new("]");
    public static readonly SpecialToken OPEN_PARENTHESES = new("(");
    public static readonly SpecialToken CLOSE_PARENTHESES = new(")");
    public static readonly SpecialToken OPEN_ANGULAR_BRACKET = new("<");
    public static readonly SpecialToken CLOSE_ANGULAR_BRACKET = new(">");

    // Types
    public static readonly SpecialToken VOID = new("void");
    public static readonly SpecialToken INT = new("int");
    public static readonly SpecialToken CHAR = new("char");
    public static readonly SpecialToken STRING = new("string");
    public static readonly SpecialToken BOOL = new("bool");
    public static readonly SpecialToken NEW = new("new");

    // Operators
    public static readonly SpecialToken ADDTION = new("+");
    public static readonly SpecialToken SUBTRACTION = new("-");
    public static readonly SpecialToken DIVISION = new("/");
    public static readonly SpecialToken MULTIPLICATION = new("*");
    public static readonly SpecialToken MODULO = new("%");
    public static readonly SpecialToken FLOOR_DIVISION = new("/-");
    public static readonly SpecialToken EXPONENTIATION = new("*+");
    public static readonly SpecialToken ADDRESS_OF = new("&");
    public static readonly SpecialToken POINTER = new("*");
    public static readonly SpecialToken INCREMENT = new("++");
    public static readonly SpecialToken DECREMENT = new("--");

    // Logical operators
    public static readonly SpecialToken EQUAL = new("==");
    public static readonly SpecialToken DIFFERENT = new("!=");
    public static readonly SpecialToken GREATER = new(">");
    public static readonly SpecialToken LESSER = new("<");
    public static readonly SpecialToken GREATER_EQUAL = new(">=");
    public static readonly SpecialToken LESSER_EQUAL = new("<=");
    public static readonly SpecialToken AND = new("&&");
    public static readonly SpecialToken OR = new("||");
    public static readonly SpecialToken NOT = new("!");
    public static readonly SpecialToken NAND = new("!&");
    public static readonly SpecialToken NOR = new("!|");

    // Binary operators
    public static readonly SpecialToken BINARY_AND = new("&");
    public static readonly SpecialToken BINARY_OR = new("|");
    public static readonly SpecialToken BINARY_XOR = new("^");
    public static readonly SpecialToken BINARY_NOT = new("~");
    public static readonly SpecialToken BINARY_NAND = new("~&");
    public static readonly SpecialToken BINARY_NOR = new("~|");
    public static readonly SpecialToken BINARY_XNOR = new("~^");

    // Attribution operators
    public static readonly SpecialToken ATTRIBUTION = new("=");
    public static readonly SpecialToken ATTRIBUTION_ADDITION = new("+=");
    public static readonly SpecialToken ATTRIBUTION_SUBTRACTION = new("-=");
    public static readonly SpecialToken ATTRIBUTION_DIVISION = new("/=");
    public static readonly SpecialToken ATTRIBUTION_MULTIPLICATION = new("*=");
    public static readonly SpecialToken ATTRIBUTION_MODULO = new("%=");
    public static readonly SpecialToken ATTRIBUTION_FLOOR_DIVISION = new("/-=");
    public static readonly SpecialToken ATTRIBUTION_EXPONENTIATION = new("*+=");
    public static readonly SpecialToken ATTRIBUTION_AND = new("&&=");
    public static readonly SpecialToken ATTRIBUTION_OR = new("||=");
    public static readonly SpecialToken ATTRIBUTION_BINARY_AND = new("&=");
    public static readonly SpecialToken ATTRIBUTION_BINARY_OR = new("|=");


    public Rune[] Text { get; init; }
    public SourceFlag Flag { get; init; }


    private SpecialToken(string name) : base(name, NextIndex++)
    {
        Guard.Against.NullOrWhiteSpace(name);

        Text = name.ToRuneArray();
        Flag = SourceFlag.Code;
    }


    public static int GetMatchesCount(Rune[] source, int offset, int length)
    {
        return List.Count(token =>
        token.Text
        .AsSpan()
        .StartsWith(
            source.AsSpan()
            .Slice(offset, length)
        ));
    }
}
