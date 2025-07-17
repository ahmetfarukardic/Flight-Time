using APIGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("gateway")]
    public class GatewayController : ControllerBase
    {
        private readonly IAirportServiceClient _airportServiceClient;

        public GatewayController(IAirportServiceClient airportServiceClient)
        {
            _airportServiceClient = airportServiceClient;
        }

        [HttpGet("locations/{departure}/{destination}")]
        public async Task<IActionResult> GetLocationsViaAirportService(string departure, string destination)
        {
            var content = await _airportServiceClient.GetLocationsAsync(departure, destination);
            return Content(content, "application/json");
        }

    }
}
