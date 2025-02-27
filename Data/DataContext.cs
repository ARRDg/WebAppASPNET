using Microsoft.EntityFrameworkCore;

namespace WebAppASPNET.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
