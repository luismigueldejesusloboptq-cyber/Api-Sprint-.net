using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Api_Lanchonete_Sprint.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_Lanchonete_Sprint.Services
{
    public class AuthService :
        IAuthService
    {
        private readonly IUsuarioRepository
            _repository;

        private readonly IConfiguration
            _configuration;

        public AuthService(
            IUsuarioRepository repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO?>
            Login(LoginRequestDTO dto)
        {
            var usuario =
                await _repository
                    .BuscarPorEmail(dto.Email);

            if (usuario == null)
                return null;

            if (usuario.Senha != dto.Senha)
                return null;

            var token =
                GerarToken(usuario);

            return new LoginResponseDTO
            {
                Token = token
            };
        }

        private string GerarToken(
            Models.Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]
            );

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    usuario.Email
                )
            };

            var credentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                issuer:
                    _configuration["Jwt:Issuer"],

                audience:
                    _configuration["Jwt:Audience"],

                claims: claims,

                expires:
                    DateTime.Now.AddHours(2),

                signingCredentials:
                    credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}