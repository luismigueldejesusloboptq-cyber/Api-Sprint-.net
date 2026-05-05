// =========================================
// IFornecedorService.cs
// =========================================

using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.DTOs.Response;

namespace Api_Lanchonete_Sprint.Services.Interfaces
{
    public interface IFornecedorService
    {
        Task<List<FornecedorResponseDTO>> GetAll();

        Task<FornecedorResponseDTO?> GetById(int id);

        Task<FornecedorResponseDTO> Create(
            FornecedorRequestDTO dto
        );

        Task<FornecedorResponseDTO?> Update(
            int id,
            FornecedorRequestDTO dto
        );

        Task<bool> Delete(int id);
    }
}