using Api_Lanchonete_Sprint.Data;
using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Lanchonete_Sprint.Repositories
{
    public class ProdutoRepository :
        IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produtos>> GetAll()
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .ToListAsync();
        }

        public async Task<Produtos?> GetById(int id)
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(
                    p => p.IdProduto == id
                );
        }

        public async Task<Produtos> Create(
            Produtos produto)
        {
            await _context.Produtos
                .AddAsync(produto);

            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produtos?> Update(
            Produtos produto)
        {
            var produtoExistente =
                await GetById(produto.IdProduto);

            if (produtoExistente == null)
                return null;

            produtoExistente.Nome =
                produto.Nome;

            produtoExistente.Preco =
                produto.Preco;

            produtoExistente.IdCategoria =
                produto.IdCategoria;

            produtoExistente.IdFornecedor =
                produto.IdFornecedor;

            _context.Produtos.Update(
                produtoExistente);

            await _context.SaveChangesAsync();

            return produtoExistente;
        }

        public async Task<bool> Delete(int id)
        {
            var produto =
                await GetById(id);

            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}