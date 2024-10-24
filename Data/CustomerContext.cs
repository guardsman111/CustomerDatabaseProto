using Microsoft.EntityFrameworkCore;

namespace CustomerDatabaseProto.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public DbSet<CustomerItem> CustomerItems { get; set; } = null!;
    }
}
