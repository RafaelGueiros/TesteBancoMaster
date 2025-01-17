using TesteBancoMaster.Business;
using TesteBancoMaster.Data;
using System;
using System.IO;

namespace TesteBancoMaster.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o caminho do arquivo de rotas:");
            string caminhoArquivo = Console.ReadLine();

            if (string.IsNullOrEmpty(caminhoArquivo) || !File.Exists(caminhoArquivo))
            {
                Console.WriteLine("Caminho inválido ou arquivo não encontrado. O programa será encerrado.");
                return;
            }

            var repositorio = new RepositorioDeRotas(caminhoArquivo);

            Console.WriteLine("Bem-vindo ao sistema de rotas!");
            while (true)
            {
                var gerenciador = new GerenciadorDeRotas(repositorio);
                Console.WriteLine("\n1. Registrar nova rota");
                Console.WriteLine("2. Consultar melhor rota");
                Console.WriteLine("3. Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        RegistrarNovaRota(repositorio);
                        break;
                    case "2":
                        ConsultarMelhorRota(gerenciador);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        static void RegistrarNovaRota(RepositorioDeRotas repositorio)
        {
            Console.Write("Digite a rota no formato Origem,Destino,Valor: ");
            var entrada = Console.ReadLine();

            try
            {
                repositorio.AdicionarRota(entrada);
                Console.WriteLine("Rota registrada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar rota: {ex.Message}");
            }
        }

        static void ConsultarMelhorRota(GerenciadorDeRotas gerenciador)
        {
            Console.Write("Digite a rota no formato Origem-Destino: ");
            var entrada = Console.ReadLine();

            try
            {
                var pontos = entrada.Split('-');
                var origem = pontos[0];
                var destino = pontos[1];

                var resultado = gerenciador.ObterMelhorRota(origem, destino);

                Console.WriteLine($"Melhor Rota: {string.Join(" - ", resultado.Caminho)} ao custo de ${resultado.Custo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar rota: {ex.Message}");
            }
        }
    }
}