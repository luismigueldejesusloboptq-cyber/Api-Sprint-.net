using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api_Lanchonete_Sprint.Map
{
    public class PedidosMap : IEntityTypeConfiguration<Pedidos>
    {
        public void Configure(EntityTypeBuilder<Pedidos> builder)
        {
            builder.ToTable("pedidos");
            builder.HasKey(p => p.IdPedido);
            builder.Property(p => p.IdPedido).HasColumnName("id_pedido");

            builder.Property(p => p.DataPedido)
                .HasColumnName("data_pedido")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(p => p.ClienteNome)
                .HasColumnName("cliente_nome")
                .HasMaxLength(100);

            builder.Property(p => p.NumeroMesa)
                .HasColumnName("numero_mesa");
        }
    }
}
