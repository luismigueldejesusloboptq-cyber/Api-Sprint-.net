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

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<fornecedores> fornecedores  { get; set; }
        public DbSet<ItensPedido> ItensPedidos { get; set; }
        public DbSet<Pedidos> pedidos { get; set; }
        
        public DbSet<Produtos> produto { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriasMap());
            modelBuilder.ApplyConfiguration(new fornecedoresMap());
            modelBuilder.ApplyConfiguration(new ItensPedidoMap());
            modelBuilder.ApplyConfiguration(new PedidosMap());
            modelBuilder.ApplyConfiguration(new ProdutosMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
