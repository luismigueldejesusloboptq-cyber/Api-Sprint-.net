
using System.Collections.Generic;
using Api_Lanchonete_Sprint.DTOs;

namespace Api_Lanchonete_Sprint.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco {  get; set; }

        //fk//
        public int IdCategoria { get; set; }
        public int IdFornecedor { get; set; }

        //relações/
        public virtual Categorias? Categorias { get; set; }
        public virtual fornecedores? Fornecedores { get; set; }
        public virtual ICollection<ItensPedido> ItensPedido { get; set; } = new List<ItensPedido>();

    }
}
