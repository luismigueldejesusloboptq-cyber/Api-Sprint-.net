using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_Lanchonete_Sprint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(
            IProdutoService service)
        {
            _service = service;
        }

        
        // LISTAR TODOS
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos =
                await _service.GetAll();

            return Ok(produtos);
        }

        
        // BUSCAR POR ID
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var produto =
                await _service.GetById(id);

            if (produto == null)
                return NotFound(
                    "Produto não encontrado."
                );

            return Ok(produto);
        }

        // CRIAR
        
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ProdutoRequestDTO dto)
        {
            var novoProduto =
                await _service.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoProduto.IdProduto },
                novoProduto
            );
        }

      
        // ATUALIZAR
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ProdutoRequestDTO dto)
        {
            var produtoAtualizado =
                await _service.Update(id, dto);

            if (produtoAtualizado == null)
                return NotFound(
                    "Produto não encontrado."
                );

            return Ok(produtoAtualizado);
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
                    "Produto não encontrado."
                );

            return NoContent();
        }
    }
}