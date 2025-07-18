using AircraftService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AircraftService.Data
{
    public class AircraftDbContext : DbContext
    {
        public AircraftDbContext(DbContextOptions<AircraftDbContext> options) : base(options)
        {
        }

        public DbSet<AircraftRegistration> AircraftRegistrations { get; set; }
        
    }
}
