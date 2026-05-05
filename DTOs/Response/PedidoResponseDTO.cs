
namespace Api_Lanchonete_Sprint.DTOs
{
    public class PedidoResponseDTO
    {
        public int IdPedido { get; set; }

        public DateTime DataPedido { get; set; }

        public string? ClienteNome { get; set; }

        public int NumeroMesa { get; set; }
    }
}