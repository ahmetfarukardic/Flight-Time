using Newtonsoft.Json;

public class HttpAircraftTypeClient : IAircraftTypeClient
{
    private readonly HttpClient _httpClient;
    private const string AircraftServiceUrl = "https://aircraftservice-ffe9htbfgmbsg0gj.germanywestcentral-01.azurewebsites.net/api/aircraft/speed/";
    public HttpAircraftTypeClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<int?> GetAircraftSpeedAsync(string AircraftType)
    {
        var response = await _httpClient.GetAsync(AircraftServiceUrl + AircraftType);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<int>(json);
    }
}
