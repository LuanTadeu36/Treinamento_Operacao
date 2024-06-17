using AutoMapper;
using System;
using TreinamentoAPI.DTOs;
using TreinamentoAPI.Entidades;
using TreinamentoAPI.Exceções;
using TreinamentoAPI.Interfaces.Bancos;
using TreinamentoAPI.Interfaces.Servicos;
using TreinamentoAPI.Repositorios;
using TreinamentoAPI.Validacoes;

namespace TreinamentoAPI.Servicos
{
    public class FuncionarioServico : IFuncionarioServico
    {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IMapper _mapper;

        public FuncionarioServico(IFuncionarioRepositorio funcionarioRepositorio, IMapper mapper)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _mapper = mapper;

        }


        public bool CadastrarFuncionario(Funcionario funcionario)
        {
            TratamentoExcessao error = new TratamentoExcessao();
            var validacao = new FuncionarioValidacao().Validate(funcionario);

           if (validacao.IsValid)
            {
                _funcionarioRepositorio.AdicionarFuncionario(funcionario);
                return true;
            }
            else
            {
                error.MakeErrorString(validacao.Errors.Select(e => e.ErrorMessage));
            }
            return false;
        }
        public List<FuncionarioDto> MostrarListaFuncionario()
        {
            var funcionarios = _funcionarioRepositorio.MostrarFuncionarios();
            return _mapper.Map<List<FuncionarioDto>>(funcionarios);
        }

        public bool ApagarFuncionario(int id)
        {
            var funcionario = _funcionarioRepositorio.MostrarFuncionarios().FirstOrDefault(p => p.Id == id);
            if (funcionario != null)
            {
                _funcionarioRepositorio.RemoverFuncionario(funcionario);
                return true;
            }
            else
            {
                throw new Exception("O ID solicitado não existe");

            }
        }

        public bool AtualizarInfoFuncionario(Funcionario novoFuncionario)
        {

            TratamentoExcessao error = new TratamentoExcessao();
            var funcionarioExistente = _funcionarioRepositorio.MostrarFuncionarios().FirstOrDefault(p => p.Id == novoFuncionario.Id);
            if (funcionarioExistente == null)
            {
                throw new Exception("O ID solicitado não existe");
            }

            var validacao = new FuncionarioValidacao().Validate(novoFuncionario);

            if (validacao.IsValid)
            {
                _funcionarioRepositorio.AtualizarInforParteFuncionario(novoFuncionario);
                return true;
            }
            else
            {
                error.MakeErrorString(validacao.Errors.Select(e => e.ErrorMessage));
                return false;
            }
        }

        public bool AtualizarInforParteFuncionario(Funcionario funcionarioAtualizado, string atributo)
        {
            TratamentoExcessao error = new TratamentoExcessao();
            var funcionarioExistente = _funcionarioRepositorio.MostrarFuncionarios().FirstOrDefault(p => p.Id == funcionarioAtualizado.Id);
            if (funcionarioExistente == null)
            {
                throw new Exception("O ID solicitado não existe");
            }

            if (!AtributoEhPermitido(atributo))
            {
               throw new Exception($"O atributo {atributo} não é permitido");
            }


            var validacao = new FuncionarioValidacao().Validate(funcionarioAtualizado);

            if (validacao.IsValid)
            {
                _funcionarioRepositorio.AtualizarParteFuncionario(funcionarioAtualizado, atributo);
                return true;
            }
            else
            {
                error.MakeErrorString(validacao.Errors.Select(e => e.ErrorMessage));
                return false;
            }

        }

        public List<string> ObterAtributosPermitidos()
        {
            return _funcionarioRepositorio.ObterAtributosPermitidos();
        }

        public bool AtributoEhPermitido(string atributo)
        {
            var atributosPermitidos = _funcionarioRepositorio.ObterAtributosPermitidos();
            return atributosPermitidos.Contains(atributo.ToLower());
        }


    }
}
