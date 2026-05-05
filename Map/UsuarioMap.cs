using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Lanchonete_Sprint.Map
{
    public class UsuarioMap :
        IEntityTypeConfiguration<Usuario>
    {
        public void Configure(
            EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios");

            builder.HasKey(u => u.IdUsuario);

            builder.Property(u => u.IdUsuario)
                .HasColumnName("id_usuario");

            builder.Property(u => u.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Senha)
                .HasColumnName("senha")
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}