using Api_Lanchonete_Sprint.DTOs;
//using Api_Lanchonete_Sprint.DTOs.Request;
using Api_Lanchonete_Sprint.DTOs.Response;
using Api_Lanchonete_Sprint.Models;
using Api_Lanchonete_Sprint.Repositories;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Api_Lanchonete_Sprint.Services.Interfaces;

namespace Api_Lanchonete_Sprint.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PedidoResponseDTO>> GetAll()
        {
            var pedidos = await _repository.GetAll();
            return pedidos.Select(p => new PedidoResponseDTO
            {
                IdPedido = p.IdPedido,
                DataPedido = p.DataPedido,
                ClienteNome = p.ClienteNome,
                NumeroMesa = p.NumeroMesa
            }).ToList();
        }

        public async Task<PedidoResponseDTO?> GetById(int id)
        {
            var pedido = await _repository.GetById(id);
            if (pedido == null) return null;

            return new PedidoResponseDTO
            {
                IdPedido = pedido.IdPedido,
                DataPedido = pedido.DataPedido,
                ClienteNome = pedido.ClienteNome,
                NumeroMesa = pedido.NumeroMesa
            };
        }

        public async Task<PedidoResponseDTO> Create(PedidoRequestDTO dto)
        {
            var pedido = new Pedidos
            {
                ClienteNome = dto.ClienteNome,
                NumeroMesa = dto.NumeroMesa,
                DataPedido = DateTime.Now
            };

            var novoPedido = await _repository.Create(pedido);

            return new PedidoResponseDTO
            {
                IdPedido = novoPedido.IdPedido,
                DataPedido = novoPedido.DataPedido,
                ClienteNome = novoPedido.ClienteNome,
                NumeroMesa = novoPedido.NumeroMesa
            };
        }

        public async Task<PedidoResponseDTO?> Update(int id, PedidoRequestDTO dto)
        {
            var pedidoExistente = await _repository.GetById(id);
            if (pedidoExistente == null) return null;

            pedidoExistente.ClienteNome = dto.ClienteNome;
            pedidoExistente.NumeroMesa = dto.NumeroMesa;

            var atualizado = await _repository.Update(pedidoExistente);

            return new PedidoResponseDTO
            {
                IdPedido = atualizado.IdPedido,
                DataPedido = atualizado.DataPedido,
                ClienteNome = atualizado.ClienteNome,
                NumeroMesa = atualizado.NumeroMesa
            };
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}