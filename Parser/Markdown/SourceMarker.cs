using static System.Text.Encoding;
using System.Text;

using Extensions.String;
using Extensions.Array;
using Parser.Token;

namespace Parser.Markdown;


public class SourceMarker
{
    public static readonly Dictionary<Rune, Func<Rune[], int, (int offset, SourceFlag flag)>> SourceCodeMarker = new()
    {
        [(Rune)'"'] = (source, start) =>
        {
            var count = Marker(source, start, 1,
                (r, i) => r[i] == (Rune)'"' && r[i - 1] != (Rune)'\\'
            );

            return (count, SourceFlag.DoubleQuoteString);
        },
        [(Rune)'\''] = (source, start) =>
        {
            var count = Marker(source, start, 1,
                (r, i) => r[i] == (Rune)'\'' && r[i - 1] != (Rune)'\\'
            );

            return (count, SourceFlag.SingleQuoteString);
        },
        [(Rune)'/'] = (source, start) =>
        {
            var nextChar = source[start + 1];

            if (nextChar != (Rune)'*' && nextChar != (Rune)'/')
                return (1, SourceFlag.Code);

            var marker = CommentMarker![nextChar];
            return marker(source, start);
        },
    };

    public static readonly Dictionary<Rune, Func<Rune[], int, (int offset, SourceFlag flag)>> CommentMarker = new()
    {
        [(Rune)'/'] = (source, start) =>
        {
            var count = Marker(source, start, 2,
                (r, i) => r[i] == (Rune)'\n'
            );

            return (count, SourceFlag.SingleLineComment);
        },
        [(Rune)'*'] = (source, start) =>
        {
            var count = Marker(source, start, 2,
                (r, i) => source[i] == (Rune)'/' && source[i - 1] == (Rune)'*'
            );

            return (count, SourceFlag.MultiLineComment);
        }
    };

    private static int Marker(Rune[] source, int start, int offset, Func<Rune[], int, bool> forBreakCondition)
    {
        var count = offset;

        for (int i = start + offset; i < source.Length; i++)
        {
            count++;
            if (forBreakCondition(source, i))
                break;
        }

        return count;
    }


    /// <summary>
    /// Opens the file at the specified path and gets its content. <para/>
    /// Author: Bruno Viotto <para/>
    /// Created: 28/12/2022 <para/>
    /// Updated: 29/12/2022
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <returns>Returns a string of the file's content.</returns>
    internal protected string GetRawSourceCode(string path)
    {
        var raw = File
            .ReadAllText(path, GetEncoding("iso-8859-1"))
            .Replace("\r", "");

        return raw;
    }

    /// <summary>
    /// Marks each character with a SourceFlag, such as Code, String, Comments, etc <para/>
    /// Author: Bruno Viotto <para/>
    /// Created: 28/12/2022 <para/>
    /// Updated: 30/12/2022
    /// </summary>
    /// <returns>Returns a (Rune, SourceFlag)[] where each character maps to a flag. </returns>
    internal protected MarkdownSource MarkdownSourceCode(string path)
    {
        var sourceFile = GetRawSourceCode(path)
            .Trim()
            .ToRuneArray();

        var markedDownSource = new (Rune character, SourceFlag flag)[sourceFile.Length];

        for (int i = 0; i < sourceFile.Length; i++)
        {
            if (SourceCodeMarker.TryGetValue(sourceFile[i], out Func<Rune[], int, (int, SourceFlag)>? marker))
            {
                var (length, flag) = marker(sourceFile, i);
                sourceFile.CopyTo(markedDownSource, i, i, length, r => (r, flag));
                i += length - 1;
                continue;
            }

            markedDownSource[i] = (sourceFile[i], SourceFlag.Code);
        }

        return new MarkdownSource(markedDownSource);
    }

    /// <summary>
    /// Parses the code at the specified path. <para/>
    /// Author: Bruno Viotto <para/>
    /// Created: 28/12/2022 <para/>
    /// Updated: 30/12/2022
    /// </summary>
    public void Parse(string path)
    {
        // TODO: Implement Logic here
    }
}
