using AircraftService.Data;
using AircraftService.Models;
using Microsoft.EntityFrameworkCore;

namespace AircraftService.Repositories
{
    public interface IAircraftsRepository
    {
        Task<Aircraft?> GetByCodeAsync(string code);
        Task<List<Aircraft>> GetAllAsync();
    }

    public class AircraftRepository : IAircraftsRepository
    {
        private readonly AppDbContext _context;

        public AircraftRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Aircraft?> GetByCodeAsync(string code)
        {
            return await _context.Aircrafts
                .FirstOrDefaultAsync(a => a.TailNumber == code);
        }

        public async Task<List<Aircraft>> GetAllAsync()
        {
            return await _context.Aircrafts.ToListAsync();
        }
    }
}