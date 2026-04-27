using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Lanchonete_Sprint.Map
{
    public class fornecedoresMap : IEntityTypeConfiguration<fornecedores>
    {
        public void Configure(EntityTypeBuilder<fornecedores> builder)
        {
            builder.ToTable("fornecedores");

            builder.HasKey(f => f.IdFornecedor );
            builder.Property(f => f.IdFornecedor).HasColumnName("id_fornecedor");

            builder.Property(f => f.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(f => f.Contato)
                .HasColumnName("contato")
                .HasMaxLength(100);
        }
    }
}