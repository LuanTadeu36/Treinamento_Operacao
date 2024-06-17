using FluentValidation;
using TreinamentoAPI.Entidades;
using TreinamentoAPI.Interfaces.Bancos;

namespace TreinamentoAPI.Validacoes
{
    public class ProdutoValidacao : AbstractValidator<Produto>
    {

        bool ValidarNome(string Nome)
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                return false;
            }

            char primeiraLetra = Nome[0];
            if (Char.IsUpper(primeiraLetra) && Nome.Length > 1 && Nome.All(c => Char.IsLetter(c) || c == ' '))
            {
                return true;
            }

            return false;
        }

        bool ValidarQuantidade(int? Quantidade)
        {
            if(Quantidade.HasValue && Quantidade >= 0)
            {
                return true;
            }

            return false;
        }

        bool ValidarPreco(float? Preco)
        {
            if(Preco.HasValue && Preco >= 0)
            {
                return true;
            }

            return false;
        }

        List<string> proibidas = new List<string> { "bosta", "otario", "trouxa", "sujo", "ruim" };

        bool ValidarNomeFornecedor(string NomeFornecedor)
        {
            if(NomeFornecedor.Length > 1 && !proibidas.Contains(NomeFornecedor))
            {
                return true;
            }

            return false;
        }

        bool ValidarNumeroFornecedor(int? NumeroFornecedor)
        {
            if (NumeroFornecedor.HasValue && NumeroFornecedor >= 0)
            {
                return true;
            }

            return false;
        }

  
        public ProdutoValidacao() 
        {
            RuleFor(a => a.Nome).Must(ValidarNome).WithMessage("A primeira letra do nome deve ser maiúscula e deve conter mais de uma letra");
            RuleFor(b => b.Quantidade).Must(ValidarQuantidade).WithMessage("Apenas valores inteiros e positivos!");

        }

       
    }
}
