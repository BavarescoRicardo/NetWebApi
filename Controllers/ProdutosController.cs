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

        [HttpGet("FiltroNome")]
        public async Task<ActionResult<IAsyncEnumerable<Produto>>> GetProdutosByName([FromQuery] string nome)
        {
            try
            {
                var produtos = await _produtoServico.GetProdutosByName(nome);

                if (produtos == null)
                    return NotFound($"Nome não localizado");
                return Ok(produtos);
            }
            catch
            {
                // return BadRequest("Request invalido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }


        [HttpGet("{id:int}", Name = "GetProduto")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            try
            {
                var produtos = await _produtoServico.GetProduto(id);

                if (produtos == null)
                    return NotFound($"Codigo não localizado");
                return Ok(produtos);
            }
            catch
            {
                return BadRequest("Request invalido");
            }
        }
 
        // Inserir novo registro // criar
        [HttpPost]
        public async Task<ActionResult> Create(Produto produto)
        {
            try
            {
                await _produtoServico.CreateProduto(produto);
                return CreatedAtRoute(nameof(GetProduto), new { id = produto.Id }, produto);
            }
            catch
            {
                return BadRequest("Request invalido");
            }
        }

        // Autalizar novo registro // criar
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Produto produto)
        {
            try
            {
                if(produto.Id == id)
                {
                    await _produtoServico.UpdateProduto(produto);
                    return Ok($"Produto do codigo {id} foi atualizado");
                }
                else
                {
                    return BadRequest("Dados não batem");
                }
            }
            catch
            {
                return BadRequest("Request invalido");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var produto = await _produtoServico.GetProduto(id);
                if(produto != null)
                {
                    await _produtoServico.DeleteProduto(produto);
                    return Ok($"Apagado Produto do codigo: {id}");
                }
                else
                {
                    return BadRequest("id não encontrado");
                }
            }
            catch
            {
                return BadRequest("Request invalido");
            }
        }





    }
}
