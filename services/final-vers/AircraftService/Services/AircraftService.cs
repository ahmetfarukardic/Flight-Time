﻿using AircraftService.Data;
using AircraftService.Models;
using Microsoft.EntityFrameworkCore;

namespace AircraftService.Services
{
    public interface IAircraftService
    {
        Task<AircraftRegistration> GetAircraftByTypeNameAsync(string typeName);
        Task<int?> GetAircraftSpeedByTypeNameAsync(string typeName);
    }

    public class AircraftService : IAircraftService
    {
        private readonly AircraftDbContext _context;

        public AircraftService(AircraftDbContext context)
        {
            _context = context;
        }

        public async Task<AircraftRegistration> GetAircraftByTypeNameAsync(string typeName)
        {
            return await _context.AircraftRegistrations
                .FirstOrDefaultAsync(a => a.TypeName == typeName);
        }

        public async Task<int?> GetAircraftSpeedByTypeNameAsync(string typeName)
        {
            var aircraft = await GetAircraftByTypeNameAsync(typeName);

            return aircraft?.SpeedMph;
        }
    }
}
