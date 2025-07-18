using Newtonsoft.Json;
using DistanceService.Models;

public class HttpAirportLocationClient : IAirportLocationClient
{
    private readonly HttpClient _httpClient;
    private const string AirportServiceUrl = "https://airportservice-c8e4eve9ghfhdfcn.germanywestcentral-01.azurewebsites.net/api/airports/locations/";

    public HttpAirportLocationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LocationDto>> GetLocationsAsync(string departure, string destination)
    {
        var response = await _httpClient.GetAsync(AirportServiceUrl + departure + "/" + destination);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        var locationsResponse = JsonConvert.DeserializeObject<AirportLocationsResponseDto>(json);

        if (locationsResponse == null)
            return null;

        return new List<LocationDto>
        {
            locationsResponse.Departure.Location,
            locationsResponse.Destination.Location
        };
    }
}
