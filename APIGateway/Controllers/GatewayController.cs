using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("gateway")]
    public class GatewayController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public GatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        private const string AirportServiceBaseUrl = "https://airportservice-c8e4eve9ghfhdfcn.germanywestcentral-01.azurewebsites.net/api/airports";
        private const string DistanceServiceBaseUrl = "https://distanceservice-ezd9apa9e9fphqeg.germanywestcentral-01.azurewebsites.net/api/distance";


        [HttpGet("airports")]
        public async Task<IActionResult> GetAllAirports()
        {
            var response = await _httpClient.GetAsync(AirportServiceBaseUrl);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, error);
            }

            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpPost("airports")]
        public async Task<IActionResult> CreateAirport([FromBody] object airportJson)
        {
            var content = new StringContent(airportJson.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{AirportServiceBaseUrl}", content);
            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        [HttpGet("distance/{fromCode}/{toCode}")]
        public async Task<IActionResult> GetDistance(string fromCode, string toCode)
        {
            var response = await _httpClient.GetAsync($"{DistanceServiceBaseUrl}/between/{fromCode}/{toCode}");
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}
