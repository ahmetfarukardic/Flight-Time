using DistanceService.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/distance")]
public class DistanceController : ControllerBase
{
    private readonly IAirportLocationClient _airportClient;
    private readonly IGreatCircleDistanceCalculator _distanceCalculator;
    private readonly IAircraftTypeClient _aircraftTypeClient;

    public DistanceController(IAirportLocationClient airportClient, IGreatCircleDistanceCalculator distanceCalculator, IAircraftTypeClient aircraftTypeClient)
    {
        _airportClient = airportClient;
        _distanceCalculator = distanceCalculator;
        _aircraftTypeClient = aircraftTypeClient;
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

    [HttpGet("{departure}/{destination}/{AircraftType}")]

    public async Task<IActionResult> GetFlightTime(string departure, string destination, string AircraftType) {
        
        var waypoints = await _airportClient.GetLocationsAsync(departure, destination);
        if (waypoints == null)
            return NotFound("Havaalanları bulunamadı.");
        int distance = await _distanceCalculator.CalculateGreatCircleDistanceAsync(waypoints);
        //finding speed of aircraft
        var speedMph = await _aircraftTypeClient.GetAircraftSpeedAsync(AircraftType);

        if (speedMph == null || speedMph <= 0)
            return NotFound("Uçak tipi bulunamadı veya hız bilgisi eksik.");

        double speedKph = speedMph.Value * 1.60934; // mph → kph dönüşümü

        double flightTimeHours = distance / speedKph;

        // Saat ve dakika olarak parçala:
        int hours = (int)flightTimeHours;
        int minutes = (int)Math.Round((flightTimeHours - hours) * 60);

        string estimatedFlightTimeFormatted = $"{hours} hours {minutes} minutes";

        return Ok(new
        {
            DistanceKm = distance,
            SpeedMph = speedMph,
            SpeedKph = Math.Round(speedKph, 2),
            EstimatedFlightTime = estimatedFlightTimeFormatted
        });


    }

}
