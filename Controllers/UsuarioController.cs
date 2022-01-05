using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetWebApi.Modelos;
using NetWebApi.Servicos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace NetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private IUsuarioServico _usuarioServico;
        public static IWebHostEnvironment _environment;

        public UsuarioController(IUsuarioServico usuarioServico, IWebHostEnvironment environment)
        {
            _usuarioServico = usuarioServico;
            _environment = environment;
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
        [HttpPost("cadastrarnovo")]
        public async Task<ActionResult> Create([FromBody] Usuario usuario)
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

         /*[HttpPost]
         [ValidateAntiForgeryToken]
         [Route("postarfotoperfil")]
        public async Task<IActionResult> InserirFotoPerfil([FromBody] FotoView model)
        {           
            string nomeUnicoArquivo = UploadedFile(model.Foto);
            FotoView foto = new FotoView
            {
                NomeImagem = nomeUnicoArquivo,
                regerenciaadd = model.regerenciaadd,
                refUsuario = model.refUsuario,
            };
            
            return Ok($"Postado imagem nome:  {model.NomeImagem} ");
        }*/
/*
        [HttpPost]
        [Route("postarfotoperfil")]
        public async Task<IActionResult> UploadedFile([FromForm] IFormFile model)
        {
            string nomeUnicoArquivo = null;

            try
            {
                string pastaFotos = Path.Combine("C:Users/Ninguem/Desktop/Desenvolvimento/Evento Web/Net Api/NetWebApi/", "Imagens");
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + model.FileName;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);

                await using (Stream stream = new FileStream(@"file path", FileMode.Create))
                {
                    using (var binaryReader = new BinaryReader(stream))
                    {
                        // Save the file here.
                        model.CopyTo(stream);
                    }
                }
                return Ok($"Postado imagem nome:  {nomeUnicoArquivo} ");
            }
            catch (Exception ex)
            {

                return BadRequest("erro no servidor com a imagem.  " + ex);
            }                        
            
        }

        [HttpPost("uploadperfil")]
        public async Task<string> EnviaArquivo([FromForm] IFormFile arquivo)
        {
            if (arquivo.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\imagens\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\imagens\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\imagens\\" + arquivo.FileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();
                        return "\\imagens\\" + arquivo.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Ocorreu uma falha no envio do arquivo...";
            }
        }*/


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
