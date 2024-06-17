using TreinamentoAPI.DTOs;
using TreinamentoAPI.Entidades;

namespace TreinamentoAPI.Interfaces.Servicos
{
    public interface IFuncionarioServico
    {
        bool CadastrarFuncionario(Funcionario funcionario);
        List<FuncionarioDto> MostrarListaFuncionario();
        bool ApagarFuncionario(int id);
        bool AtualizarInfoFuncionario(Funcionario novoFuncionario);
        bool AtualizarInforParteFuncionario(Funcionario funcionarioAtualizado, string atributo);

        bool AtributoEhPermitido(string atributo);

    }
}
