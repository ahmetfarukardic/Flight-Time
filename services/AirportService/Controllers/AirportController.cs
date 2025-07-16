using Microsoft.AspNetCore.Mvc;
using AirportService.Repositories;

namespace AirportService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportRepository _airportRepo;

        public AirportsController(IAirportRepository airportRepo)
        {
            _airportRepo = airportRepo;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetAirportByCode(string code)
        {
            var airport = await _airportRepo.GetByCodeAsync(code);
            if (airport == null) return NotFound();

            return Ok(airport);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var airports = await _airportRepo.GetAllAsync();
            return Ok(airports);
        }

        [HttpGet("distance")]
        public async Task<IActionResult> GetDistanceBetweenAirports([FromQuery] string code1, [FromQuery] string code2)
        {
            var airport1 = await _airportRepo.GetByCodeAsync(code1);
            var airport2 = await _airportRepo.GetByCodeAsync(code2);

            if (airport1 == null || airport2 == null)
                return NotFound("One or both airports not found.");

            double distance = CalculateHaversineDistance(
                airport1.Latitude, airport1.Longitude,
                airport2.Latitude, airport2.Longitude
            );

            return Ok(new { code1, code2, distanceInKm = distance });
        }

        private double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371.0; // Dünya'nın yarıçapı (km)
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double angle)
        {
            return angle * Math.PI / 180.0;
        }


    }
}
