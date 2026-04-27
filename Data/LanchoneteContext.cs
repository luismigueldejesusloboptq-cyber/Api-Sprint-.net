using Api_Lanchonete_Sprint.Map;
using Api_Lanchonete_Sprint.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;




namespace Api_Lanchonete_Sprint.Data
{
    public class LanchoneteContext : DbContext
    {
        public LanchoneteContext(DbContextOptions<LanchoneteContext> options) : base(options) 
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categorias> Categoria { get; set; }
        public DbSet<Produtos> Produto { get; set; }
        public DbSet<ItemVenda> ItemVenda { get; set; }
        public DbSet<Venda> vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new VendaMap());
            modelBuilder.ApplyConfiguration(new ItemVendaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
