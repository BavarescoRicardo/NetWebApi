using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<dynamic>> FazerLogin([FromBody] Usuario user)
        {
            try
            {
                var login = await _usuarioServico.FazerLogin(user);
                // tenta converter o objeto retornado pelo banco para o modelo que eu posso usar
                foreach (Usuario usuario in login)
                {
                    if (usuario.Id > 0)
                        user = usuario;
                }
                // Verifica se o usuário existe
                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                // Gera o Token
                var token = TokenService.GenerateToken(user);

                // Oculta a senha
                user.Senha = "";

                // Retorna os dados
                return new
                {
                    user = user,
                    token = token
                };
            }
            catch (Exception ex)
            {
                return BadRequest("Usuário ou senha inválidos " + ex.Message); // ou NotFound(nao encontrado apelido e senha)
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

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "Autorizado")]
        public string Employee() => "Autorizado";

        [HttpGet]
        [Route("Gestor")]
        [Authorize(Roles = "Gestor")]
        public string Manager() => "Gestor";

        [HttpGet]
        [Route("Administrador")]
        [Authorize(Roles = "Administrador")]
        public string Testeapi() => "Deu certo ";
    }
}
