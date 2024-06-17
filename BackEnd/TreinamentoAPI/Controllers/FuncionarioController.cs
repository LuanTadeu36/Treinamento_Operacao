using Microsoft.AspNetCore.Mvc;
using TreinamentoAPI.DTOs;
using TreinamentoAPI.Entidades;
using TreinamentoAPI.Interfaces.Servicos;
using TreinamentoAPI.Servicos;

namespace TreinamentoAPI.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly ILogger<FuncionarioController> _logger;

        private readonly IFuncionarioServico _funcionarioServico;

        public FuncionarioController(ILogger<FuncionarioController> logger, IFuncionarioServico funcionarioServico)
        {
            _logger = logger;
            _funcionarioServico = funcionarioServico;
        }


        [HttpGet(Name = "GetFuncionario")]
        public ActionResult<List<FuncionarioDto>> Get()
        {
            var funcionarios = _funcionarioServico.MostrarListaFuncionario();
            return Ok(funcionarios);
        }

        [HttpPost(Name = "PostFuncionario")]
        public IActionResult Post([FromBody] Funcionario funcionario)
        {
            _funcionarioServico.CadastrarFuncionario(funcionario);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteFuncionario")]
        public IActionResult Delete(int id)
        {
            _funcionarioServico.ApagarFuncionario(id);
            return Ok();
        }


        [HttpPut(Name = "PutFuncionario")]
        public IActionResult Put([FromBody] Funcionario novoFuncionario)
        {

            _funcionarioServico.AtualizarInfoFuncionario(novoFuncionario);
            return Ok();
        }



        [HttpPatch(Name = "PatchFuncionario")]
        public IActionResult Patch([FromQuery]string atributo,[FromBody] Funcionario funcionarioAtualizado)
        {

            _funcionarioServico.AtualizarInforParteFuncionario(funcionarioAtualizado,atributo);
            return Ok();
        }


    }
}
