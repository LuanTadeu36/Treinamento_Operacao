using System.Security.Cryptography;
using TreinamentoAPI.Entidades;
using TreinamentoAPI.Interfaces.Bancos;

namespace TreinamentoAPI.Repositorios
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        public static List<Funcionario> Funcionarios = new List<Funcionario>();


        public void AdicionarFuncionario(Funcionario funcionario)
        {
            Funcionarios.Add(funcionario);
        }
        public void RemoverFuncionario(Funcionario funcionario)
        {
            Funcionarios.Remove(funcionario);
        }
        public List<Funcionario> MostrarFuncionarios()
        {
            return Funcionarios;
        }

        public bool AtualizarInforParteFuncionario(Funcionario novoFuncionario)
        {
            var funcionario = Funcionarios.FirstOrDefault(pro => pro.Id == novoFuncionario.Id);

            if (funcionario != null)
            {
                funcionario.Nome = novoFuncionario.Nome;
                funcionario.Cargo = novoFuncionario.Cargo;
                funcionario.CPF = novoFuncionario.CPF;

            }
            return true;
        }

        public bool AtualizarParteFuncionario(Funcionario funcionarioAtualizado, string atributo)
        {

            var funcionarioExistente = MostrarFuncionarios().FirstOrDefault(p => p.Id == funcionarioAtualizado.Id);


            if(funcionarioExistente == null)
            {
                return false;
            } 
            
                switch (atributo.ToLower())
                {
                    case "nome":
                        if (!string.IsNullOrEmpty(funcionarioAtualizado.Nome))
                        {
                            funcionarioExistente.Nome = funcionarioAtualizado.Nome;
                        }
                        break;
                    case "cargo":
                        if (!string.IsNullOrEmpty(funcionarioAtualizado.Cargo))
                        {
                            funcionarioExistente.Cargo = funcionarioAtualizado.Cargo;
                        }
                        break;
                    case "cpf":
                        funcionarioExistente.CPF = funcionarioAtualizado.CPF;
                        break;

                }
            return true;
          
        }

        public List<string> ObterAtributosPermitidos()
        {
            return new List<string> { "nome", "cargo","cpf" };
        }
    }
        
}
