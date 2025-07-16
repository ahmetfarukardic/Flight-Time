using Microsoft.EntityFrameworkCore;
using AircraftService.Models;

namespace AircraftService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Aircraft> Aircrafts { get; set; }
    }
}
