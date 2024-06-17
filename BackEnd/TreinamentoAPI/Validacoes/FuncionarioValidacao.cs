using FluentValidation;
using TreinamentoAPI.Entidades;

namespace TreinamentoAPI.Validacoes
{
    public class FuncionarioValidacao : AbstractValidator<Funcionario>
    {

        bool ValidarNome(string Nome)
        {
            char primeiraLetra = Nome[0];
            if (Char.IsUpper(primeiraLetra) && Nome.Length > 1 && Nome.All(c => Char.IsLetter(c) || c == ' '))
            {
                return true;
            }


            return false;
        }

        List<string> proibidas = new List<string> { "bosta", "otario", "trouxa", "sujo", "ruim" };

        bool ValidarCargo(string Cargo)
        {
            if (Cargo.Length > 1 && !proibidas.Contains(Cargo))
            {
                return true;
            }

            return false;
        }

        bool ValidarCPF(int CPF)
        {
            if (CPF >= 0)
            {
                return true;
            }

            return false;
        }

        public FuncionarioValidacao()
        {
            RuleFor(a => a.Nome).Must(ValidarNome).WithMessage("A primeira letra do nome deve ser maiúscula e deve conter mais de uma letra");
            RuleFor(b => b.Cargo).Must(ValidarCargo).WithMessage("ASem palavroes");
            RuleFor(c => c.CPF).Must(ValidarCPF).WithMessage("Apenas valores positivos!");

        }

    }
}
