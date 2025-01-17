# Documentação da Aplicação

## Como Executar a Aplicação

Para executar a aplicação, siga os passos abaixo:

1. **Pré-requisitos:**
   - **.NET 9 ou superior** instalado no seu sistema. Você pode verificar se o .NET está instalado executando o comando:
     ```
     dotnet --version
     ```
   - **Editor de código** (recomendado: [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio](https://visualstudio.microsoft.com/)).

2. **Clonar o Repositório:**
   Caso você ainda não tenha o repositório, clone-o com o seguinte comando:
   ```
   git clone <URL-do-repositório>
   ```

3. **Restaurar Pacotes:**
   Execute o comando abaixo para restaurar os pacotes NuGet necessários:
   ```
   dotnet restore
   ```

4. **Executar a Aplicação:**
   Para rodar a aplicação no modo de console, execute:
   ```
   dotnet run
   ```
   
   Ao iniciar a aplicação aparecerá a seguinte mensagem "Digite o caminho do arquivo de rotas:", informar o caminho completo do arquivo de rotas, conforme exemplo abaixo:
   
   ```
   C:\Projetos\TesteBancoMaster\rotas.txt
   ```

5. **Testes:**
   Para rodar os testes automatizados (unitários), execute:
   ```
   dotnet test
   ```

## Estrutura dos Arquivos/Pacotes

A aplicação segue uma estrutura de **camadas** dividida em pacotes e arquivos principais:

```
TesteBancoMaster/
├── Application/
│   └── Program.cs                      # Ponto de entrada da aplicação (console)
├── Business/
│   ├── GerenciadorDeRotas.cs           # Lógica de negócios para gerenciar rotas
│   ├── ResultadoRota.cs                # Modelo que contém os resultados de uma rota
├── Data/
│   ├── RepositorioDeRotas.cs          # Acesso a dados, incluindo a leitura e gravação de rotas

TesteBancoMaster.Tests/
├── Tests/
│   ├── GerenciadorDeRotasTests.cs      # Testes unitários para a lógica de negócios
│   ├── RepositorioDeRotasTests.cs     # Testes unitários para o repositório de dados
```

## Decisões de Design Adotadas

### 1. Separação de responsabilidades:
	**Objetivo**: Tornar o código modular, testável e fácil de manter.
	**Como foi implementado**: 
		- A lógica de gerenciamento de rotas (ex.: busca de melhores rotas) foi colocada na classe GerenciadorDeRotas, mantendo-a separada do acesso a dados.
		- A persistência e leitura de dados das rotas são tratadas pela classe RepositorioDeRotas, que abstrai o acesso ao sistema de arquivos.
		- A classe ResultadoRota encapsula os dados de saída para facilitar o retorno de informações sobre rotas.

### 2. Extensibilidade e substituição:
	**Objetivo**: Facilitar alterações futuras e a substituição de partes do sistema.
	**Como foi implementado**:
		- O uso da classe RepositorioDeRotas permite substituir o mecanismo de armazenamento de rotas (como mudar de arquivo para banco de dados) sem alterar a lógica de negócios.
		- Para testes, foi criado um repositório simulado (RepositorioDeRotasSimulado), que evita dependências de arquivos e acelera a execução.

### 3. Busca de rotas com um algoritmo simples:
   	**Objetivo**: Resolver o problema proposto de forma eficiente e compreensível.
	**Como foi implementado**: 
		- Um algoritmo de busca baseado em filas foi utilizado para encontrar o menor custo entre origem e destino.

### 4. Testes unitários como garantia de qualidade:
   	**Objetivo**: Garantir o funcionamento correto da aplicação.
	**Como foi implementado**:
		- Os testes cobrem os principais casos de uso (rotas válidas, melhores custos, entradas inválidas).
