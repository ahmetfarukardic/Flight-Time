using Microsoft.EntityFrameworkCore;
using RouteService.Models;

namespace RouteService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Routes> RoutesDb { get; set; }
        public DbSet<WayPoints> WayPoints { get; set; }
    }
}
