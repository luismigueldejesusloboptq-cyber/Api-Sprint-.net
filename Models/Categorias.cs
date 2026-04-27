using System.Collections.Generic;
namespace Api_Lanchonete_Sprint.Models
{
    public class Categorias
    {
        public int Idcategoria {  get; set; }
        public string Nome { get; set; } = string.Empty;

        public virtual ICollection<Produto> Produtos { get; set; } = new List<Produtos>();

    }

}
