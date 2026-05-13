

using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Lanchonete_Sprint.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(
            ICategoriaService service)
        {
            _service = service;
        }

        // LISTAR TODAS

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias =
                await _service.ListarTodas();

            return Ok(categorias);
        }


        // BUSCAR POR ID

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var categoria =
                await _service.BuscarPorId(id);

            if (categoria == null)
            {
                return NotFound(
                    "Categoria não encontrada."
                );
            }

            return Ok(categoria);
        }

        // CRIAR
       
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CategoriaRequestDTO dto)
        {
            var novaCategoria =
                await _service.Criar(dto);

            return Ok(novaCategoria);
        }

       
        // ATUALIZAR
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CategoriaRequestDTO dto)
        {
            var atualizado =
                await _service.Atualizar(
                    id,
                    dto
                );

            if (!atualizado)
            {
                return NotFound(
                    "Categoria não encontrada."
                );
            }

            return Ok(
                "Categoria atualizada com sucesso."
            );
        }

       
        // DELETAR
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var deletado =
                await _service.Excluir(id);

            if (!deletado)
            {
                return NotFound(
                    "Categoria não encontrada."
                );
            }

            return Ok(
                "Categoria deletada com sucesso."
            );
        }
    }
}