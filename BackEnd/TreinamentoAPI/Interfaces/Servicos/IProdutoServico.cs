using TreinamentoAPI.DTOs;
using TreinamentoAPI.Entidades;

namespace TreinamentoAPI.Interfaces.Servicos
{
    public interface IProdutoServico
    {
         bool CadastrarProduto(Produto produto);
          List<ProdutoDto> MostrarListaProduto();
         bool ApagarProduto(int id);
         bool AtualizarInfoProduto(Produto novoProduto);
         bool AtualizarInforParteProduto(Produto produtoAtualizado, string atributo);
        List<Produto> PegaMaiorQue5();
        Produto? UsoDoFirst();
        List<string> ObterNomesDosProdutos();
        List<Produto> OrdenarProdutosPorQuantidade();
        List<int?> ObterQuantidadesUnicas();
        List<string> FiltrarNomesPorLetra(char letra);
        bool AtributoEhPermitido(string atributo);
    }
}
