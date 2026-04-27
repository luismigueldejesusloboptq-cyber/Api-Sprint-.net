namespace Api_Lanchonete_Sprint.Models
{
    public class ItensPedido
    {
        public int IdItem { get; set; }

        //fk//
        public int IdPedido { get; set; }
        public int IdPorduto { get; set; }


        // campos//
        public int Quantidade { get; set; }
        public decimal PrecoUnitario    { get; set; }

        //relações//

        public virtual Pedidos? Pedido { get; set; }
        public virtual Produto? Produtos { get; set; }    
    }
}
