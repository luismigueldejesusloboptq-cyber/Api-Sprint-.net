
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Lanchonete_Sprint.Data;
using Api_Lanchonete_Sprint.Models;

namespace Api_Lanchonete_Sprint.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ClientesController : ControllerBase
    {
        private readonly LanchoneteContext _context;
        public ClientesController(LanchoneteContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();

            return Ok(clientes);
        }


        //Busca cliente pelo id//
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientes(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            return Ok(cliente);
        }



        //Atualiza os dados de um cliente existete//
        [HttpPut("{id}")]

        public async Task<IActionResult> PutClientes([FromRoute]int id, [FromBody] Cliente clientRequest)
        {


            var cliente = _context.Clientes.FirstOrDefault(c => c.IdClientes == id);

            if (cliente == null)
            {
                return BadRequest("O ID da URL não confere com o ID do cliente.");
            }

            cliente.Nome = clientRequest.Nome;
            cliente.Email = clientRequest.Email;

            _context.Clientes.Update(cliente);

            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        //Apaga um cliente do banco de dados//
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteClientes(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if(cliente == null)
                 return NotFound("Cliente não encontrado para exclusão");
            

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();

            return NoContent(); //204 - sucesso//

        }

    }
}
