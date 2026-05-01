using Api_Lanchonete_Sprint.DTOs;

namespace Api_Lanchonete_Sprint.Services
{
    public interface IProdutoService
    {
        Task<List<ProdutoResponseDTO>> GetAll();

        Task<ProdutoResponseDTO?> GetById(int id);

        Task<ProdutoResponseDTO> Create(
            ProdutoRequestDTO dto);

        Task<ProdutoResponseDTO?> Update(
            int id,
            ProdutoRequestDTO dto);

        Task<bool> Delete(int id);
    }
}