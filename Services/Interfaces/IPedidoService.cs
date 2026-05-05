using Api_Lanchonete_Sprint.DTOs;

using Api_Lanchonete_Sprint.DTOs.Response;

namespace Api_Lanchonete_Sprint.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<List<PedidoResponseDTO>> GetAll();
        Task<PedidoResponseDTO?> GetById(int id);
        Task<PedidoResponseDTO> Create(PedidoRequestDTO dto);
        Task<PedidoResponseDTO?> Update(int id, PedidoRequestDTO dto);
        Task<bool> Delete(int id);
    }
}