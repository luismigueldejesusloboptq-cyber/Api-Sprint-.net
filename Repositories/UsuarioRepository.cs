using Api_Lanchonete_Sprint.Data;
using Api_Lanchonete_Sprint.Models;
using Api_Lanchonete_Sprint.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Lanchonete_Sprint.Repositories
{
    public class UsuarioRepository :
        IUsuarioRepository
    {
        private readonly LanchoneteContext
            _context;

        public UsuarioRepository(
            LanchoneteContext context)
        {
            _context = context;
        }

        public async Task<Usuario?>
            BuscarPorEmail(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(
                    u => u.Email == email
                );
        }

        public async Task<Usuario>
            Criar(Usuario usuario)
        {
            await _context.Usuarios
                .AddAsync(usuario);

            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}