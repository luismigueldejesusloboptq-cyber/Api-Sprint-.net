using System.Collections.Generic;
namespace Api_Lanchonete_Sprint.Models
{
    public class fornecedores 
    {
        public int IdFornecedor { get; set; }
        public string Nome { get; set; }
        public string? Contato { get; set; }

        public virtual ICollection<Produtos> Produto { get; set; } = new List<Produtos> ();

    }
}
