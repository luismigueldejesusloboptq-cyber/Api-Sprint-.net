namespace Api_Lanchonete_Sprint.DTOs
{
    public class ProdutoResponseDTO
    {
        public int IdProduto { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public int IdCategoria { get; set; }

        public int IdFornecedor { get; set; }
    }
}