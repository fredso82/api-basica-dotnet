using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Context
{
    public class ApiContext : DbContext
    {
        //com sql server
        public ApiContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Produto> Produtos { get; set; }

        //com sqlite
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=webapi.db");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);

            modelBuilder.Entity<Produto>().Property(p => p.Sku)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Produto>().Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Produto>().ToTable("Produtos");
        }
    }
}
