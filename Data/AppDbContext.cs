using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data 
{    public class AppDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
				
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}