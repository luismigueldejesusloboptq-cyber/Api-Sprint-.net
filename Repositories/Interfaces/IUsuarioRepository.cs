using Api_Lanchonete_Sprint.Models;

namespace Api_Lanchonete_Sprint.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> BuscarPorEmail(
            string email
        );

        Task<Usuario> Criar(
            Usuario usuario
        );
    }
}