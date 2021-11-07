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

    public class UsuarioController : ControllerBase
    {
        private IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Usuario>>> GetUsuarios()
        {
            try
            {
                var Usuarios = await _usuarioServico.GetUsuarios();
                return Ok(Usuarios);
            }
            catch
            {
                // return BadRequest("Request invalido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }

        [HttpGet("FiltroNome")]
        public async Task<ActionResult<IAsyncEnumerable<Usuario>>> GetUsuariosByName([FromQuery] string nome)
        {
            try
            {
                var Usuarios = await _usuarioServico.GetUsuariosByName(nome);

                if (Usuarios == null)
                    return NotFound($"Nome não localizado");
                return Ok(Usuarios);
            }
            catch
            {
                // return BadRequest("Request invalido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }


        [HttpGet("{id:int}", Name = "GetUsuario")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            try
            {
                var Usuarios = await _usuarioServico.GetUsuario(id);

                if (Usuarios == null)
                    return NotFound($"Codigo não localizado");
                return Ok(Usuarios);
            }
            catch
            {
                return BadRequest("Request invalido");
            }
        }

        [HttpPost("fazerlogin")]
        public async Task<ActionResult<Usuario>> FazerLogin(string nome, string senha)
        {
            try
            {
                var login = await _usuarioServico.FazerLogin(nome, senha);
                return Ok(login);
            }
            catch (Exception)
            {

                return BadRequest("Erro no login");
            }

        }

        // Inserir novo registro // criar
        [HttpPost]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            try
            {
                await _usuarioServico.CreateUsuario(usuario);
                return CreatedAtRoute(nameof(GetUsuario), new { id = usuario.Id }, usuario);
            }
            catch
            {
                return BadRequest("Request invalido");
            }
        }

        // Autalizar novo registro // criar
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (usuario.Id == id)
                {
                    await _usuarioServico.UpdateUsuario(usuario);
                    return Ok($"Usuario do codigo {id} foi atualizado");
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
                var produto = await _usuarioServico.GetUsuario(id);
                if (produto != null)
                {
                    await _usuarioServico.DeleteUsuario(produto);
                    return Ok($"Apagado Usuario do codigo: {id}");
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
