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
        int custoExplosaoSolar = ForcaBase * 2;
        TempoMaximoTransformacaoEmSegundos -= custoExplosaoSolar;

        Console.WriteLine($"{Nome} disparou uma \u001b[31mExplosão Solar ☄️\u001b[0m a {TemperaturaChama}°C!🔥!");
        Thread.Sleep(3000);
        Console.WriteLine($"\nFoi consumido um valor de energia do Omnitrix equivalente á {custoExplosaoSolar / 60} minutos.");
        Thread.Sleep(4500);
        Console.WriteLine($"Restam {TempoMaximoTransformacaoEmSegundos / 60} minutos da sua atual transformação.");
        Thread.Sleep(4500);
        Console.Clear();

    }
}