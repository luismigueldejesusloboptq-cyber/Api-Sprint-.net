

using Api_Lanchonete_Sprint.Data;
using Api_Lanchonete_Sprint.Models;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Lanchonete_Sprint.Repositories
{
    public class ItensPedidoRepository :
        IItensPedidoRepository
    {
        private readonly LanchoneteContext _context;

        public ItensPedidoRepository(
            LanchoneteContext context)
        {
            _context = context;
        }

        
        // LISTAR TODOS
        
        public async Task<List<ItensPedido>> GetAll()
        {
            return await _context.ItensPedidos
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .ToListAsync();
        }

        
        // BUSCAR POR ID
       
        public async Task<ItensPedido?> GetById(int id)
        {
            return await _context.ItensPedidos
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(
                    i => i.IdItem == id
                );
        }

       
        // CRIAR
        
        public async Task<ItensPedido> Create(
            ItensPedido item)
        {
            await _context.ItensPedidos
                .AddAsync(item);

            await _context.SaveChangesAsync();

            return item;
        }

        
        // ATUALIZAR
       
        public async Task<ItensPedido?> Update(
            ItensPedido item)
        {
            var itemExistente =
                await GetById(item.IdItem);

            if (itemExistente == null)
                return null;

            itemExistente.IdPedido =
                item.IdPedido;

            itemExistente.IdProduto =
                item.IdProduto;

            itemExistente.Quantidade =
                item.Quantidade;

            itemExistente.PrecoUnitario =
                item.PrecoUnitario;

            _context.ItensPedidos
                .Update(itemExistente);

            await _context.SaveChangesAsync();

            return itemExistente;
        }

        // DELETAR
       
        public async Task<bool> Delete(int id)
        {
            var item =
                await GetById(id);

            if (item == null)
                return false;

            _context.ItensPedidos
                .Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}