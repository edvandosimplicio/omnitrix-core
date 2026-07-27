using System;
using Omnitrix.Contracts;

namespace Omnitrix.Base;

public abstract class AlienBase : ITransformacao
{
    public int? Id { get; protected set; }
    public string Nome { get; protected set; } = string.Empty;
    public string Especie { get; protected set; } = string.Empty;
    public string PlanetaOrigem { get; protected set; } = string.Empty;
    public int ForcaBase { get; protected set; }
    public int TempoMaximoTransformacaoEmSegundos { get; protected set; } = 600;

    protected AlienBase(string nome, string especie, string planetaOrigem, int forcaBase)
    {
        Nome = nome;
        Especie = especie;
        PlanetaOrigem = planetaOrigem;
        ForcaBase = forcaBase;

        if (TempoMaximoTransformacaoEmSegundos < 0)
        {
            TempoMaximoTransformacaoEmSegundos = 0;
        }
        if (TempoMaximoTransformacaoEmSegundos > 600)
        {
            TempoMaximoTransformacaoEmSegundos = 600;
        }
    }

    protected AlienBase(int id, string nome, string especie, string planetaOrigem, int forcaBase) : this(nome, especie, planetaOrigem, forcaBase)
    {
        DefinirId(id);
    }

    public void DefinirId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("O ID do alien precisa ser maior que zero.");
        }

        Id = id;
    }

    public virtual void Atacar()
    {
        int custoTempo = ForcaBase / 2;
        TempoMaximoTransformacaoEmSegundos -= custoTempo;
        Console.WriteLine($"{Nome} desferiu um ataque base com força de {ForcaBase}.");
        Thread.Sleep(3000);
        Console.WriteLine($"Foi consumido um valor de energia do Omnitrix equivalente á {custoTempo / 60} minutos.");
        Thread.Sleep(4500);
        Console.WriteLine($"Restam {TempoMaximoTransformacaoEmSegundos / 60} minutos da sua atual transformação.");
        Thread.Sleep(4500);
        Console.Clear();
    }

    public abstract void UsarPoderEspecial();

}