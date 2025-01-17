using System.Collections.Generic;

namespace TesteBancoMaster.Business
{
    public class ResultadoRota
    {
        public List<string> Caminho { get; }
        public int Custo { get; }

        public ResultadoRota(List<string> caminho, int custo)
        {
            Caminho = caminho;
            Custo = custo;
        }
    }
}