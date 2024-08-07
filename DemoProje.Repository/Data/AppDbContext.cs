

using DemoProje.Entitiy.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoProje.Repository.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
