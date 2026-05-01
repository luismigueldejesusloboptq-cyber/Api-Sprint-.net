using Api_Lanchonete_Sprint.Models;

namespace Api_Lanchonete_Sprint.Repositories
{
    public interface IProdutoRepository
    {
        Task<List<Produtos>> GetAll();

        Task<Produtos?> GetById(int id);

        Task<Produtos> Create(Produtos produto);

        Task<Produtos?> Update(Produtos produto);

        Task<bool> Delete(int id);
    }
}