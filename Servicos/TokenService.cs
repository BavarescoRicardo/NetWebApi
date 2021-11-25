using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NetWebApi.Modelos;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace NetWebApi.Servicos
{
    public class TokenService
    {
        private enum Nivel { Administrador = 3, Gestor = 2, Autorizado = 1, Visitante = 0 };
        public static string GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ApelidoLogin.ToString()),
                    // new Claim(ClaimTypes.Role, user.Permissao.ToString())
                    new Claim(ClaimTypes.Role, ((Nivel)user.Permissao).ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
