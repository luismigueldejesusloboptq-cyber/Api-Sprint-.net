using Api_Lanchonete_Sprint.Data;
using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Lanchonete_Sprint.Repositories
{
    public class PedidoRepository :
        IPedidoRepository
    {
        private readonly LanchoneteContext _context;

        public PedidoRepository(
            LanchoneteContext context)
        {
            _context = context;
        }

        public async Task<List<Pedidos>> GetAll()
        {
            return await _context.pedidos
                .ToListAsync();
        }

        public async Task<Pedidos?> GetById(int id)
        {
            return await _context.pedidos
                .FirstOrDefaultAsync(
                    p => p.IdPedido == id
                );
        }

        public async Task<Pedidos> Create(
            Pedidos pedido)
        {
            await _context.pedidos
                .AddAsync(pedido);

            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<Pedidos?> Update(
            Pedidos pedido)
        {
            var pedidoExistente =
                await GetById(pedido.IdPedido);

            if (pedidoExistente == null)
                return null;

            pedidoExistente.ClienteNome =
                pedido.ClienteNome;

            pedidoExistente.NumeroMesa =
                pedido.NumeroMesa;

            _context.pedidos.Update(
                pedidoExistente);

            await _context.SaveChangesAsync();

            return pedidoExistente;
        }

        public async Task<bool> Delete(int id)
        {
            var pedido =
                await GetById(id);

            if (pedido == null)
                return false;

            _context.pedidos.Remove(pedido);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}