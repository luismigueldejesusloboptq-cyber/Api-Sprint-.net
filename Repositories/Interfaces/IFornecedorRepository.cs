
using Api_Lanchonete_Sprint.Models;

namespace Api_Lanchonete_Sprint.Repositories.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<List<fornecedores>> GetAll();

        Task<fornecedores?> GetById(int id);

        Task<fornecedores> Create(
            fornecedores fornecedor
        );

        Task<fornecedores?> Update(
            fornecedores fornecedor
        );

        Task<bool> Delete(int id);
    }
}