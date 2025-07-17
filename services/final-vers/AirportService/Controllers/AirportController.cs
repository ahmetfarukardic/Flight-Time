using AirportService.Data;
using AirportService.Models;
using AirportService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirportService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportsController : ControllerBase
    {
        private readonly AirportDbContext _context;
        private readonly Services.AirportService _airportService;

        public AirportsController(AirportDbContext context, Services.AirportService airportService)
        {
            _context = context;
            _airportService = airportService;
        }
        
        [HttpGet("{IataCode}")]
        public async Task<ActionResult<Airport>> GetAirport(string IataCode)
        {
            var airport = await _context.Airports
                .FirstOrDefaultAsync(a => a.IataCode == IataCode);

            if (airport == null)
                return NotFound();

            return airport;
        }


        [HttpGet("locations/{departure}/{destination}")]
        public async Task<IActionResult> GetDepartureAndDestinationLocations(string departure, string destination)
        {
            var result = await _airportService.GetDepartureAndDestinationLocations(departure, destination);

            if (result == null)
                return NotFound("Departure veya destination havaalanı bulunamadı.");

            return Ok(result);
        }


    }
}
