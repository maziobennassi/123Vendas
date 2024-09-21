using _123Vendas.Vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.Vendas.Infrastructure.Persistance
{
    internal class VendasDbContext(DbContextOptions<VendasDbContext> options) : DbContext(options)
    {
        internal DbSet<Venda> Vendas { get; set; }
        internal DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Venda>()
                .HasMany(v => v.Produtos)
                .WithOne()
                .HasForeignKey(p => p.VendaId);
        }
    }
}
