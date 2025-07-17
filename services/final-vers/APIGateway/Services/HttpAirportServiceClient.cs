using System.Net.Http;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    public class HttpAirportServiceClient : IAirportServiceClient
    {
        private readonly HttpClient _httpClient;
        private const string AirportServiceBaseUrl = "https://airportservice-c8e4eve9ghfhdfcn.germanywestcentral-01.azurewebsites.net/api/airports";
        private const string DistanceServiceBaseUrl = "https://distanceservice-ezd9apa9e9fphqeg.germanywestcentral-01.azurewebsites.net/api/distance";

        public HttpAirportServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetLocationsAsync(string departure, string destination)
        {
            var response = await _httpClient.GetAsync($"{AirportServiceBaseUrl}/locations/{departure}/{destination}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
