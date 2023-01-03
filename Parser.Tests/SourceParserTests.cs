using static System.Text.Encoding;

using Extensions.Array;
using Parser.Markdown;
using Parser.Token;

namespace Parser.Tests;


public class SourceParserTests
{
    private static readonly SourceMarker _sut = new SourceMarker();
    private static readonly string _projectPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..");
    private static readonly string _resourcesPath = Path.Combine(_projectPath, "Resources");
    private static readonly string _className = nameof(SourceMarker);


    [Fact]
    public void GetRawSourceCode_ShouldGetTheFileContent_WhenTheFilePathIsValid()
    {
        // Arrange
        var fileName = $"{nameof(TestFiles.ShouldGetTheFileContent_WhenTheFilePathIsValid)}.lohi";
        var filePath = Path.Combine(_resourcesPath, _className, nameof(_sut.GetRawSourceCode), fileName);
        var fileBytes = TestFiles.ShouldGetTheFileContent_WhenTheFilePathIsValid;
        var fileText = UTF8.GetString(fileBytes);

        // Act
        var text = _sut.GetRawSourceCode(filePath);

        // Assert
        text.Should().Be("This is a test.\n\nI repeat.\n\nThis ís à tést ñyaa~.");
    }

    [Fact]
    public void MarkdowSourceCode_ShouldReturnAListOfTupleLinkingEachCharacterToAFlag_WhenTheFilePathIsValid()
    {
        // Arrange
        var raw = "// This is a test */\n\"Another test\"\n/*\nYet\nanother\ntest\n*/\n\npublic class Test\n{\n\t'Yup, you guessed it, another test'\n}\n\nEOF // Whatever"
            .EnumerateRunes()
            .ToArray();

        var markdown = new (int, SourceFlag)[]
        {
            (21, SourceFlag.SingleLineComment),
            (14, SourceFlag.DoubleQuoteString),
            (1, SourceFlag.Code),
            (22, SourceFlag.MultiLineComment),
            (23, SourceFlag.Code),
            (35, SourceFlag.SingleQuoteString),
            (8, SourceFlag.Code),
            (11, SourceFlag.SingleLineComment)
        };

        var expected = new MarkdownSource(raw.Length);
        for (int i = 0; i < markdown.Length; i++)
        {
            var index = markdown[..i].Sum(m => m.Item1);
            var length = markdown[..(i + 1)].Sum(m => m.Item1) - index;
            raw.CopyTo(expected.Source, index, index, length, r => (r, markdown[i].Item2));
        }

        var fileName = $"{nameof(TestFiles.ShouldReturnAListOfTupleLinkingEachCharacterToAFlag_WhenTheFilePathIsValid)}.lohi";
        var filePath = Path.Combine(_resourcesPath, _className, nameof(_sut.MarkdownSourceCode), fileName);

        // Act
        var output = _sut.MarkdownSourceCode(filePath);

        // Assert
        expected.Source.Count().Should().Be(output.Source.Count());
        foreach (var (e, o) in expected.Source.Zip(output.Source))
        {
            e.character.Should().Be(o.Item1);
            e.flag.Should().Be(o.Item2);
        }
    }
}
