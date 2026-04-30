namespace Api_Lanchonete_Sprint.DTOs
{
    public class ItemPedidoResponseDTO
    {
        public int IdItem { get; set; }

        public int IdPedido { get; set; }

        public int IdProduto { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}