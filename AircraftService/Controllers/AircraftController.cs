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
        public async Task<ActionResult<AircraftRegistration>> GetAircraft(string id)
        {
            var Aircraft = await _context.AircraftRegistrations.FindAsync(id);

            if (Aircraft == null)
                return NotFound();

            return Aircraft;
        }

        [HttpGet]
        public async Task<IActionResult> GetAircrafts()
        {
            var Aircrafts = await _context.AircraftRegistrations.ToListAsync();
            return Ok(Aircrafts);
        }

        [HttpPost]
        public async Task<ActionResult<AircraftRegistration>> CreateAircraft(AircraftRegistration Aircraft)
        {
            _context.AircraftRegistrations.Add(Aircraft);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAircraft), new { id = Aircraft.Id }, Aircraft);
        }

    }
}
