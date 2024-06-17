using TreinamentoAPI.Entidades;

namespace TreinamentoAPI.Interfaces.Bancos
{
    public interface IFuncionarioRepositorio
    {

        void AdicionarFuncionario(Funcionario funcionario);
        void RemoverFuncionario(Funcionario funcionario);
        List<Funcionario> MostrarFuncionarios();
        bool AtualizarInforParteFuncionario(Funcionario novoFuncionario);
        bool AtualizarParteFuncionario(Funcionario funcionarioAtualizado, string atributo);

        List<string> ObterAtributosPermitidos();

    }
}
