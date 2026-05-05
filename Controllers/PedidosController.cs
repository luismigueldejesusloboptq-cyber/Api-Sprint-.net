using Api_Lanchonete_Sprint.DTOs;
//using Api_Lanchonete_Sprint.DTOs.Request;
using Api_Lanchonete_Sprint.DTOs.Response;
using Api_Lanchonete_Sprint.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Lanchonete_Sprint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidosController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _service.GetAll();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _service.GetById(id);
            if (pedido == null) return NotFound("Pedido não encontrado.");
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PedidoRequestDTO dto)
        {
            var novoPedido = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = novoPedido.IdPedido }, novoPedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PedidoRequestDTO dto)
        {
            var pedidoAtualizado = await _service.Update(id, dto);
            if (pedidoAtualizado == null) return NotFound("Pedido não encontrado.");
            return Ok(pedidoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletado = await _service.Delete(id);
            if (!deletado) return NotFound("Pedido não encontrado.");
            return NoContent();
        }
    }
}