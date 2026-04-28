using Api_Lanchonete_Sprint.Data;
using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Lanchonete_Sprint.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly LanchoneteContext _context;

        public CategoriaRepository(LanchoneteContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<categorias>> GetAllAsync() => await _context.Categorias.ToListAsync();

        public async Task<categorias?> GetByIdAsync(int id) => await _context.Categorias.FindAsync(id);

        public async Task AddAsync(categorias categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(categorias categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(categorias categoria)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}