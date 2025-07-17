using AirportService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AirportService.Data
{
    public class AirportDbContext : DbContext
    {
        public AirportDbContext(DbContextOptions<AirportDbContext> options) : base(options)
        {
        }

        public DbSet<Airport> Airports { get; set; }

        public DbSet<Location> Locations { get; set; }

    }
}
