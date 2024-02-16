using Blog.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Blog.Api.Models;
using Blog.Api.Properties;
using Blog.Api.Repositories.Interfaces;

namespace Blog.Api.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public JwtTokenService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public string GenerateToken(Usuario user)
        {
            var usuario = _usuarioRepository.Get(user.UserName, user.Password);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
