using Api_Lanchonete_Sprint.DTOs;
namespace Api_Lanchonete_Sprint.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaResponseDTO>> ListarTodas();
        Task<CategoriaResponseDTO?> BuscarPorId(int id);
        Task<CategoriaResponseDTO> Criar(CategoriaRequestDTO dto);
        Task<bool> Atualizar(int id, CategoriaResponseDTO dto);
        Task<bool> Excluir(int id);
    }
}
