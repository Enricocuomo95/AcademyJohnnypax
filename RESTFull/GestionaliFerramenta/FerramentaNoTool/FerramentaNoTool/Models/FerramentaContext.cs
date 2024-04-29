using Microsoft.EntityFrameworkCore;

namespace FerramentaNoTool.Models
{
    public class FerramentaContext : DbContext
    {
        public FerramentaContext(DbContextOptions<FerramentaContext> options) : base(options) { }

        public DbSet<Prodotto> Prodotti { get; set; }
    }
}
