namespace Compiler;


internal class Program
{
    static void Main()
    {
        // var path = @"C:\Users\bruno\OneDrive\Área de Trabalho\Projetos\LoHi\Compiler\Sources\SingleClassExample.lohi";

        var test = "Isto é um teste ñýàá".ToCharArray();

        foreach (var c in test)
            Console.WriteLine(c);
    }
}
