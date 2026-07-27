using System;
using Omnitrix.Base;
using Omnitrix.Integrations;
using Omnitrix.Enums;
using DispositivoOmnitrix = Omnitrix.Templates.Relogio;
using Microsoft.VisualBasic;

namespace Omnitrix.Services;

public class OmnitrixService
{
    private readonly DispositivoOmnitrix _omnitrix;
    private readonly Ben10DataIntegration _integracaoBanco;
    public OmnitrixService(DispositivoOmnitrix omnitrix, Ben10DataIntegration integracaoBanco)
    {
        _omnitrix = omnitrix;
        _integracaoBanco = integracaoBanco;
    }

    public void ListarEspeciesDisponiveis(AliensApiResponse[] aliens)
    {
        try
        {
            Console.WriteLine("=== Aliens disponíveis no seu Omnitrix ===\n");

            for (int i = 0; i < aliens.Length; i++)
            {
                var alien = aliens[i];

                Console.WriteLine(
                    $"{i + 1} - {alien.Nome} | Espécie: {alien.Especie} | Planeta: {alien.PlanetaOrigem} | Força: {alien.ForcaBase ?? 85}"
                );
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Falha na sincronização: {ex.Message}");

        }
    }

    public AlienBase TransformarEmAlien(AliensApiResponse aliensApi)
    {
        LimparConsole();

        AlienBase alien = AlienFactory.CriarAlien(aliensApi);

        _omnitrix.DesbloquearAlien(alien);

        Console.WriteLine($"{_omnitrix.Portador} ativou o Omnitrix!");
        Thread.Sleep(2500);
        Console.WriteLine($"Transformação selecionada: {alien.Nome} ({alien.Especie})");
        Thread.Sleep(2500);
        Console.WriteLine($"Planeta de origem: {alien.PlanetaOrigem}");
        Thread.Sleep(2500);
        Console.WriteLine($"Força base: {alien.ForcaBase}");
        Thread.Sleep(3000);

        LimparConsole();

        return alien;
    }

    public void SalvarOuAtualizarAlienNoBanco(AlienBase alien)
    {
        int? idAlienExistente = _integracaoBanco.ObterIdAlienNoBanco(alien);
        try
        {
            if (idAlienExistente.HasValue)
            {

                alien.DefinirId(idAlienExistente.Value);

                _integracaoBanco.AtualizarAlienNoBanco(alien);

                Thread.Sleep(2500);
                Console.WriteLine($"Alien '{alien.Nome}' ({alien.Especie}) já existia e foi atualizado no relógio!");
                Thread.Sleep(2500);
                LimparConsole();
            }
            else
            {
                _integracaoBanco.SalvarAlienNoBanco(alien);
                Thread.Sleep(2500);
                Console.WriteLine($"Alien novo! '{alien.Nome}' foi salvo no relogóio!");
                Thread.Sleep(2500);
                LimparConsole();
            }
        }
        catch (Exception ex)
        {
            Thread.Sleep(2500);
            Console.WriteLine("Não foi possível salvar o alien no relógio.");
            Console.WriteLine($"Detalhe técnico: {ex.Message}");
            Thread.Sleep(7000);
            LimparConsole();
        }

    }

    public void ExecutarHabilidade(AlienBase alien, TipoHabilidade habilidade)

    {
        if (habilidade == TipoHabilidade.AtaqueBasico)
        {
            UsarAtaqueBasico(alien);
        }
        else if (habilidade == TipoHabilidade.PoderEspecial)
        {
            UsarPoderEspecial(alien);
        }
        else
        {
            Thread.Sleep(1000);
            Console.WriteLine("Habilidade inválida.");
        }
    }

    public void UsarAtaqueBasico(AlienBase alien)
    {
        alien.Atacar();
        DescarregarBateria(alien);
    }

    public void UsarPoderEspecial(AlienBase alien)
    {
        alien.UsarPoderEspecial();
        DescarregarBateria(alien);
    }

    public void MostrarStatusTransformacao(AlienBase alien)
    {
        Console.WriteLine("\n=== Status da transformação ===");
        Console.WriteLine($"Alien atual: {alien.Nome}");
        Console.WriteLine($"Espécie: {alien.Especie}");
        Console.WriteLine($"Tempo restante: {alien.TempoMaximoTransformacaoEmSegundos / 60} minutos");
    }

    public void DescarregarBateria(AlienBase alien)
    {
        _omnitrix.AtualizarBateriaPeloTempoDoAlien(alien);
    }

    private static void LimparConsole()
    {
        Console.Clear();
        Console.Write("\u001b[3J\u001b[2J\u001b[H");
        Console.Out.Flush();
    }
}