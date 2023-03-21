using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi_.Models;

namespace UsuariosApi_.Services
{
    public class TokenService
    {
        public Token CreateToken(CustomIdentityUser usuario, string role)
        {
            Claim[] direitosUsuarios = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, "REGULAR"),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString())
            };

            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasjdasadajsd09asjd09sajcnzxn"));

            var credenciais = new SigningCredentials(chave,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: direitosUsuarios, signingCredentials: credenciais, expires: DateTime.UtcNow.AddHours(1));

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
        }


    }
}
