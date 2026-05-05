using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Lanchonete_Sprint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController :
        ControllerBase
    {
        private readonly IAuthService
            _service;

        public AuthController(
            IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult>
            Login(
                [FromBody]
                LoginRequestDTO dto
            )
        {
            var resultado =
                await _service.Login(dto);

            if (resultado == null)
            {
                return Unauthorized(
                    "Email ou senha inválidos."
                );
            }

            return Ok(resultado);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(
    [FromBody]
    CadastroUsuarioDTO dto)
        {
            var resultado =
                await _service.Registrar(dto);

            if (!resultado)
            {
                return BadRequest(
                    "Email já cadastrado."
                );
            }

            return Ok(
                "Usuário criado com sucesso."
            );
        }
    }
}