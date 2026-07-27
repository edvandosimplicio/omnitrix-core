using System;
using System.Collections.Generic;
using Omnitrix.Base;

namespace Omnitrix.Templates;

public class Relogio
{
    public string Portador { get; private set; }
    public int NivelBateria { get; private set; }

    private readonly List<AlienBase> _aliensDesbloqueados = new();
    public IReadOnlyList<AlienBase> AliensDesbloqueados => _aliensDesbloqueados;

    public Relogio(string portador)
    {
        Portador = portador;
        NivelBateria = 100;
    }

    public void DesbloquearAlien(AlienBase novoAlien)
    {
        _aliensDesbloqueados.Add(novoAlien);
        Console.Clear();
        Console.WriteLine($"DNA de {novoAlien.Nome} ({novoAlien.Especie}) foi adicionado à matriz! aguarde... ");
        Thread.Sleep(3000);
        Console.Clear();
    }

    public void AtualizarBateriaPeloTempoDoAlien(AlienBase alien)
    {
        const int tempoTotalTransformacao = 600; // 10 minutos

        int bateriaCalculada = alien.TempoMaximoTransformacaoEmSegundos * 100 / tempoTotalTransformacao;

        if (bateriaCalculada < 0)
        {
            bateriaCalculada = 0;
        }

        if (bateriaCalculada > 100)
        {
            bateriaCalculada = 100;
        }

        NivelBateria = bateriaCalculada;

        Console.WriteLine($"Bateria atual do Omnitrix: {NivelBateria}%");
        Thread.Sleep(3000);
    }
}