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
    public class FuncionarioServicoTeste
    {
        private readonly FuncionarioServico _funcionarioServico;
        private readonly AutoMocker mocker = new();
        public FuncionarioServicoTeste()
        {
            _funcionarioServico = mocker.CreateInstance<FuncionarioServico>();
        }

        [Fact]
        public void CadastrarFuncionario_RegrasValidas_RetornaSucesso()
        {
            #region Arrange
            var funcionarioTeste = new Funcionario
            {
                Id = 1,
                Nome = "Laranja",
                Cargo = "estagio",
                CPF = 65

            };
            #endregion

            #region Act
            var retorno = _funcionarioServico.CadastrarFuncionario(funcionarioTeste);
            #endregion

            #region Assert
            Assert.True(retorno);
            #endregion
        }

        [Fact]
        public void CadastrarFuncionario_RegrasInvalidas_RetornaError()
        {
            #region Arrange
            var funcionarioTeste = new Funcionario
            {
                Id = 1,
                Nome = "luan",
                Cargo = "bosta",
                CPF = -85,
            };
            #endregion

            #region Act & Assert
            Assert.Throws<Exception>(() => _funcionarioServico.CadastrarFuncionario(funcionarioTeste));
            #endregion

        }
        [Fact]
        public void MostrarListaFuncionario_RegrasValidas_RetornaSucessoOuNull()
        {
            #region Arrange
            var listaFuncionarios = new List<Funcionario>
    {
        new Funcionario {
            Id = 1,
            Nome = "Luan",
            Cargo = "estagio",
            CPF = 65
            
        }
    };

            var listaFuncionariosDto = new List<FuncionarioDto>
    {
        new FuncionarioDto {
            Id = 1,
            Nome = "Luan",
            Cargo = "estagio",

        }
    };

            mocker.GetMock<IFuncionarioRepositorio>()
                .Setup(r => r.MostrarFuncionarios())
                .Returns(listaFuncionarios);

            mocker.GetMock<IMapper>()
                .Setup(m => m.Map<List<FuncionarioDto>>(It.IsAny<List<Funcionario>>()))
                .Returns(listaFuncionariosDto);
            #endregion

            #region Act
            var retorno = _funcionarioServico.MostrarListaFuncionario();
            #endregion

            #region Assert
            Assert.NotNull(retorno);
            Assert.Equal(listaFuncionariosDto.Count, retorno.Count);
            #endregion
        }


        [Fact]
        public void ApagarFuncionario_RegrasValidas_RetornaSucesso()
        {
            #region Arrange
            int id = 1;

            mocker.GetMock<IFuncionarioRepositorio>()
                .Setup(r => r.MostrarFuncionarios())
                .Returns(new List<Funcionario>()
                {
                    new Funcionario {
                        Id = 1,
                        Nome = "Luan",
                        Cargo = "estagio",
                        CPF = 65,
                        }
                });
            #endregion

            #region Act
            var retorno = _funcionarioServico.ApagarFuncionario(id);
            #endregion

            #region Assert
            Assert.True(retorno);
            #endregion
        }

        [Fact]
        public void ApagarFuncionario_RegrasInvalidas_RetornaError()
        {
            #region Arrange
            int id = 2;

            mocker.GetMock<IFuncionarioRepositorio>()
                .Setup(r => r.MostrarFuncionarios())
                .Returns(new List<Funcionario>()
                {
                    new Funcionario {
                        Id = 1,
                        Nome = "Luan",
                        Cargo = "estagio",
                        CPF = 65,
                    }
                });
            #endregion
            #region Act & Assert
            Assert.Throws<Exception>(() => _funcionarioServico.ApagarFuncionario(id));
            #endregion
        }


        [Fact]
        public void AtualizarInfoFuncionario_RegrasValidas_RetornaSucesso()
        {
            #region Arrange
            var funcionarioExistente = new Funcionario
            {
                Id = 1,
                Nome = "Luan",
                Cargo = "estagio",
                CPF = 65
            };

            var funcionarioAtualizado = new Funcionario
            {
                Id = 1,
                Nome = "Tadeu",
                Cargo = "dev",
                CPF = 8123125
            };

            mocker.GetMock<IFuncionarioRepositorio>()
                  .Setup(r => r.MostrarFuncionarios())
                  .Returns(new List<Funcionario> { funcionarioExistente });

            mocker.GetMock<IFuncionarioRepositorio>()
                  .Setup(r => r.AtualizarInforParteFuncionario(It.IsAny<Funcionario>()))
                  .Returns(true);
            #endregion

            #region Act
            var retorno = _funcionarioServico.AtualizarInfoFuncionario(funcionarioAtualizado);
            #endregion

            #region Assert
            Assert.True(retorno);
            #endregion
        }

        [Fact]
        public void AtualizarInfoFuncionario_RegrasInvalidas_RetornaError()
        {
            #region Arrange
            var funcionarioExistente = new Funcionario
            {
                Id = 1,
                Nome = "Luan",
                Cargo = "estagio",
                CPF = 65
                
            };

            var funcionarioAtualizado = new Funcionario
            {
                Id = 1,
                Nome = "luan",
                Cargo = "bosta",
                CPF = -85
               
            };

            mocker.GetMock<IFuncionarioRepositorio>()
                  .Setup(r => r.MostrarFuncionarios())
                  .Returns(new List<Funcionario> { funcionarioExistente });

            mocker.GetMock<IFuncionarioRepositorio>()
                  .Setup(r => r.AtualizarInforParteFuncionario(It.IsAny<Funcionario>()))
                  .Returns(true);
            #endregion

            #region Act & Assert
            Assert.Throws<Exception>(() => _funcionarioServico.AtualizarInfoFuncionario(funcionarioAtualizado));
            #endregion
        }
        [Fact]
        public void AtualizarInfoParteFuncionario_RegrasValidas_RetornaSucesso()
        {
            #region Arrange
            var funcionarioTeste = new Funcionario
            {
                Id = 1,
                Nome = "Luan",
                Cargo = "estagio",
                CPF = 65
            };
            string atributo = "Nome";
            var funcionarioAtualizado = new Funcionario
            {
                Id = 1,
                Nome = "Luan",
                Cargo = "estagio",
                CPF = 65
              
            };

            mocker.GetMock<IFuncionarioRepositorio>()
                  .Setup(r => r.MostrarFuncionarios())
                  .Returns(new List<Funcionario> { funcionarioTeste });

            mocker.GetMock<IFuncionarioRepositorio>()
                  .Setup(r => r.AtualizarParteFuncionario(It.IsAny<Funcionario>(), It.IsAny<string>()))
                  .Returns(true);

            mocker.GetMock<IFuncionarioRepositorio>()
                  .Setup(r => r.ObterAtributosPermitidos())
                  .Returns(new List<string> { "nome" });
            #endregion

            #region Act
            var retorno = _funcionarioServico.AtualizarInforParteFuncionario(funcionarioAtualizado, atributo);
            #endregion

            #region Assert
            Assert.True(retorno);
            #endregion
        }

        [Fact]
        public void AtualizarInfoParteFuncionario_AtributoInvalido_RetornaErro()
        {
            #region Arrange
            var funcionarioTeste = new Funcionario
            {
                Id = 1,
                Nome = "luan",
                Cargo = "estagio",
                CPF = 65
            };
            string atributoInvalido = "atributoInvalido";

            mocker.GetMock<IFuncionarioRepositorio>()
                .Setup(r => r.MostrarFuncionarios())
                .Returns(new List<Funcionario> { funcionarioTeste });

            mocker.GetMock<IFuncionarioRepositorio>()
                .Setup(r => r.ObterAtributosPermitidos())
                .Returns(new List<string> { "nome", "cargo", "cpf", });
            #endregion

            #region Act & Assert
            var ex = Assert.Throws<Exception>(() => _funcionarioServico.AtualizarInforParteFuncionario(funcionarioTeste, atributoInvalido));
            Assert.Equal($"O atributo {atributoInvalido} não é permitido", ex.Message);
            #endregion
        }
    }
}