using Api_Lanchonete_Sprint.DTOs.Request;
using Api_Lanchonete_Sprint.DTOs.Response;
using Api_Lanchonete_Sprint.Models;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Api_Lanchonete_Sprint.Services.Interfaces;

namespace Api_Lanchonete_Sprint.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoriaResponseDTO>> ListarTodas()
        {
            var lista = await _repository.GetAllAsync();
            return lista.Select(c => new CategoriaResponseDTO { IdCategoria = c.IdCategoria, Nome = c.Nome });
        }

        public async Task<CategoriaResponseDTO?> BuscarPorId(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            return c == null ? null : new CategoriaResponseDTO { IdCategoria = c.IdCategoria, Nome = c.Nome };
        }

        public async Task<CategoriaResponseDTO> Criar(CategoriaRequestDTO dto)
        {
            var nova = new Categorias { Nome = dto.Nome };
            await _repository.AddAsync(nova);
            return new CategoriaResponseDTO { IdCategoria = nova.IdCategoria, Nome = nova.Nome };
        }

        public async Task<bool> Atualizar(int id, CategoriaRequestDTO dto)
        {
            var existente = await _repository.GetByIdAsync(id);
            if (existente == null) return false;

            existente.Nome = dto.Nome;
            await _repository.UpdateAsync(existente);
            return true;
        }

        public async Task<bool> Excluir(int id)
        {
            var existente = await _repository.GetByIdAsync(id);
            if (existente == null) return false;

            await _repository.DeleteAsync(existente);
            return true;
        }
    }
}