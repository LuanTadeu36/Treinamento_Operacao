using TreinamentoAPI.Entidades;

namespace TreinamentoAPI.Interfaces.Bancos
{
    public interface IProdutoRepositorio
    {
        void AdicionarProduto(Produto produto);
        void RemoverProduto(Produto produto);
        List<Produto> MostrarProdutos();
        bool AtualizarInforParteProduto(Produto novoProduto);
       bool AtualizarParteProduto(Produto produtoAtualizado, string atributo);
        List<string> ObterAtributosPermitidos();
    }
}
