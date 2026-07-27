using System;
using Omnitrix.Base;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;

namespace Omnitrix.Integrations;

// 1. Um molde simples só para "ler" o JSON que vem da internet
public class AliensApiResponse
{
    [JsonPropertyName("name")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("species")]
    public string Especie { get; set; } = string.Empty;

    [JsonPropertyName("homeworld")]
    public string PlanetaOrigem { get; set; } = string.Empty;

    [JsonPropertyName("strength")]
    public int? ForcaBase { get; set; }
}

// 2. A classe principal que conecta a Internet ao seu Banco SQL
public class Ben10DataIntegration
{

    // Para fins didáticos eu vou deixar a senha exposta aqui mesmo.
    // Entendo que o ideal seria jogar em um appsettings.json ou algo do tipo.
    private readonly string _connectionString = "Server=localhost,1433;Database=OmnitrixDB;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;";

    // O HttpClient é o "navegador" interno do C#, responsável por ir na web
    private readonly HttpClient _httpClient = new();

    public async Task<AliensApiResponse[]> ObterAliensDaApiAsync()
    {
        try
        {
            string url = "https://gist.githubusercontent.com/edvandosimplicio/940806c7193640b909c3135b18cce9a9/raw/75f73c69b00aca4082a64c8ae315ea5ed648abc5/api-ben10-aliens.json";

            string jsonResposta = await _httpClient.GetStringAsync(url);

            var listaAliens = JsonSerializer.Deserialize<AliensApiResponse[]>(jsonResposta);

            return listaAliens ?? Array.Empty<AliensApiResponse>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Nenhum alien se encontra disponível. Verifique a conexão do seu Omntrix!");
            Thread.Sleep(2000);
            Console.WriteLine($"Segue detalhes técnicos: ${ex}");
            Console.WriteLine("\nDemonstração incompleta finalizada.");
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
            Environment.Exit(0);
            return Array.Empty<AliensApiResponse>();
        }
    }

    public int? ObterIdAlienNoBanco(AlienBase alien)
    {
        using var conexao = new SqlConnection(_connectionString);
        conexao.Open();

        string sql = @"
        SELECT TOP 1 IdAlien
        FROM Alien
        WHERE Nome = @Nome
          AND Especie = @Especie;
        ";

        using var comando = new SqlCommand(sql, conexao);

        comando.Parameters.AddWithValue("@Nome", alien.Nome);
        comando.Parameters.AddWithValue("@Especie", alien.Especie);

        var resultado = comando.ExecuteScalar();

        if (resultado == null)
        {
            return null;
        }

        return Convert.ToInt32(resultado);
    }

    public int SalvarAlienNoBanco(AlienBase alien)
    {
        using var conexao = new SqlConnection(_connectionString);
        conexao.Open();

        using var comando = new SqlCommand("sp_GerenciarAlien", conexao);
        comando.CommandType = System.Data.CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@Id", 0);
        comando.Parameters.AddWithValue("@Nome", alien.Nome);
        comando.Parameters.AddWithValue("@Especie", alien.Especie);
        comando.Parameters.AddWithValue("@Forca", alien.ForcaBase);
        comando.Parameters.AddWithValue("@PlanetaOrigem", string.IsNullOrWhiteSpace(alien.PlanetaOrigem) ? "Desconhecido" : alien.PlanetaOrigem);
        comando.Parameters.AddWithValue("@Galaxia", "Via Láctea");
        comando.Parameters.AddWithValue("@Operacao", "i");

        var resultado = comando.ExecuteScalar();

        int idGerado = Convert.ToInt32(resultado);

        alien.DefinirId(idGerado);

        return idGerado;
    }

    public void AtualizarAlienNoBanco(AlienBase alien)
    {
        using var conexao = new SqlConnection(_connectionString);
        conexao.Open();

        using var comando = new SqlCommand("sp_GerenciarAlien", conexao);
        comando.CommandType = System.Data.CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@Id", alien.Id);
        comando.Parameters.AddWithValue("@Nome", alien.Nome);
        comando.Parameters.AddWithValue("@Especie", alien.Especie);
        comando.Parameters.AddWithValue("@Forca", alien.ForcaBase);
        comando.Parameters.AddWithValue("@PlanetaOrigem", string.IsNullOrWhiteSpace(alien.PlanetaOrigem) ? "Desconhecido" : alien.PlanetaOrigem);
        comando.Parameters.AddWithValue("@Galaxia", "Via Láctea");
        comando.Parameters.AddWithValue("@Operacao", "u");

        comando.ExecuteScalar();

    }

    public void DeletarAlienDoBanco(AlienBase alien)
    {
        using var conexao = new SqlConnection(_connectionString);
        conexao.Open();

        using var comando = new SqlCommand("sp_GerenciarAlien", conexao);
        comando.CommandType = System.Data.CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@Id", alien.Id);
        comando.Parameters.AddWithValue("@Operacao", "d");

        comando.ExecuteScalar();
    }


}