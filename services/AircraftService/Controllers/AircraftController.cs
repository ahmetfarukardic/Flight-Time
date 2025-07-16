using Microsoft.AspNetCore.Mvc;
using AircraftService.Repositories;

namespace AircraftService.Controllers
{
    [ApiController]
    [Route("api/aircrafts")]
    public class AircraftsController : ControllerBase
    {
        private readonly IAircraftsRepository _aircraftRepo;

        public AircraftsController(IAircraftsRepository aircraftRepo)
        {
            _aircraftRepo = aircraftRepo;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetaircraftByCode(string code)
        {
            var aircraft = await _aircraftRepo.GetByCodeAsync(code);
            if (aircraft == null) return NotFound();

            return Ok(aircraft);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var aircrafts = await _aircraftRepo.GetAllAsync();
            return Ok(aircrafts);
        }
    }
}
