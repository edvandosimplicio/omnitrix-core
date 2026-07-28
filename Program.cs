using System;
using System.Threading.Tasks;
using Omnitrix.Base;
using Omnitrix.Enums;
using Omnitrix.Integrations;
using Omnitrix.Services;
using Omnitrix.Templates;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.Clear();
Console.WriteLine("=\nCarregando sistema");
Thread.Sleep(2000);
Console.Clear();
Console.WriteLine("====\nCarregando sistema");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("========\nCarregando sistema");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("============\nCarregando sistema");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("================\nCarregando sistema");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("====================\nCarregando sistema");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("========================\nCarregando sistema");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("============================\nCarregando sistema");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("================================\nCarregando sistema");
Thread.Sleep(1000);

Console.WriteLine("=");
Thread.Sleep(2000);
Console.Clear();
Console.WriteLine("================================\nCarregando sistema");
Console.WriteLine("====");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("================================\nCarregando sistema");
Console.WriteLine("========");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("================================\nCarregando sistema");
Console.WriteLine("============");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("================================\nCarregando sistema");
Console.WriteLine("================");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("================================\nCarregando sistema");
Console.WriteLine("====================");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("================================\nCarregando sistema");
Console.WriteLine("========================");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("================================\nCarregando sistema");
Console.WriteLine("============================");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("================================\nCarregando sistema");
Console.WriteLine("================================");
Thread.Sleep(1000);
Console.Clear();

Console.WriteLine("======================================");
Console.WriteLine("      SISTEMA OMNITRIX INICIADO!    ");
Console.WriteLine("======================================");
Thread.Sleep(3000);

Console.Write("\nSeja bem-vindo(a) novo(a) portador(a)! \nDigite seu nome e sobrenome: ");
string? nomePortador = Console.ReadLine();

//tratamento de string vazia/em branco acidental
if (string.IsNullOrWhiteSpace(nomePortador))
{
    nomePortador = "Ben Tennyson";
}

//instancia relogio passando o portador como argumento
Relogio relogioPortador = new(nomePortador);
//instancia banco
Ben10DataIntegration integracaoApiBanco = new();
//instancia services do omnitrix com portador e integracao do banco/api como argumento
OmnitrixService acoesDoRelogio = new(relogioPortador, integracaoApiBanco);

Console.Clear();
Console.WriteLine("\nBuscando aliens disponíveis .");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("\nBuscando aliens disponíveis ..");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("\nBuscando aliens disponíveis ...");
Thread.Sleep(1000);
Console.Clear();

//puxa lista de aliens da api pública com async
AliensApiResponse[] aliensDisponiveis = await integracaoApiBanco.ObterAliensDaApiAsync();

Console.WriteLine("\nSincronizando dados dos DNA's alienígenas no seu relógio .");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("\nSincronizando dados dos DNA's alienígenas no seu relógio ..");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("\nSincronizando dados dos DNA's alienígenas no seu relógio ...");
Thread.Sleep(1000);
Console.Clear();

//lista os aliens da api
acoesDoRelogio.ListarEspeciesDisponiveis(aliensDisponiveis);
//essa function tá lá em baixo, criada apenas p tratar melhor a opção que o user for selecionar
int opcaoAlien = LerOpcaoNumerica("\nEscolha o número referente ao alien do qual deseja se transformar: ", 1, aliensDisponiveis.Length);

//instancia recebendo o índice do alien selecionado pelo usuário sobre a API pública
AliensApiResponse alienSelecionadoApi = aliensDisponiveis[opcaoAlien - 1];

//instancia recebe método de transformação com o alien selecionado como argumento
AlienBase alienTransformado = acoesDoRelogio.TransformarEmAlien(alienSelecionadoApi);
Console.Clear();
Console.WriteLine("Transformando...");
Thread.Sleep(2500);
Console.Clear();

//método de verificação para salvar ou atualizar alien no banco
acoesDoRelogio.SalvarOuAtualizarAlienNoBanco(alienTransformado);

LimparConsole();
Console.WriteLine("\nUm inimigo surgiu!");
Thread.Sleep(2500);
LimparConsole();
Console.WriteLine("Vilgax!");
Thread.Sleep(1500);
LimparConsole();
Console.WriteLine("Vilgax .");
Thread.Sleep(1500);
Console.Clear();
Console.WriteLine("Vilgax ..");
Thread.Sleep(1500);
Console.Clear();
Console.WriteLine("Vilgax ...");
Thread.Sleep(1500);
Console.Clear();
Console.WriteLine("Vilgax: Me entregue agora o Omnitrix, seu isolente!");
Thread.Sleep(4000);
Console.WriteLine("Vilgax: É uma vergonha uma arma tão poderosa estar nas mãos de um humano como você.");
Thread.Sleep(5000);
Console.WriteLine("Vilgax: Prepare-se para ser obliterado!");
Thread.Sleep(4000);
LimparConsole();
Console.WriteLine("Defenda seu planeta de Vilgax! \nEscolha sua primeira ação:");
Thread.Sleep(4500);
Console.WriteLine("1 - Ataque básico");
Console.WriteLine("2 - Poder especial:");

int opcaoHabilidade = LerOpcaoNumerica("", 1, 2);
//atribui a opcção selecionada do user para nosso Enum
TipoHabilidade primeiraHabilidade = (TipoHabilidade)opcaoHabilidade;
Thread.Sleep(3000);

LimparConsole();
//executa habilidade passando alien e habilidade selecionada como argumento
acoesDoRelogio.ExecutarHabilidade(alienTransformado, primeiraHabilidade);
Thread.Sleep(3000);

//Verifica qual habilidade foi utilizada para oferecer a próxima que restou para o usuário
TipoHabilidade segundaHabilidade = primeiraHabilidade == TipoHabilidade.AtaqueBasico ? TipoHabilidade.PoderEspecial : TipoHabilidade.AtaqueBasico;

Console.WriteLine("O inimigo ainda está de pé!");
Thread.Sleep(3000);
Console.WriteLine("Vilgax: DROGA! você quase me derrotou. Não pegarei mais leve com você.");
Thread.Sleep(5000);
Console.WriteLine($"Use agora a habilidade restante: '{segundaHabilidade}' e finalize o inimigo.");
Thread.Sleep(5000);

AguardarEnter();

Thread.Sleep(3000);
LimparConsole();

//após verificação, executa a segunda habilidade
acoesDoRelogio.ExecutarHabilidade(alienTransformado, segundaHabilidade);

LimparConsole();
Console.WriteLine("Vilgax: NÃO É POSSÍVEL!!");
Thread.Sleep(4500);
Console.WriteLine("Vilgax: Eu reencarnarei para lhe destruir na próxima vida, seu miserável!");
Thread.Sleep(5000);
Console.WriteLine("Vilgax: VOCÊ ME PAGA!!");
Thread.Sleep(4500);
LimparConsole();
Console.WriteLine("O inimigo foi derrotado com sucesso!");
Thread.Sleep(4500);
Console.WriteLine("Seu planeta está em segurança e o Omnitrix está em boas mãos.");
Thread.Sleep(4500);

acoesDoRelogio.MostrarStatusTransformacao(alienTransformado);
Thread.Sleep(5000);

Console.WriteLine("\nDemonstração finalizada.");
Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();
Environment.Exit(0);

static int LerOpcaoNumerica(string mensagem, int minimo, int maximo)
{
    while (true)
    {
        Console.Write(mensagem);

        string? entrada = Console.ReadLine();

        bool conversaoValida = int.TryParse(entrada, out int opcao);

        if (conversaoValida && opcao >= minimo && opcao <= maximo)
        {
            return opcao;
        }

        Console.WriteLine($"Opção inválida. Digite um número entre {minimo} e {maximo}.");
    }

}

static void AguardarEnter()
{
    Console.WriteLine("\nPressione Enter para continuar...");
    Console.ReadLine();
    LimparConsole();
}

static void LimparConsole()
{
    Console.Clear();

    // Força limpeza mais completa em terminais que suportam ANSI, como o VS Code
    Console.Write("\x1b[3J\x1b[H\x1b[2J");
}