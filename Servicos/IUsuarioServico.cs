using NetWebApi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetWebApi.Servicos
{
    public interface IUsuarioServico
    {
        Task<IEnumerable<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuario(int id);
        Task<IEnumerable<Usuario>> GetUsuariosByName(string nome);
        Task CreateUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(Usuario usuario);
        Task<IEnumerable<Usuario>> FazerLogin(string nome, string senha);

    }
}
