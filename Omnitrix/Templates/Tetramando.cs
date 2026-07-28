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
        int custoTempoEmSegundos = ForcaBase * 2;

        TempoMaximoTransformacaoEmSegundos -= custoTempoEmSegundos;

        if (TempoMaximoTransformacaoEmSegundos < 0)
        {
            TempoMaximoTransformacaoEmSegundos = 0;
        }

        TimeSpan tempoConsumido = TimeSpan.FromSeconds(custoTempoEmSegundos);
        TimeSpan tempoRestante = TimeSpan.FromSeconds(TempoMaximoTransformacaoEmSegundos);

        Console.WriteLine($"{Nome} formou uma \u001b[31mOnda Sônica 🔊\u001b[0m com seus {QuantidadeDeBracos} braços!🦾.");
        Thread.Sleep(3000);
        Console.WriteLine($"\nFoi consumido um valor de energia equivalente a {tempoConsumido.Minutes} minutos e {tempoConsumido.Seconds} segundos.");
        Thread.Sleep(4500);
        Console.WriteLine($"Restam {tempoRestante.Minutes} minutos e {tempoRestante.Seconds} da transformação.");
        Thread.Sleep(4500);
        Console.Clear();

    }
}
