using DistanceService.Models;

public interface IAirportLocationClient
{
    Task<List<LocationDto>> GetLocationsAsync(string departure, string destination);
}
