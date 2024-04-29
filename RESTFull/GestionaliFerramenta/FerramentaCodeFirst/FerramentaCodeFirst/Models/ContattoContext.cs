using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FerramentaCodeFirst.Models
{
    public class ContattoContext : DbContext
    {
        public ContattoContext (DbContextOptions<ContattoContext> options): base(options) { }
    }
}
