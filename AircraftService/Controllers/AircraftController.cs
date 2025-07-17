using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AircraftService.Data;
using AircraftService.Models;

namespace AircraftService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AircraftsController : ControllerBase
    {
        private readonly AircraftDbContext _context;

        public AircraftsController(AircraftDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aircraft>> GetAircraft(Guid id)
        {
            var Aircraft = await _context.Aircrafts.FindAsync(id);

            if (Aircraft == null)
                return NotFound();

            return Aircraft;
        }

        [HttpGet]
        public async Task<IActionResult> GetAircrafts()
        {
            var Aircrafts = await _context.Aircrafts.ToListAsync();
            return Ok(Aircrafts);
        }

        [HttpPost]
        public async Task<ActionResult<Aircraft>> CreateAircraft(Aircraft Aircraft)
        {
            _context.Aircrafts.Add(Aircraft);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAircraft), new { id = Aircraft.Id }, Aircraft);
        }

        
        [HttpGet("icao/{code}")]
        public async Task<ActionResult<Aircraft>> GetAircraftByCode(string code)
        {
            var Aircraft = await _context.Aircrafts.FirstOrDefaultAsync(a => a.Icao == code);

            if (Aircraft == null)
                return NotFound();

            return Aircraft;
        }

    }
}
