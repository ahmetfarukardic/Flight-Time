using AircraftService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AircraftService.Controllers
{
    [ApiController]
    [Route("api/aircraft")]
    public class AircraftController : ControllerBase
    {
        private readonly IAircraftService _aircraftService;

        public AircraftController(IAircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        [HttpGet("{typeName}")]
        public async Task<IActionResult> GetAircraftByTypeName(string typeName)
        {
            var aircraft = await _aircraftService.GetAircraftByTypeNameAsync(typeName);

            if (aircraft == null)
                return NotFound();

            return Ok(aircraft);
        }

        [HttpGet("speed/{typeName}")]
        public async Task<IActionResult> GetAircraftSpeed(string typeName)
        {
            var speed = await _aircraftService.GetAircraftSpeedByTypeNameAsync(typeName);

            if (speed == null)
                return NotFound("Aircraft not found.");

            return Ok(speed);
        }

    }
}
