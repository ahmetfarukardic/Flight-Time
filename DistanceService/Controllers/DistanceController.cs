using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DistanceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistanceController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public DistanceController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        private const string AirportServiceBaseUrl = "https://airportservice-c8e4eve9ghfhdfcn.germanywestcentral-01.azurewebsites.net/api/airports/code/";


        [HttpGet("between/{fromCode}/{toCode}")]
        public async Task<IActionResult> GetLocations(string fromCode, string toCode)
        {
            // 1️ From Airport bilgisi alınıyor
            var fromResponse = await _httpClient.GetAsync(AirportServiceBaseUrl + fromCode);
            if (!fromResponse.IsSuccessStatusCode)
                return NotFound($"Airport '{fromCode}' not found.");

            var fromJson = await fromResponse.Content.ReadAsStringAsync();
            var fromData = JObject.Parse(fromJson);
            var fromLocationId = fromData["locationId"]?.ToString();

            // 2️ To Airport bilgisi alınıyor
            var toResponse = await _httpClient.GetAsync(AirportServiceBaseUrl + toCode);
            if (!toResponse.IsSuccessStatusCode)
                return NotFound($"Airport '{toCode}' not found.");
                
            var toJson = await toResponse.Content.ReadAsStringAsync();
            var toData = JObject.Parse(toJson);
            var toLocationId = toData["locationId"]?.ToString();

            // 3️ Sonuç dönülüyor
            return Ok(new
            {
                FromAirport = fromCode,
                FromLocationId = fromLocationId,
                ToAirport = toCode,
                ToLocationId = toLocationId
            });
        }
    }
}
