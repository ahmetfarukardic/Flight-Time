using System.Net.Http;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    public class HttpDistanceServiceClient : IDistanceServiceClient
    {
        private readonly HttpClient _httpClient;
        private const string DistanceServiceBaseUrl = "https://distanceservice-ezd9apa9e9fphqeg.germanywestcentral-01.azurewebsites.net/api/distance";

        public HttpDistanceServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDistanceAsync(string departure, string destination)
        {
            var response = await _httpClient.GetAsync($"{DistanceServiceBaseUrl}/{departure}/{destination}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetFlightTimeAsync(string departure, string destination, string aircraftType)
        {
            var url = $"{DistanceServiceBaseUrl}/{departure}/{destination}/{aircraftType}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStringAsync();
        }
    }
}
