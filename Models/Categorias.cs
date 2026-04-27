using System.Collections.Generic;
namespace Api_Lanchonete_Sprint.Models
{
    public class Categorias
    {
        public int IdCategoria {  get; set; }
        public string Nome { get; set; } = string.Empty;

        public virtual ICollection<Produtos> Produto { get; set; } = new List<Produtos>();

    }

}
