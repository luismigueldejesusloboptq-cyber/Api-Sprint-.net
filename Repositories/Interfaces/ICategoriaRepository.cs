using Api_Lanchonete_Sprint.Models;

namespace Api_Lanchonete_Sprint.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categorias>> GetAllAsync();
        Task<Categorias?> GetByIdAsync(int id);
        Task AddAsync(Categorias categoria);
        Task UpdateAsync(Categorias categoria);
        Task DeleteAsync(Categorias categoria);
    }
}