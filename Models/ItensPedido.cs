namespace Api_Lanchonete_Sprint.Models
{
    public class ItensPedido
    {
        public int IdItem { get; set; }

        // Chaves Estrangeiras (fk)
        public int IdPedido { get; set; }

        public int IdProduto { get; set; }

        // Campos
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        // Relações
        public virtual Pedidos? Pedido { get; set; }
        public virtual Produtos? Produto { get; set; }
    }
}