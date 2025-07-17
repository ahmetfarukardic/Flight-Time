using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirportService.Data;
using AirportService.Models;

namespace AirportService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportsController : ControllerBase
    {
        private readonly AirportDbContext _context;

        public AirportsController(AirportDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Airport>> GetAirport(Guid id)
        {
            var airport = await _context.Airports.FindAsync(id);

            if (airport == null)
                return NotFound();

            return airport;
        }

        [HttpGet]
        public async Task<IActionResult> GetAirports()
        {
            var airports = await _context.Airports.ToListAsync();
            return Ok(airports);
        }

        [HttpPost]
        public async Task<ActionResult<Airport>> CreateAirport(Airport airport)
        {
            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAirport), new { id = airport.Id }, airport);
        }
        [HttpGet("code/{code}")]
        public async Task<ActionResult<Airport>> GetAirportByCode(string code)
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(a => a.AirportCode == code);

            if (airport == null)
                return NotFound();

            return airport;
        }


    }
}
