using Api_Lanchonete_Sprint.Models;

namespace Api_Lanchonete_Sprint.Repositories
{
    public interface IPedidoRepository
    {
        Task<List<Pedidos>> GetAll();

        Task<Pedidos?> GetById(int id);

        Task<Pedidos> Create(Pedidos pedido);

        Task<Pedidos?> Update(Pedidos pedido);

        Task<bool> Delete(int id);
    }
}