using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_Lanchonete_Sprint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidosController(
            IPedidoService service)
        {
            _service = service;
        }

        
        // LISTAR TODOS
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos =
                await _service.GetAll();

            return Ok(pedidos);
        }

        
        // BUSCAR POR ID
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var pedido =
                await _service.GetById(id);

            if (pedido == null)
                return NotFound(
                    "Pedido não encontrado."
                );

            return Ok(pedido);
        }

        // CRIAR
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] PedidoRequestDTO dto)
        {
            var novoPedido =
                await _service.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoPedido.IdPedido },
                novoPedido
            );
        }

        
        // ATUALIZAR
       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] PedidoRequestDTO dto)
        {
            var pedidoAtualizado =
                await _service.Update(id, dto);

            if (pedidoAtualizado == null)
                return NotFound(
                    "Pedido não encontrado."
                );

            return Ok(pedidoAtualizado);
        }

       
        // DELETAR
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var deletado =
                await _service.Delete(id);

            if (!deletado)
                return NotFound(
                    "Pedido não encontrado."
                );

            return NoContent();
        }
    }
}