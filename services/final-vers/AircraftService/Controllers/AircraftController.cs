using AircraftService.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
