using Api_Lanchonete_Sprint.Data;
using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Lanchonete_Sprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly LanchoneteContext _context;

        public CategoriasController(LanchoneteContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaResponseDTO>>> GetCategorias()
        {
            var categorias = await _context.Categorias
                .Select(c => new CategoriaResponseDTO
                {
                    Idcategoria = c.IdCategoria,
                    Nome = c.Nome
                })
                .ToListAsync();

            return Ok(categorias);
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponseDTO>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            var dto = new CategoriaResponseDTO
            {
                Idcategoria = categoria.IdCategoria,
                Nome = categoria.Nome
            };

            return Ok(dto);
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<CategoriaResponseDTO>> PostCategoria(CategoriaRequestDTO dto)
        {
            // Transforma a DTO recebida em uma Model para salvar no banco
            var novaCategoria = new Categorias
            {
                Nome = dto.Nome
            };

            _context.Categorias.Add(novaCategoria);
            await _context.SaveChangesAsync();

            // Monta a DTO de resposta com o ID gerado pelo banco
            var responseDto = new CategoriaResponseDTO
            {
                Idcategoria = novaCategoria.IdCategoria,
                Nome = novaCategoria.Nome
            };

            return CreatedAtAction(nameof(GetCategoria), new { id = novaCategoria.IdCategoria }, responseDto);
        }

        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, CategoriaRequestDTO dto)
        {
            var categoriaExistente = await _context.Categorias.FindAsync(id);

            if (categoriaExistente == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            // Atualiza os dados
            categoriaExistente.Nome = dto.Nome;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}