using Microsoft.EntityFrameworkCore;

namespace RealTime.Models
{
    public class ProdDB : DbContext
    {
        public ProdDB(DbContextOptions<ProdDB> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
