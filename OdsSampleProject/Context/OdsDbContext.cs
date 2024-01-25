using Microsoft.EntityFrameworkCore;
using OdsSampleProject.Models;

namespace OdsSampleProject.Context
{
    public class OdsDbContext : DbContext
    {
        public OdsDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<City> City { get; set; }
        public DbSet<Region> Region { get; set; }
    }
}
