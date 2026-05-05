



using Api_Lanchonete_Sprint.Data;
using Api_Lanchonete_Sprint.Models;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Lanchonete_Sprint.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly LanchoneteContext _context;

        public FornecedorRepository(
            LanchoneteContext context)
        {
            _context = context;
        }

    
        // LISTAR TODOS
        
        public async Task<List<fornecedores>> GetAll()
        {
            return await _context.fornecedores
                .ToListAsync();
        }

        // BUSCAR POR ID

        public async Task<fornecedores?> GetById(int id)
        {
            return await _context.fornecedores
                .FirstOrDefaultAsync(
                    f => f.IdFornecedor == id
                );
        }

        // CRIAR
        
        public async Task<fornecedores> Create(
            fornecedores fornecedor)
        {
            await _context.fornecedores
                .AddAsync(fornecedor);

            await _context.SaveChangesAsync();

            return fornecedor;
        }

       
        // ATUALIZAR
       
        public async Task<fornecedores?> Update(
            fornecedores fornecedor)
        {
            var fornecedorExistente =
                await GetById(fornecedor.IdFornecedor);

            if (fornecedorExistente == null)
                return null;

            fornecedorExistente.Nome =
                fornecedor.Nome;

            fornecedorExistente.Contato =
                fornecedor.Contato;

            _context.fornecedores
                .Update(fornecedorExistente);

            await _context.SaveChangesAsync();

            return fornecedorExistente;
        }

        
        // DELETAR
        
        public async Task<bool> Delete(int id)
        {
            var fornecedor =
                await GetById(id);

            if (fornecedor == null)
                return false;

            _context.fornecedores
                .Remove(fornecedor);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}