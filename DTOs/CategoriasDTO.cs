

namespace Api_Lanchonete_Sprint.DTOs
{
    public class CategoriaResponseDTO
    {
        public int Idcategoria { get; set; }
        public string Nome { get; set; } = string.Empty;

    }

    public class CategoriaRequestDTO
    {
        public string Nome { get; set; } = string.Empty;
    }
}
