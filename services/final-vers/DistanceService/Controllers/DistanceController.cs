using DistanceService.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/distance")]
public class DistanceController : ControllerBase
{
    private readonly IAirportLocationClient _airportClient;
    private readonly IGreatCircleDistanceCalculator _distanceCalculator;

    public DistanceController(IAirportLocationClient airportClient, IGreatCircleDistanceCalculator distanceCalculator)
    {
        _airportClient = airportClient;
        _distanceCalculator = distanceCalculator;
    }

    [HttpGet("{departure}/{destination}")]
    public async Task<IActionResult> GetDistance(string departure, string destination)
    {
        var waypoints = await _airportClient.GetLocationsAsync(departure, destination);

        if (waypoints == null)
            return NotFound("Havaalanları bulunamadı.");

        int distance = await _distanceCalculator.CalculateGreatCircleDistanceAsync(waypoints);

        return Ok(new { DistanceInKm = distance });
    }
}
