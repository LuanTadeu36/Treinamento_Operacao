namespace TreinamentoAPI.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int ?Quantidade { get; set; }

        public float? Preco { get; set; }
        public string NomeFornecedor { get; set; } = string.Empty;
        public int? NumeroFornecedor { get; set; }
    }
}
