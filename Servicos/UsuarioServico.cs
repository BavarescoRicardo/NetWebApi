using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetWebApi.Context;
using NetWebApi.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace NetWebApi.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly NetdbContext _context;

        public UsuarioServico(NetdbContext context)
        {
            _context = context;
        }

        public async Task CreateUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }



        [HttpPost]
        public async Task<IEnumerable<Usuario>> FazerLogin([FromBody] Usuario model)
        {
            try
            {
                IEnumerable<Usuario> login;
                if (!string.IsNullOrWhiteSpace(model.ApelidoLogin))
                {
                    login = await _context.Usuarios.Where(x => x.ApelidoLogin == model.ApelidoLogin).Where(x => x.Senha == model.Senha).ToListAsync();
                }
                else
                {
                    login = null;
                }
                return login;
            }
            catch (Exception )
            {
                return null;
            }
        }



    public async Task<Usuario> GetUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                return usuario;
            }
            catch
            {

                throw;
            }
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosByName(string nome)
        {
            IEnumerable<Usuario> usuario;
            if (!string.IsNullOrWhiteSpace(nome))
            {
                usuario = await _context.Usuarios.Where(x => x.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                usuario = await GetUsuarios();
            }
            return usuario;
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
