using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Lanchonete_Sprint.Map
{
    public class ProdutosMap : IEntityTypeConfiguration<Produtos>
    {
        public void Configure(EntityTypeBuilder<Produtos> builder)
        {
            
            builder.ToTable("produtos");

          
            builder.HasKey(p => p.IdProduto);
            builder.Property(p => p.IdProduto).HasColumnName("id_produto");

         
            builder.Property(p => p.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Preco)
                .HasColumnName("preco")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

      
            builder.Property(p => p.IdCategoria).HasColumnName("id_categoria").IsRequired();
            builder.Property(p => p.IdFornecedor).HasColumnName("id_fornecedor").IsRequired();

           
  
            // Relação: Produto pertence a uma Categoria
            builder.HasOne(p => p.Categorias)
                .WithMany(c => c.Produto)
                .HasForeignKey(p => p.IdCategoria);

            // Relação: Produto pertence a um Fornecedor
            builder.HasOne(p => p.Fornecedores)
                .WithMany(f => f.Produto)
                .HasForeignKey(p => p.IdFornecedor);
        }
    }
}