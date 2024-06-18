using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.AutoMock;
using TreinamentoAPI.DTOs;
using TreinamentoAPI.Entidades;
using TreinamentoAPI.Interfaces.Bancos;
using TreinamentoAPI.Servicos;

namespace Testes
{
    public class ProdutoServicoTeste
    {
        private readonly ProdutoServico _produtoServico;
        private readonly AutoMocker mocker = new();
        public ProdutoServicoTeste()
        {
            _produtoServico = mocker.CreateInstance<ProdutoServico>();
        }

        [Fact]
        public void CadastrarProduto_RegrasValidas_RetornaSucesso()
        {
            #region Arrange
            var produtoTeste = new Produto
            {
                Id = 1,
                Nome = "Laranja",
                Quantidade = 10,
                Preco = (float?)6.5,
                NomeFornecedor = "abc",
                NumeroFornecedor = 16,

            };
            #endregion

            #region Act
            var retorno = _produtoServico.CadastrarProduto(produtoTeste);
            #endregion

            #region Assert
            Assert.True(retorno);
            #endregion
        }

        [Fact]
        public void CadastrarProduto_RegrasInvalidas_RetornaError()
        {
            #region Arrange
            var produtoTeste = new Produto
            {
                Id = 1,
                Nome = "uva",
                Quantidade = -10,
                Preco = (float)-8.5,
                NomeFornecedor = "bosta",
                NumeroFornecedor = -9
            };
            #endregion

            #region Act & Assert
            Assert.Throws<Exception>(() => _produtoServico.CadastrarProduto(produtoTeste));
            #endregion

        }
        [Fact]
        public void MostrarListaProduto_RegrasValidas_RetornaSucessoOuNull()
        {
            #region Arrange
            var listaProdutos = new List<Produto>
    {
        new Produto {
            Id = 1,
            Nome = "Laranja",
            Quantidade = 10,
            Preco = (float?)6.5,
            NomeFornecedor = "abc",
            NumeroFornecedor = 16,
        }
    };

            var listaProdutosDto = new List<ProdutoDto>
    {
        new ProdutoDto {
            Id = 1,
            Nome = "Laranja",
            Quantidade = 10,
            
        }
    };

            mocker.GetMock<IProdutoRepositorio>()
                .Setup(r => r.MostrarProdutos())
                .Returns(listaProdutos);

            mocker.GetMock<IMapper>()
                .Setup(m => m.Map<List<ProdutoDto>>(It.IsAny<List<Produto>>()))
                .Returns(listaProdutosDto);
            #endregion

            #region Act
            var retorno = _produtoServico.MostrarListaProduto();
            #endregion

            #region Assert
            Assert.NotNull(retorno);
            Assert.Equal(listaProdutosDto.Count, retorno.Count);
            #endregion
        }


        [Fact]
        public void ApagarProduto_RegrasValidas_RetornaSucesso()
        {
            #region Arrange
            int id = 1;

            mocker.GetMock<IProdutoRepositorio>()
                .Setup(r => r.MostrarProdutos())
                .Returns(new List<Produto>()
                {
                    new Produto { 
                        Id = 1,
                        Nome = "Laranja",
                        Quantidade = 10,
                        Preco = (float?)6.5,
                        NomeFornecedor = "abc",
                        NumeroFornecedor = 16,}
                });
            #endregion

            #region Act
            var retorno = _produtoServico.ApagarProduto(id);
            #endregion

            #region Assert
            Assert.True(retorno);
            #endregion
        }

        [Fact]
        public void ApagarProduto_RegrasInvalidas_RetornaError()
        {
            #region Arrange
            int id = 2;

            mocker.GetMock<IProdutoRepositorio>()
                .Setup(r => r.MostrarProdutos())
                .Returns(new List<Produto>()
                {
                    new Produto {
                        Id = 1,
                        Nome = "Laranja",
                        Quantidade = 10,
                        Preco = (float?)6.5,
                        NomeFornecedor = "abc",
                        NumeroFornecedor = 16,}
                });
            #endregion
            #region Act & Assert
            Assert.Throws<Exception>(() => _produtoServico.ApagarProduto(id));
            #endregion
        }


        [Fact]
        public void AtualizarInfoProduto_RegrasValidas_RetornaSucesso()
        {
            #region Arrange
            var produtoExistente = new Produto
            {
                Id = 1,
                Nome = "Laranja",
                Quantidade = 10,
                Preco = 6.5f,
                NomeFornecedor = "abc",
                NumeroFornecedor = 16
            };

            var produtoAtualizado = new Produto
            {
                Id = 1,
                Nome = "Maca",
                Quantidade = 20,
                Preco = 8.5f,
                NomeFornecedor = "def",
                NumeroFornecedor = 20
            };

            mocker.GetMock<IProdutoRepositorio>()
                  .Setup(r => r.MostrarProdutos())
                  .Returns(new List<Produto> { produtoExistente });

            mocker.GetMock<IProdutoRepositorio>()
                  .Setup(r => r.AtualizarInforParteProduto(It.IsAny<Produto>()))
                  .Returns(true);
            #endregion

            #region Act
            var retorno = _produtoServico.AtualizarInfoProduto(produtoAtualizado);
            #endregion

            #region Assert
            Assert.True(retorno);
            #endregion
        }

        [Fact]
        public void AtualizarInfoProduto_RegrasInvalidas_RetornaError()
        {
            #region Arrange
            var produtoExistente = new Produto
            {
                Id = 1,
                Nome = "Laranja",
                Quantidade = 10,
                Preco = 6.5f,
                NomeFornecedor = "abc",
                NumeroFornecedor = 16
            };

            var produtoAtualizado = new Produto
            {
                Id = 1,
                Nome = "maca",
                Quantidade = -20,
                Preco = -8.5f,
                NomeFornecedor = "bosta",
                NumeroFornecedor = -20
            };

            mocker.GetMock<IProdutoRepositorio>()
                  .Setup(r => r.MostrarProdutos())
                  .Returns(new List<Produto> { produtoExistente });

            mocker.GetMock<IProdutoRepositorio>()
                  .Setup(r => r.AtualizarInforParteProduto(It.IsAny<Produto>()))
                  .Returns(true);
            #endregion

            #region Act & Assert
            Assert.Throws<Exception>(() => _produtoServico.AtualizarInfoProduto(produtoAtualizado));
            #endregion
        }
        [Fact]
        public void AtualizarInfoParteProduto_RegrasValidas_RetornaSucesso()
        {
            #region Arrange
            var produtoTeste = new Produto
            {
                Id = 1,
                Nome = "Laranja",
                Quantidade = 10,
                Preco = (float?)6.5,
                NomeFornecedor = "abc",
                NumeroFornecedor = 16,
            };
            string atributo = "Nome";
            var produtoAtualizado = new Produto
            {
                Id = 1,
                Nome = "Pera",
                Quantidade = 10,
                Preco = (float?)6.5,
                NomeFornecedor = "abc",
                NumeroFornecedor = 16,
            };

            mocker.GetMock<IProdutoRepositorio>()
                  .Setup(r => r.MostrarProdutos())
                  .Returns(new List<Produto> { produtoTeste });

            mocker.GetMock<IProdutoRepositorio>()
                  .Setup(r => r.AtualizarParteProduto(It.IsAny<Produto>(), It.IsAny<string>()))
                  .Returns(true);

            mocker.GetMock<IProdutoRepositorio>()
                  .Setup(r => r.ObterAtributosPermitidos())
                  .Returns(new List<string> { "nome" });
            #endregion

            #region Act
            var retorno = _produtoServico.AtualizarInforParteProduto(produtoAtualizado, atributo);
            #endregion

            #region Assert
            Assert.True(retorno);
            #endregion
        }

        [Fact]
        public void AtualizarInfoParteProduto_AtributoInvalido_RetornaErro()
        {
            #region Arrange
            var produtoTeste = new Produto
            {
                Id = 1,
                Nome = "laranja",
                Quantidade = 10,
                Preco = (float?)6.5,
                NomeFornecedor = "abc",
                NumeroFornecedor = 16,
            };
            string atributoInvalido = "atributoInvalido";

            mocker.GetMock<IProdutoRepositorio>()
                .Setup(r => r.MostrarProdutos())
                .Returns(new List<Produto> { produtoTeste });

            mocker.GetMock<IProdutoRepositorio>()
                .Setup(r => r.ObterAtributosPermitidos())
                .Returns(new List<string> { "nome", "quantidade", "preco", "nomefornecedor", "numerofornecedor" });
            #endregion

            #region Act & Assert
            var ex = Assert.Throws<Exception>(() => _produtoServico.AtualizarInforParteProduto(produtoTeste, atributoInvalido));
            Assert.Equal($"O atributo {atributoInvalido} não é permitido", ex.Message);
            #endregion
        }
    }
}