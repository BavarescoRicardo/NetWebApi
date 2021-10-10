using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetWebApi.Modelos;
using NetWebApi.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private IProdutoServico _produtoServico;

        public ProdutosController(IProdutoServico produtoServico)
        {
            _produtoServico = produtoServico;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Produto>>> GetProdutos()
        {
            try
            {
                var produtos = await _produtoServico.GetProdutos();
                return Ok(produtos);
            }
            catch
            {
                // return BadRequest("Request invalido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }
    }
}
