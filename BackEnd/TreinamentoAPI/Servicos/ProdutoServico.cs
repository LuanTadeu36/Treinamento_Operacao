using AutoMapper;
using System;
using TreinamentoAPI.DTOs;
using TreinamentoAPI.Entidades;
using TreinamentoAPI.Exceções;
using TreinamentoAPI.Interfaces.Bancos;
using TreinamentoAPI.Interfaces.Servicos;
using TreinamentoAPI.Repositorios;
using TreinamentoAPI.Validacoes;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TreinamentoAPI.Servicos
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IMapper _mapper;


        public ProdutoServico(IProdutoRepositorio produtoRepositorio, IMapper mapper )
        {
            _produtoRepositorio = produtoRepositorio;
            _mapper = mapper;
        }
       
        public bool CadastrarProduto(Produto produto)
        {
            TratamentoExcessao error = new TratamentoExcessao();
            var validacao = new ProdutoValidacao().Validate(produto);

            if(validacao.IsValid) 
              {
                _produtoRepositorio.AdicionarProduto(produto);
                return true;
              }
            else
            {
                error.MakeErrorString(validacao.Errors.Select(e => e.ErrorMessage));
            }

            return false;
            
        }
        public  List<ProdutoDto> MostrarListaProduto()
        {
            var produtos = _produtoRepositorio.MostrarProdutos();
            return _mapper.Map<List<ProdutoDto>>(produtos);

            
        }

        public bool ApagarProduto(int id)
        {

            var produto = _produtoRepositorio.MostrarProdutos().FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                _produtoRepositorio.RemoverProduto(produto);
                return true;
            }
            else
            {
                throw new Exception("O ID solicitado não existe");

            }

           
        }

        public bool AtualizarInfoProduto(Produto novoProduto)
        {
            TratamentoExcessao error = new TratamentoExcessao();
            var produtoExistente = _produtoRepositorio.MostrarProdutos().FirstOrDefault(p => p.Id == novoProduto.Id);
            if (produtoExistente == null)
            {
                throw new Exception("O ID solicitado não existe");
            }

            var validacao = new ProdutoValidacao().Validate(novoProduto);

            if (validacao.IsValid)
            {
                _produtoRepositorio.AtualizarInforParteProduto(novoProduto);
                return true;
            }
            else
            {
                error.MakeErrorString(validacao.Errors.Select(e => e.ErrorMessage));
                return false;

            }

        }

        public bool AtributoEhPermitido(string atributo)
        {
            var atributosPermitidos = _produtoRepositorio.ObterAtributosPermitidos();
            return atributosPermitidos.Contains(atributo.ToLower());
        }

        public bool AtualizarInforParteProduto(Produto produtoAtualizado, string atributo)
        {
            TratamentoExcessao error = new TratamentoExcessao();
            var produtoExistente = _produtoRepositorio.MostrarProdutos().FirstOrDefault(p => p.Id == produtoAtualizado.Id);
            if (produtoExistente == null)
            {
                    throw new Exception("O ID solicitado não existe");
            }

            if (!AtributoEhPermitido(atributo))
            {
                throw new Exception($"O atributo {atributo} não é permitido");
                
            }


            var validacao = new ProdutoValidacao().Validate(produtoAtualizado);

            if (validacao.IsValid)
            {
                _produtoRepositorio.AtualizarParteProduto(produtoAtualizado, atributo);
                return true;
            }
            else
            {
                error.MakeErrorString(validacao.Errors.Select(e => e.ErrorMessage));
                return false;
            }
            
        }

        public List<Produto> PegaMaiorQue5()
        {
            return _produtoRepositorio.MostrarProdutos().Where(produto => produto.Quantidade >= 5).ToList();
        }

        public Produto? UsoDoFirst()
        {
            return _produtoRepositorio.MostrarProdutos().FirstOrDefault(produto => produto.Preco > 50);
        }

        public List<string> ObterNomesDosProdutos()
        {
            return _produtoRepositorio.MostrarProdutos().Select(produto => produto.Nome).ToList();
        }

        public List<Produto> OrdenarProdutosPorQuantidade()
        {
            return _produtoRepositorio.MostrarProdutos().OrderBy(p => p.Quantidade).ToList();
        }

        public List<string> FiltrarNomesPorLetra(char letra)
        {
            return _produtoRepositorio.MostrarProdutos().Where(p => p.Nome.StartsWith("L", StringComparison.OrdinalIgnoreCase)).Select(p => p.Nome).ToList();
        }

        public List<int?> ObterQuantidadesUnicas()
        {
            return _produtoRepositorio.MostrarProdutos().Select(p => p.Quantidade).Distinct().ToList();
        }

        public List<string> ObterAtributosPermitidos()
        {
            return _produtoRepositorio.ObterAtributosPermitidos();
        }

    

    }
}
