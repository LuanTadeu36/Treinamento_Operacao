using TreinamentoAPI.Entidades;
using TreinamentoAPI.Servicos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TreinamentoAPI.Repositorios;
using TreinamentoAPI.Interfaces.Bancos;
using TreinamentoAPI.Interfaces.Servicos;
using TreinamentoAPI.DTOs;

namespace TreinamentoAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProdutoController : ControllerBase
    {
        //PARTE 2 - INJEÇÃO DE DEPENDENCIAS
        private readonly ILogger<ProdutoController> _logger;

        private readonly IProdutoServico _produtoServico;


        public ProdutoController(ILogger<ProdutoController> logger, IProdutoServico produtoServico)
        {
            _logger = logger;
            _produtoServico = produtoServico;
        }




        //PARTE 1 - REQUISIÇÕES HTTP CRUD


        [HttpGet(Name = "GetProduto")]
        public ActionResult<List<ProdutoDto>> Get()
        {
            var produtos = _produtoServico.MostrarListaProduto();
            return Ok(produtos);
        }



        [HttpPost(Name = "PostProduto")]
        public IActionResult Post([FromBody] Produto produto)
        {
            _produtoServico.CadastrarProduto(produto);
            return Ok();
        }


        [HttpDelete("{id}", Name = "DeleteProduto")]
        public IActionResult Delete(int id)
        {
            _produtoServico.ApagarProduto(id);
            return Ok();
        }


        [HttpPut(Name = "PutProduto")]
        public IActionResult Put([FromBody] Produto novoProduto)
        { 
             _produtoServico.AtualizarInfoProduto(novoProduto);
             return Ok();
            
        }



        [HttpPatch(Name = "PatchProduto")]
        public IActionResult Patch([FromQuery]string atributo, [FromBody] Produto produtoAtualizado)
        {
            
            _produtoServico.AtualizarInforParteProduto(produtoAtualizado,atributo);
            return Ok();
        }
    

    //PARTE 3 - DESAFIO: METODO EXTRA


        [HttpGet("PegaMaiorQue5", Name = "MaiorQue5")]
        public IActionResult MaiorQue5()
        {
        var produtos = _produtoServico.PegaMaiorQue5();
        return Ok(produtos);
        }

        [HttpGet("UsoDoFirst", Name = "UsoDoFirst")]
        public IActionResult UsoDoFirst()
        {
            var produto = _produtoServico.UsoDoFirst();
           
            return Ok(produto);
        }

        [HttpGet("ObterNomesDosProdutos", Name = "ObterNomesDosProdutos")]  
        public IActionResult ObterNomesDosProdutos()
        {
            var nomes = _produtoServico.ObterNomesDosProdutos();
            return Ok(nomes);
        }

        [HttpGet("OrdenarProdutosPorQuantidade", Name = "OrdenarProdutosPorQuantidade")]
        public IActionResult OrdenarProdutosPorQuantidade()
        {
            var produtos = _produtoServico.OrdenarProdutosPorQuantidade();
            return Ok(produtos);
        }

        [HttpGet("FiltrarNomesPorLetra/{letra}", Name = "FiltrarNomesPorLetra")]
        public IActionResult FiltrarNomesPorLetra(char letra)
        {
            var nomes = _produtoServico.FiltrarNomesPorLetra(letra);
            return Ok(nomes);
        }

        [HttpGet("ObterQuantidadesUnicas", Name = "ObterQuantidadesUnicas")]
        public IActionResult ObterQuantidadesUnicas()
        {
            var quantidadesUnicas = _produtoServico.ObterQuantidadesUnicas();
            return Ok(quantidadesUnicas);
        }


    }
}
    







