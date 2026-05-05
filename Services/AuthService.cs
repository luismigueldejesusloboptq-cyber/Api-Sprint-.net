using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Models;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Api_Lanchonete_Sprint.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Api_Lanchonete_Sprint.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUsuarioRepository repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO?> Login(LoginRequestDTO dto)
        {
            var usuario = await _repository.BuscarPorEmail(dto.Email);

            if (usuario == null)
                return null;

            var passwordHasher = new PasswordHasher<Usuario>();

            var resultadoSenha = passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.Senha,
                dto.Senha
            );

            if (resultadoSenha == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var token = GerarToken(usuario);

            return new LoginResponseDTO
            {
                Token = token
            };
        }

        public async Task<bool> Registrar(CadastroUsuarioDTO dto)
        {
            var usuarioExistente = await _repository.BuscarPorEmail(dto.Email);

            if (usuarioExistente != null)
                return false;

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email
            };

            var passwordHasher = new PasswordHasher<Usuario>();

            usuario.Senha = passwordHasher.HashPassword(usuario, dto.Senha);

            await _repository.Criar(usuario);

            return true;
        }

        private string GerarToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "ChaveSecretaSuperLongaParaEvitarErrosDeCompilacao123!");

            var claimsDicionario = new Dictionary<string, object>
            {
                { JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString() },
                { JwtRegisteredClaimNames.Name, usuario.Nome },
                { JwtRegisteredClaimNames.Email, usuario.Email }
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: null,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            foreach (var claim in claimsDicionario)
            {
                token.Payload[claim.Key] = claim.Value;
            }

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}