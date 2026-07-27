using Omnitrix.Base;
using Omnitrix.Integrations;
using Omnitrix.Templates;

namespace Omnitrix.Services;

public static class AlienFactory
{
    public static AlienBase CriarAlien(AliensApiResponse aliensApi)
    {
        int forcaBase = aliensApi.ForcaBase ?? 85;

        string nome = string.IsNullOrWhiteSpace(aliensApi.Nome) ? "Alien desconhecida" : aliensApi.Nome;

        string especie = string.IsNullOrWhiteSpace(aliensApi.Especie) ? "Espécie desconhecida" : aliensApi.Especie;

        string planetaOrigem = string.IsNullOrWhiteSpace(aliensApi.PlanetaOrigem) ? "Desconhecido" : aliensApi.PlanetaOrigem;

        return especie.ToLower() switch
        {
            "pyronita" => new Pyronita(nome, planetaOrigem, forcaBase),
            "tetramando" => new Tetramando(nome, planetaOrigem, forcaBase),
            _ => new AlienGenericoDaApi(nome, especie, planetaOrigem, forcaBase)
        };
    }
}