namespace Omnitrix.Contracts;

public interface ITransformacao
{
    string Nome { get; }
    string Especie { get; }
    string PlanetaOrigem { get; }
    int ForcaBase { get; }
    int TempoMaximoTransformacaoEmSegundos { get; }

    void Atacar();
    void UsarPoderEspecial();
}