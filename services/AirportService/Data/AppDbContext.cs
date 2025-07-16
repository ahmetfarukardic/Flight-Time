using Microsoft.EntityFrameworkCore;
using AirportService.Models;

namespace AirportService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Airport> Airports { get; set; }
    }
}
