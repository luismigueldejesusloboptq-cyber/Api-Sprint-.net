
using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.DTOs.Response;

namespace Api_Lanchonete_Sprint.Services.Interfaces
{
    public interface IItensPedidoService
    {
        Task<List<ItemPedidoResponseDTO>> GetAll();

        Task<ItemPedidoResponseDTO?> GetById(int id);

        Task<ItemPedidoResponseDTO> Create(
            ItemPedidoRequestDTO dto
        );

        Task<ItemPedidoResponseDTO?> Update(
            int id,
            ItemPedidoRequestDTO dto
        );

        Task<bool> Delete(int id);
    }
}