
using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Lanchonete_Sprint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItensPedidoController : ControllerBase
    {
        private readonly IItensPedidoService _service;

        public ItensPedidoController(
            IItensPedidoService service)
        {
            _service = service;
        }

        
        // LISTAR TODOS
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var itens = await _service.GetAll();

            return Ok(itens);
        }

        
        // BUSCAR POR ID
      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetById(id);

            if (item == null)
            {
                return NotFound(
                    "Item do pedido não encontrado."
                );
            }

            return Ok(item);
        }

       
        // CRIAR
        
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ItemPedidoRequestDTO dto)
        {
            var novoItem =
                await _service.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoItem.IdItem },
                novoItem
            );
        }

        
        // ATUALIZAR
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ItemPedidoRequestDTO dto)
        {
            var itemAtualizado =
                await _service.Update(id, dto);

            if (itemAtualizado == null)
            {
                return NotFound(
                    "Item do pedido não encontrado."
                );
            }

            return Ok(itemAtualizado);
        }

        
        // DELETAR
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletado =
                await _service.Delete(id);

            if (!deletado)
            {
                return NotFound(
                    "Item do pedido não encontrado."
                );
            }

            return NoContent();
        }
    }
}