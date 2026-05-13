using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Lanchonete_Sprint.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedoresController
        : ControllerBase
    {
        private readonly IFornecedorService
            _service;

        public FornecedoresController(
            IFornecedorService service)
        {
            _service = service;
        }

        // LISTAR TODOS
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fornecedores =
                await _service.GetAll();

            return Ok(fornecedores);
        }

        // BUSCAR POR ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var fornecedor =
                await _service.GetById(id);

            if (fornecedor == null)
            {
                return NotFound(
                    "Fornecedor não encontrado."
                );
            }

            return Ok(fornecedor);
        }

        // CRIAR
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] FornecedorRequestDTO dto)
        {
            var novoFornecedor =
                await _service.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    id =
                        novoFornecedor.IdFornecedor
                },
                novoFornecedor
            );
        }

        // ATUALIZAR
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] FornecedorRequestDTO dto)
        {
            var fornecedorAtualizado =
                await _service.Update(id, dto);

            if (fornecedorAtualizado == null)
            {
                return NotFound(
                    "Fornecedor não encontrado."
                );
            }

            return Ok(fornecedorAtualizado);
        }

        // DELETAR
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var deletado =
                await _service.Delete(id);

            if (!deletado)
            {
                return NotFound(
                    "Fornecedor não encontrado."
                );
            }

            return NoContent();
        }
    }
}