

using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.DTOs.Response;
using Api_Lanchonete_Sprint.Models;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Api_Lanchonete_Sprint.Services.Interfaces;

namespace Api_Lanchonete_Sprint.Services
{
    public class FornecedorService :
        IFornecedorService
    {
        private readonly IFornecedorRepository _repository;

        public FornecedorService(
            IFornecedorRepository repository)
        {
            _repository = repository;
        }

        
        // LISTAR TODOS
       
        public async Task<List<FornecedorResponseDTO>>
            GetAll()
        {
            var fornecedores =
                await _repository.GetAll();

            return fornecedores.Select(f =>
                new FornecedorResponseDTO
                {
                    IdFornecedor = f.IdFornecedor,
                    Nome = f.Nome,
                    Contato = f.Contato
                }).ToList();
        }

       
        // BUSCAR POR ID
       
        public async Task<FornecedorResponseDTO?>
            GetById(int id)
        {
            var fornecedor =
                await _repository.GetById(id);

            if (fornecedor == null)
                return null;

            return new FornecedorResponseDTO
            {
                IdFornecedor =
                    fornecedor.IdFornecedor,

                Nome = fornecedor.Nome,

                Contato = fornecedor.Contato
            };
        }

        // CRIAR
       
        public async Task<FornecedorResponseDTO>
            Create(FornecedorRequestDTO dto)
        {
            var fornecedor = new fornecedores
            {
                Nome = dto.Nome,
                Contato = dto.Contato
            };

            var novoFornecedor =
                await _repository.Create(fornecedor);

            return new FornecedorResponseDTO
            {
                IdFornecedor =
                    novoFornecedor.IdFornecedor,

                Nome = novoFornecedor.Nome,

                Contato = novoFornecedor.Contato
            };
        }

     
        // ATUALIZAR
        
        public async Task<FornecedorResponseDTO?>
            Update(
                int id,
                FornecedorRequestDTO dto)
        {
            var fornecedor = new fornecedores
            {
                IdFornecedor = id,
                Nome = dto.Nome,
                Contato = dto.Contato
            };

            var fornecedorAtualizado =
                await _repository.Update(fornecedor);

            if (fornecedorAtualizado == null)
                return null;

            return new FornecedorResponseDTO
            {
                IdFornecedor =
                    fornecedorAtualizado.IdFornecedor,

                Nome =
                    fornecedorAtualizado.Nome,

                Contato =
                    fornecedorAtualizado.Contato
            };
        }

     
        // DELETAR
     
        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}