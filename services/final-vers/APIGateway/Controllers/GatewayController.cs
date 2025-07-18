using APIGateway.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("gateway")]
    public class GatewayController : ControllerBase
    {
        private readonly IAirportLocationClient _airportServiceClient;
        private readonly IDistanceServiceClient _distanceServiceClient;
        private readonly IAircraftServiceClient _aircraftServiceClient;

        public GatewayController(IAirportLocationClient airportServiceClient, IDistanceServiceClient distanceServiceClient, IAircraftServiceClient aircraftServiceClient)
        {
            _airportServiceClient = airportServiceClient;
            _distanceServiceClient = distanceServiceClient;
            _aircraftServiceClient = aircraftServiceClient;
        }

        [HttpGet("location/{iataCode}")]
        public async Task<IActionResult> GetLocationViaAirportService(string iataCode)
        {
            var content = await _airportServiceClient.GetLocationAsync(iataCode);
            return Content(content, "application/json");
        }

        [HttpGet("distance/{departure}/{destination}")]
        public async Task<IActionResult> GetDistanceViaDistanceService(string departure, string destination)
        {
            var content = await _distanceServiceClient.GetDistanceAsync(departure, destination);
            return Content(content, "application/json");
        }
        
        [HttpGet("aircraft/{typeName}")]
        public async Task<IActionResult> GetAircraftViaAircraftService(string typeName)
        {
            var content = await _aircraftServiceClient.GetAircraftByTypeNameAsync(typeName);

            if (string.IsNullOrEmpty(content))
                return NotFound();

            return Content(content, "application/json");
        }

        [HttpGet("flighttime/{departure}/{destination}/{aircraftType}")]
        public async Task<IActionResult> GetFlightTimeFromDistanceService(string departure, string destination, string aircraftType)
        {
            var content = await _distanceServiceClient.GetFlightTimeAsync(departure, destination, aircraftType);

            if (content == null)
                return NotFound("Uçuş süresi hesaplanamadı.");

            return Content(content, "application/json");
        }

    }
}
