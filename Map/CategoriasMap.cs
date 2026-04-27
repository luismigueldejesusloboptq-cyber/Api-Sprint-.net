using Api_Lanchonete_Sprint.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Api_Lanchonete_Sprint.Map
{
    public class CategoriasMap : IEntityTypeConfiguration<Categorias>
    {
        public void Configure(EntityTypeBuilder<Categorias> builder)
        {
            builder.ToTable("categorias");

            builder.HasKey(c => c.Idcategoria);
            builder.Property(c => c.Idcategoria).HasColumnName("id_categoria");

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

        }
    }
}
