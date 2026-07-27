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
        int custoPoderEspecial = ForcaBase * 2;
        TempoMaximoTransformacaoEmSegundos -= custoPoderEspecial;

        Console.WriteLine($"{Nome} usou um poder especial da espécie {Especie}!");
        Thread.Sleep(3000);
        Console.WriteLine($"Foi consumido um valor de energia equivalente a {custoPoderEspecial / 60} minutos.");
        Thread.Sleep(4500);
        Console.WriteLine($"Restam {TempoMaximoTransformacaoEmSegundos / 60} minutos da transformação.");
        Thread.Sleep(4500);
        Console.Clear();

    }
}