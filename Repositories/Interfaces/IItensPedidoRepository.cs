

using Api_Lanchonete_Sprint.Models;

namespace Api_Lanchonete_Sprint.Repositories.Interfaces
{
    public interface IItensPedidoRepository
    {
        Task<List<ItensPedido>> GetAll();

        Task<ItensPedido?> GetById(int id);

        Task<ItensPedido> Create(
            ItensPedido item
        );

        Task<ItensPedido?> Update(
            ItensPedido item
        );

        Task<bool> Delete(int id);
    }
}