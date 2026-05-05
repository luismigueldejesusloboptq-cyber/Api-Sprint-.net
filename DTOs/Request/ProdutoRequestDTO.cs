
using Api_Lanchonete_Sprint.DTOs;
namespace Api_Lanchonete_Sprint.DTOs
{
    public class ProdutoRequestDTO
    {
        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public int IdCategoria { get; set; }

        public int IdFornecedor { get; set; }
    }
}