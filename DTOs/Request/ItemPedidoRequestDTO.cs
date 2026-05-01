using System;

namespace Api_Lanchonete_Sprint.DTOs
{
  
    public class ItemPedidoRequestDTO
    {
        public int IdPedido { get; set; }

        public int IdProduto { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }
    }
}