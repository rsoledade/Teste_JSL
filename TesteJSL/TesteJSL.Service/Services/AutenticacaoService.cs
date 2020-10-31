using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TesteJSL.Domain.Entities;
using TesteJSL.Domain.Interfaces;
using TesteJSL.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace TesteJSL.Services
{
    public class AutenticacaoService :  ControllerBase, IAutenticacaoService
    {        
        protected readonly IUsuarioService _usuarioService;

        public AutenticacaoService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<string> GerarToken(Usuario usuario, IConfiguration configuration)
        {
            string tokenString = default(string);
            var usuarioValidado = await _usuarioService.BuscaUsuario(usuario);

            if (usuarioValidado != null)
            {
                var appSettingsSection = configuration.GetSection("AppSettings");
                var appSettings = appSettingsSection.Get<AppSettings>();
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);

                if (usuario == null)
                {
                    return null;
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("id", usuario.Id.ToString())
                    }),

                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                tokenString = tokenHandler.WriteToken(token);
            }

            return tokenString;
        }

    }
}
