using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FlightService.Controllers
{
    [ApiController]
    [Route("api/flights")]
    public class FlightController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public FlightController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("plan")]
        public async Task<IActionResult> GetFlightPlan(
            [FromQuery] string aircraftCode,
            [FromQuery] string departureCode,
            [FromQuery] string arrivalCode)
        {
            // Uçak verisi al
            var aircraft = await _httpClient.GetFromJsonAsync<object>($"http://localhost:5001/api/aircrafts/{aircraftCode}");
            if (aircraft == null)
                return NotFound("Aircraft not found.");

            // Kalkış havaalanı al
            var departureAirport = await _httpClient.GetFromJsonAsync<object>($"http://localhost:5002/api/airports/{departureCode}");
            if (departureAirport == null)
                return NotFound("Departure airport not found.");

            // Varış havaalanı al
            var arrivalAirport = await _httpClient.GetFromJsonAsync<object>($"http://localhost:5002/api/airports/{arrivalCode}");
            if (arrivalAirport == null)
                return NotFound("Arrival airport not found.");

            // Mesafeyi al
            var distanceResponse = await _httpClient.GetFromJsonAsync<object>(
                $"http://localhost:5002/api/airports/distance?code1={departureCode}&code2={arrivalCode}"
            );

            return Ok(new
            {
                Aircraft = aircraft,
                From = departureAirport,
                To = arrivalAirport,
                Distance = distanceResponse
            });
        }
    }
}
