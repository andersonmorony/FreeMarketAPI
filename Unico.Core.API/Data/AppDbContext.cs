using Microsoft.EntityFrameworkCore;

namespace Unico.Core.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Market> Markets { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
