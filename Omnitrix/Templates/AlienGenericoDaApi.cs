using System;
using Omnitrix.Base;

namespace Omnitrix.Templates;

public class AlienGenericoDaApi : AlienBase
{
    public AlienGenericoDaApi(string nome, string especie, string planetaOrigem, int forcaBase) : base(nome, especie, planetaOrigem, forcaBase)
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

        Console.WriteLine($"{Nome} usou um poder especial da espécie {Especie}!");
        Thread.Sleep(3000);
        Console.WriteLine($"Foi consumido um valor de energia equivalente a {tempoConsumido.Minutes} minutos e {tempoConsumido.Seconds} segundos.");
        Thread.Sleep(4500);
        Console.WriteLine($"Restam {tempoRestante.Minutes} minutos e {tempoRestante.Seconds} segundos da transformação.");
        Thread.Sleep(4500);
        Console.Clear();

    }
}