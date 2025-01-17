using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TesteBancoMaster.Data
{
    public class RepositorioDeRotas
    {
        private readonly string _arquivo;
        private static readonly object _lock = new object();

        public RepositorioDeRotas(string arquivo)
        {
            _arquivo = arquivo;

            if (_arquivo != null && !File.Exists(_arquivo))
            {
                throw new FileNotFoundException("Arquivo de rotas não encontrado", _arquivo);
            }
        }

        public virtual Dictionary<string, List<(string Destino, int Custo)>> ObterRotas()
        {
            var rotas = new Dictionary<string, List<(string, int)>>();

            if (_arquivo != null)
            {

                foreach (var linha in File.ReadLines(_arquivo))
                {
                    var partes = linha.Split(',').Select(p => p.Trim()).ToArray();

                    if (partes.Length != 3 || !int.TryParse(partes[2], out int custo))
                    {
                        throw new FormatException($"Linha inválida no arquivo: {linha}");
                    }

                    var origem = partes[0];
                    var destino = partes[1];

                    if (!rotas.ContainsKey(origem))
                        rotas[origem] = new List<(string, int)>();

                    rotas[origem].Add((destino, custo));
                }
            }

            return rotas;
        }

        public virtual void AdicionarRota(string entrada)
        {
            if (_arquivo != null)
            {
                var partes = entrada.Split(',').Select(p => p.Trim()).ToArray();

                if (partes.Length != 3 || !int.TryParse(partes[2], out _))
                {
                    throw new FormatException("Formato inválido. Use o formato Origem,Destino,Custo.");
                }

                lock (_lock)
                {
                    File.AppendAllText(_arquivo, entrada + Environment.NewLine);
                }
            }
        }
    }
}