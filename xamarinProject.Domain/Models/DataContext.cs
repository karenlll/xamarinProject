namespace xamarinProject.Domain.Models
{
    using Microsoft.EntityFrameworkCore;
    using xamarinProject.Common.Models;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
    }
}
