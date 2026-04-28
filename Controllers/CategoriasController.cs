using Api_Lanchonete_Sprint.DTOs;
using Api_Lanchonete_Sprint.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_Lanchonete_Sprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaResponseDTO>>> Get()
            => Ok(await _service.ListarTodas());

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponseDTO>> GetById(int id)
        {
            var res = await _service.BuscarPorId(id);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoriaRequestDTO dto)
        {
            var criado = await _service.Criar(dto);
            return CreatedAtAction(nameof(GetById), new { id = criado.IdCategoria }, criado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoriaRequestDTO dto)
        {
            var sucesso = await _service.Atualizar(id, dto);
            return sucesso ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var sucesso = await _service.Excluir(id);
            return sucesso ? NoContent() : NotFound();
        }
    }
}