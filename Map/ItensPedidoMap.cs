using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Lanchonete_Sprint.Map
{
    public class ItensPedidoMap : IEntityTypeConfiguration<ItensPedido>
    {
        public void Configure(EntityTypeBuilder<ItensPedido> builder)
        {
            builder.ToTable("itens_pedido");

            builder.HasKey(i => i.IdItem);
            builder.Property(i => i.IdItem).HasColumnName("id_item");

            builder.Property(i => i.IdPedido).HasColumnName("id_pedido").IsRequired();
            builder.Property(i => i.IdProduto).HasColumnName("id_produto").IsRequired();

            builder.Property(i => i.Quantidade).HasColumnName("quantidade").IsRequired();

            builder.Property(i => i.PrecoUnitario)
                .HasColumnName("preco_unitario")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            // Relação: Um Pedido tem muitos itens (Cascade apaga os itens se o pedido for deletado)
            builder.HasOne(i => i.Pedido)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.IdPedido)
                .OnDelete(DeleteBehavior.Cascade);

            // Relação: O item pertence a um Produto
            builder.HasOne(i => i.Produto)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.IdProduto);
        }
    }
}