using System.Net.Http;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    public class HttpAircraftServiceClient : IAircraftServiceClient
    {
        private readonly HttpClient _httpClient;
        private const string AircraftServiceBaseUrl = "https://aircraftservice-ffe9htbfgmbsg0gj.germanywestcentral-01.azurewebsites.net/api/aircraft";

        public HttpAircraftServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<string> GetAircraftByTypeNameAsync(string typeName)
        {
            var response = await _httpClient.GetAsync($"{AircraftServiceBaseUrl}/{typeName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
