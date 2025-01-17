using NUnit.Framework;
using System;
using System.IO;
using TesteBancoMaster.Data;

namespace TesteBancoMaster.Tests
{
    [TestFixture]
    public class RepositorioDeRotasTests
    {
        private string _arquivoTemp;

        [SetUp]
        public void Setup()
        {
            _arquivoTemp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(_arquivoTemp))
            {
                File.Delete(_arquivoTemp);
            }
        }

        [Test]
        [Category("ObterRotas")]
        public void ObterRotas_ArquivoValido_DeveRetornarRotasCorretas()
        {
            File.WriteAllLines(_arquivoTemp, new[]
            {
                "GRU,BRC,10",
                "BRC,SCL,5"
            });

            var repositorio = new RepositorioDeRotas(_arquivoTemp);
            var rotas = repositorio.ObterRotas();

            Assert.IsTrue(rotas.ContainsKey("GRU"));
            Assert.AreEqual(1, rotas["GRU"].Count);
            Assert.AreEqual(("BRC", 10), rotas["GRU"][0]);

            Assert.IsTrue(rotas.ContainsKey("BRC"));
            Assert.AreEqual(1, rotas["BRC"].Count);
            Assert.AreEqual(("SCL", 5), rotas["BRC"][0]);
        }

        [Test]
        [Category("ObterRotas")]
        public void ObterRotas_ArquivoMalFormatado_DeveLancarFormatException()
        {
            File.WriteAllLines(_arquivoTemp, new[]
            {
                "GRU,BRC", 
                "BRC,SCL,X" 
            });

            var repositorio = new RepositorioDeRotas(_arquivoTemp);

            var ex = Assert.Throws<FormatException>(() => repositorio.ObterRotas());
            Assert.That(ex.Message, Does.Contain("Linha inválida no arquivo"));
        }

        [Test]
        [Category("AdicionarRota")]
        public void AdicionarRota_EntradaValida_DeveAdicionarRotaAoArquivo()
        {
            File.WriteAllLines(_arquivoTemp, new[] { "GRU,BRC,10" });
            var repositorio = new RepositorioDeRotas(_arquivoTemp);

            repositorio.AdicionarRota("BRC,SCL,5");

            var linhas = File.ReadAllLines(_arquivoTemp);
            Assert.AreEqual(2, linhas.Length);
            Assert.AreEqual("BRC,SCL,5", linhas[1]);
        }

        [Test]
        [Category("AdicionarRota")]
        public void AdicionarRota_EntradaInvalida_DeveLancarFormatException()
        {
            File.WriteAllLines(_arquivoTemp, new[] { "GRU,BRC,10" });

            var repositorio = new RepositorioDeRotas(_arquivoTemp);

            var ex = Assert.Throws<FormatException>(() => repositorio.AdicionarRota("GRU,BRC"));
            Assert.That(ex.Message, Does.Contain("Formato inválido"));
        }

        [Test]
        [Category("Inicialização")]
        public void Construtor_ArquivoNaoExiste_DeveLancarFileNotFoundException()
        {
            var caminhoInexistente = Path.Combine(Path.GetTempPath(), "arquivo_inexistente.txt");

            var ex = Assert.Throws<FileNotFoundException>(() => new RepositorioDeRotas(caminhoInexistente));
            Assert.That(ex.Message, Does.Contain("Arquivo de rotas não encontrado"));
        }
    }
}