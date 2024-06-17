using System;
using System.Collections.Generic;
using System.Linq;
using TreinamentoAPI.Entidades;
using TreinamentoAPI.Interfaces.Bancos;

namespace TreinamentoAPI.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        public static List<Produto> Produtos = new List<Produto>();

        public void AdicionarProduto(Produto produto)
        {
            Produtos.Add(produto);
        }

        public void RemoverProduto(Produto produto)
        {
            Produtos.Remove(produto);
        }

        public List<Produto> MostrarProdutos()
        {
            return Produtos;
        }

        public bool AtualizarInforParteProduto(Produto novoProduto)
        {
            var produto = Produtos.FirstOrDefault(pro => pro.Id == novoProduto.Id);

            if (produto != null)
            {
                produto.Nome = novoProduto.Nome;
                produto.Quantidade = novoProduto.Quantidade;
                produto.Preco = novoProduto.Preco;
                produto.NomeFornecedor = novoProduto.NomeFornecedor;
                produto.NumeroFornecedor = novoProduto.NumeroFornecedor;
            }
            return true;
        }

        public bool AtualizarParteProduto(Produto produtoAtualizado, string atributo)
        {
            var produtoExistente = MostrarProdutos().FirstOrDefault(p => p.Id == produtoAtualizado.Id);

            if (produtoExistente == null)
            {
                return false;
            }
                switch (atributo.ToLower())
                {
                    case "nome":
                        if (!string.IsNullOrEmpty(produtoAtualizado.Nome))
                        {
                            produtoExistente.Nome = produtoAtualizado.Nome;
                        }
                        break;
                    case "quantidade":
                        produtoExistente.Quantidade = produtoAtualizado.Quantidade;
                        break;
                    case "preco":
                        produtoExistente.Preco = produtoAtualizado.Preco;
                        break;
                    case "nomeFornecedor":
                        produtoExistente.NomeFornecedor = produtoAtualizado.NomeFornecedor;
                        break;
                    case "numeroFornecedor":
                        produtoExistente.NumeroFornecedor = produtoAtualizado.NumeroFornecedor;
                        break;
                }

                return true;
            
           
        }

        public List<string> ObterAtributosPermitidos()
        {
            return new List<string> { "nome", "quantidade", "preco", "nomeFornecedor", "numeroFornecedor" };
        }
    }
}
