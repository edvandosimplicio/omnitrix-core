using System;
using Omnitrix.Base;

namespace Omnitrix.Templates;

public class Tetramando : AlienBase
{
    public int QuantidadeDeBracos { get; set; } = 4;
    public Tetramando(string nome, string planetaOrigem, int forcaBase) : base(nome, "Tetramando", planetaOrigem, forcaBase)
    {
    }

    public override void UsarPoderEspecial()
    {
        int custoPalmaSonica = ForcaBase * 2;
        TempoMaximoTransformacaoEmSegundos -= custoPalmaSonica;

        Console.WriteLine($"{Nome} formou uma \u001b[31mOnda Sônica 🔊\u001b[0m com seus {QuantidadeDeBracos} braços!🦾.");
        Thread.Sleep(3000);
        Console.WriteLine($"\nFoi consumido um valor de energia do Omnitrix equivalente á {custoPalmaSonica / 60} minutos.");
        Thread.Sleep(4500);
        Console.WriteLine($"Restam {TempoMaximoTransformacaoEmSegundos / 60} minutos da sua atual transformação.");
        Thread.Sleep(4500);
        Console.Clear();

    }
}
