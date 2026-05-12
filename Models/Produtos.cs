using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Adicione este
using System.ComponentModel.DataAnnotations.Schema; // Adicione este

namespace Api_Lanchonete_Sprint.Models
{
    [Table("produtos")] // Nome exato da tabela no MySQL
    public class Produtos
    {
        [Key]
        [Column("id_produto")] // Nome exato da coluna no MySQL
        public int IdProduto { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("preco")]
        public decimal Preco { get; set; }

        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [Column("id_fornecedor")]
        public int IdFornecedor { get; set; }

        // Relações (mantenha como está)
        public virtual Categorias? Categorias { get; set; }
        public virtual fornecedores? Fornecedores { get; set; }
        public virtual ICollection<ItensPedido> ItensPedido { get; set; } = new List<ItensPedido>();
    }
}