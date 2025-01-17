using System;
using System.Collections.Generic;

namespace TesteBancoMaster.Business
{
    public class GerenciadorDeRotas
    {
        private readonly Dictionary<string, List<(string Destino, int Custo)>> _rotas;

        public GerenciadorDeRotas(Data.RepositorioDeRotas repositorio)
        {
            _rotas = repositorio.ObterRotas();
        }

        public ResultadoRota ObterMelhorRota(string origem, string destino)
        {
            if (!_rotas.ContainsKey(origem))
                throw new Exception("Origem não encontrada.");
            if (!_rotas.ContainsKey(destino))
                throw new Exception("Destino não encontrada");

            var melhorRota = (Caminho: new List<string>(), Custo: int.MaxValue);
            var fila = new Queue<(string Atual, List<string> Caminho, int Custo)>();
            fila.Enqueue((origem, new List<string> { origem }, 0));

            while (fila.Count > 0)
            {
                var (atual, caminho, custo) = fila.Dequeue();

                if (atual == destino && custo < melhorRota.Custo)
                {
                    melhorRota = (new List<string>(caminho), custo);
                }

                if (_rotas.ContainsKey(atual))
                {
                    foreach (var (proximo, custoProximo) in _rotas[atual])
                    {
                        if (!caminho.Contains(proximo))
                        {
                            var novoCaminho = new List<string>(caminho) { proximo };
                            fila.Enqueue((proximo, novoCaminho, custo + custoProximo));
                        }
                    }
                }
            }

            if (melhorRota.Custo == int.MaxValue)
                throw new Exception("Rota não encontrada.");

            return new ResultadoRota(melhorRota.Caminho, melhorRota.Custo);
        }
    }
}