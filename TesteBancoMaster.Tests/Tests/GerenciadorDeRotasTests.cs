using NUnit.Framework;
using System;
using System.Collections.Generic;
using TesteBancoMaster.Business;
using TesteBancoMaster.Data;

namespace TesteBancoMaster.Tests
{
    [TestFixture]
    public class GerenciadorDeRotasTests
    {
        private GerenciadorDeRotas _gerenciador;
        private RepositorioDeRotas _repositorio;

        [SetUp]
        public void Setup()
        {
            var rotas = new Dictionary<string, List<(string Destino, int Custo)>>
            {
                { "GRU", new List<(string, int)> { ("BRC", 10), ("SCL", 20) } },
                { "BRC", new List<(string, int)> { ("SCL", 5) } },
                { "SCL", new List<(string, int)> { ("GRU", 15) } }
            };

            _repositorio = new RepositorioDeRotasSimulado(rotas);
            _gerenciador = new GerenciadorDeRotas(_repositorio);
        }

        [Test]
        [Category("ObterMelhorRota")]
        public void ObterMelhorRota_RotaValida_DeveRetornarCaminhoCorreto()
        {
            var resultado = _gerenciador.ObterMelhorRota("GRU", "SCL");

            Assert.IsNotNull(resultado);
            Assert.AreEqual(3, resultado.Caminho.Count);
            Assert.AreEqual("GRU", resultado.Caminho[0]);
            Assert.AreEqual("BRC", resultado.Caminho[1]);
            Assert.AreEqual("SCL", resultado.Caminho[2]);
            Assert.AreEqual(15, resultado.Custo);
        }

        [Test]
        [Category("ObterMelhorRota")]
        public void ObterMelhorRota_DestinoInexistente_DeveLancarException()
        {
            var ex = Assert.Throws<Exception>(() => _gerenciador.ObterMelhorRota("GRU", "XYZ"));

            Assert.That(ex.Message, Does.Contain("Destino não encontrada"));
        }

        [Test]
        [Category("ObterMelhorRota")]
        public void ObterMelhorRota_OrigemInexistente_DeveLancarException()
        {
            var ex = Assert.Throws<Exception>(() => _gerenciador.ObterMelhorRota("XYZ", "SCL"));

            Assert.That(ex.Message, Does.Contain("Origem não encontrada"));
        }

        [Test]
        [Category("ObterMelhorRota")]
        public void ObterMelhorRota_MelhorCusto_DeveEscolherRotaMaisBarata()
        {
            var resultado = _gerenciador.ObterMelhorRota("GRU", "SCL");

            Assert.AreEqual(15, resultado.Custo);
            Assert.AreEqual("GRU", resultado.Caminho[0]);
            Assert.AreEqual("BRC", resultado.Caminho[1]);
            Assert.AreEqual("SCL", resultado.Caminho[2]);
        }
    }

    public class RepositorioDeRotasSimulado : RepositorioDeRotas
    {
        private readonly Dictionary<string, List<(string Destino, int Custo)>> _rotas;

        public RepositorioDeRotasSimulado(Dictionary<string, List<(string Destino, int Custo)>> rotas)
            : base(null)
        {
            _rotas = rotas;
        }

        public override Dictionary<string, List<(string Destino, int Custo)>> ObterRotas()
        {
            return _rotas;
        }

        public override void AdicionarRota(string entrada)
        {
            
        }
    }
}
