using AirportService.Data;
using AirportService.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportService.Repositories
{
    public interface IAirportRepository
    {
        Task<Airport?> GetByCodeAsync(string code);
        Task<List<Airport>> GetAllAsync();
    }

    public class AirportRepository : IAirportRepository
    {
        private readonly AppDbContext _context;

        public AirportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Airport?> GetByCodeAsync(string code)
        {
            return await _context.Airports
                .FirstOrDefaultAsync(a => a.IcaoCode == code || a.IataCode == code);
        }

        public async Task<List<Airport>> GetAllAsync()
        {
            return await _context.Airports.ToListAsync();
        }
    }
}