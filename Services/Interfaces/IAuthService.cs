using Api_Lanchonete_Sprint.DTOs;

namespace Api_Lanchonete_Sprint.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO?>
            Login(LoginRequestDTO dto);
    }
}