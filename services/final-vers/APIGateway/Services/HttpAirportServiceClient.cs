using System.Net.Http;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    public class HttpAirportServiceClient : IAirportLocationClient
    {
        private readonly HttpClient _httpClient;
        private const string AirportServiceBaseUrl = "https://airportservice-c8e4eve9ghfhdfcn.germanywestcentral-01.azurewebsites.net/api/airports";

        public HttpAirportServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetLocationAsync(string iatacode)
        {
            var response = await _httpClient.GetAsync($"{AirportServiceBaseUrl}/{iatacode}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
