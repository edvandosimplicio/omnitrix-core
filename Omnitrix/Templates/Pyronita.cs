using System;
using Omnitrix.Base;

namespace Omnitrix.Templates;

public class Pyronita : AlienBase
{
    public int TemperaturaChama { get; set; } = 1500;
    public Pyronita(string nome, string planetaOrigem, int forcaBase) : base(nome, "Pyronita", planetaOrigem, forcaBase)
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

        Console.WriteLine($"{Nome} disparou uma \u001b[31mExplosão Solar ☄️\u001b[0m a {TemperaturaChama}°C!🔥!");
        Thread.Sleep(3000);
        Console.WriteLine($"\nFoi consumido um valor de energia equivalente a {tempoConsumido.Minutes} minutos e {tempoConsumido.Seconds} segundos.");
        Thread.Sleep(4500);
        Console.WriteLine($"Restam {tempoRestante.Minutes} minutos e {tempoRestante.Seconds} da transformação.");
        Thread.Sleep(4500);
        Console.Clear();

    }
}