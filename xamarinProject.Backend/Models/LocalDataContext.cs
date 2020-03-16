namespace xamarinProject.Backend.Models
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using xamarinProject.Common.Models;

    public class LocalDataContext : DataContext
    {
        public LocalDataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }
    }
}
