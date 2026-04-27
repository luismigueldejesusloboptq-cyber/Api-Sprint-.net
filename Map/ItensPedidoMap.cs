using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Lanchonete_Sprint.Map
{
    public class ItensPedidoMap : IEntityTypeConfiguration<ItensPedido>
    {
        public void  Configure(EntityTypeBuilder<ItensPedido> builder)
        {
            builder.ToTable("itens_pedido");
            builder.HasKey(I => I.IdItem);
            builder.Property(i => i.IdItem).HasColumnName("id_item");

            builder.Property(i => i.IdPedido).HasColumnName("id_pedido").IsRequired();
            builder.Property(i => i.IdPedido).HasColumnName("id_produto").IsRequired();

            builder.Property(i => i.Quantidade).HasColumnName("quantidade").IsRequired();

            builder.Property(i => i.PrecoUnitario)
                .HasColumnName("preco_unitario")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(i => i.Pedido)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.IdPedido)
                .OnDelete(DeleteBehavior.Cascade); // Garante "ON DELETE CASCADE" do MySQL

            builder.HasOne(i => i.Produtos)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(i => i.IdPorduto);
        }
    }
}
