

using Api_Lanchonete_Sprint.DTOs;
//using Api_Lanchonete_Sprint.DTOs.Request;
using Api_Lanchonete_Sprint.Models;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Api_Lanchonete_Sprint.Services.Interfaces;

namespace Api_Lanchonete_Sprint.Services
{
    public class ItensPedidoService :
        IItensPedidoService
    {
        private readonly IItensPedidoRepository
            _repository;

        public ItensPedidoService(
            IItensPedidoRepository repository)
        {
            _repository = repository;
        }

        // LISTAR TODOS
        
        public async Task<List<ItemPedidoResponseDTO>>
            GetAll()
        {
            var itens =
                await _repository.GetAll();

            return itens.Select(i =>
                new ItemPedidoResponseDTO
                {
                    IdItem = i.IdItem,
                    IdPedido = i.IdPedido,
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    PrecoUnitario =
                        i.PrecoUnitario,

                    Subtotal =
                        i.Quantidade *
                        i.PrecoUnitario
                }).ToList();
        }

        
        // BUSCAR POR ID
  
        public async Task<ItemPedidoResponseDTO?>
            GetById(int id)
        {
            var item =
                await _repository.GetById(id);

            if (item == null)
                return null;

            return new ItemPedidoResponseDTO
            {
                IdItem = item.IdItem,
                IdPedido = item.IdPedido,
                IdProduto = item.IdProduto,
                Quantidade = item.Quantidade,
                PrecoUnitario =
                    item.PrecoUnitario,

                Subtotal =
                    item.Quantidade *
                    item.PrecoUnitario
            };
        }

        
        // CRIAR
        
        public async Task<ItemPedidoResponseDTO>
            Create(ItemPedidoRequestDTO dto)
        {
            var item = new ItensPedido
            {
                IdPedido = dto.IdPedido,
                IdProduto = dto.IdProduto,
                Quantidade = dto.Quantidade,
                PrecoUnitario =
                    dto.PrecoUnitario
            };

            var novoItem =
                await _repository.Create(item);

            return new ItemPedidoResponseDTO
            {
                IdItem = novoItem.IdItem,
                IdPedido = novoItem.IdPedido,
                IdProduto = novoItem.IdProduto,
                Quantidade = novoItem.Quantidade,
                PrecoUnitario =
                    novoItem.PrecoUnitario,

                Subtotal =
                    novoItem.Quantidade *
                    novoItem.PrecoUnitario
            };
        }

        // ATUALIZAR
        
        public async Task<ItemPedidoResponseDTO?>
            Update(
                int id,
                ItemPedidoRequestDTO dto)
        {
            var item = new ItensPedido
            {
                IdItem = id,
                IdPedido = dto.IdPedido,
                IdProduto = dto.IdProduto,
                Quantidade = dto.Quantidade,
                PrecoUnitario =
                    dto.PrecoUnitario
            };

            var itemAtualizado =
                await _repository.Update(item);

            if (itemAtualizado == null)
                return null;

            return new ItemPedidoResponseDTO
            {
                IdItem =
                    itemAtualizado.IdItem,

                IdPedido =
                    itemAtualizado.IdPedido,

                IdProduto =
                    itemAtualizado.IdProduto,

                Quantidade =
                    itemAtualizado.Quantidade,

                PrecoUnitario =
                    itemAtualizado.PrecoUnitario,

                Subtotal =
                    itemAtualizado.Quantidade *
                    itemAtualizado.PrecoUnitario
            };
        }

        // DELETAR
     
        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}