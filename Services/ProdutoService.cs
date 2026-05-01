using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Models;
using Api_Lanchonete_Sprint.Repositories;

namespace Api_Lanchonete_Sprint.Services
{
    public class ProdutoService :
        IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(
            IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProdutoResponseDTO>>
            GetAll()
        {
            var produtos =
                await _repository.GetAll();

            return produtos.Select(p =>
                new ProdutoResponseDTO
                {
                    IdProduto = p.IdProduto,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    IdCategoria = p.IdCategoria,
                    IdFornecedor = p.IdFornecedor
                }).ToList();
        }

        public async Task<ProdutoResponseDTO?>
            GetById(int id)
        {
            var produto =
                await _repository.GetById(id);

            if (produto == null)
                return null;

            return new ProdutoResponseDTO
            {
                IdProduto = produto.IdProduto,
                Nome = produto.Nome,
                Preco = produto.Preco,
                IdCategoria = produto.IdCategoria,
                IdFornecedor = produto.IdFornecedor
            };
        }

        public async Task<ProdutoResponseDTO>
            Create(ProdutoRequestDTO dto)
        {
            var produto = new Produtos
            {
                Nome = dto.Nome,
                Preco = dto.Preco,
                IdCategoria = dto.IdCategoria,
                IdFornecedor = dto.IdFornecedor
            };

            var novoProduto =
                await _repository.Create(produto);

            return new ProdutoResponseDTO
            {
                IdProduto = novoProduto.IdProduto,
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco,
                IdCategoria = novoProduto.IdCategoria,
                IdFornecedor = novoProduto.IdFornecedor
            };
        }

        public async Task<ProdutoResponseDTO?>
            Update(
                int id,
                ProdutoRequestDTO dto)
        {
            var produto = new Produtos
            {
                IdProduto = id,
                Nome = dto.Nome,
                Preco = dto.Preco,
                IdCategoria = dto.IdCategoria,
                IdFornecedor = dto.IdFornecedor
            };

            var produtoAtualizado =
                await _repository.Update(produto);

            if (produtoAtualizado == null)
                return null;

            return new ProdutoResponseDTO
            {
                IdProduto =
                    produtoAtualizado.IdProduto,
                Nome = produtoAtualizado.Nome,
                Preco = produtoAtualizado.Preco,
                IdCategoria =
                    produtoAtualizado.IdCategoria,
                IdFornecedor =
                    produtoAtualizado.IdFornecedor
            };
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}