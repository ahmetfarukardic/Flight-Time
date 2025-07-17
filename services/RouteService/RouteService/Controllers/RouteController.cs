using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RouteService.Data;
using RouteService.Models;
using RouteService.Repositories;

namespace RouteService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IGreatCircleDistanceCalculator _distanceCalculator;
        private readonly HttpClient _httpClient;
        private readonly IRouteRepository _routeRepository;
        private readonly ApplicationDbContext _context;

        public RouteController(IHttpClientFactory factory , IGreatCircleDistanceCalculator distanceCalculator , ApplicationDbContext context)
        {
            _httpClient = factory.CreateClient("AirportService");
            _distanceCalculator = distanceCalculator;
            _context = context;
        }

        [HttpPost("calculate")]
        public IActionResult CalculateDistance([FromBody] RouteRequestDto request)
        {
            if (request == null || request.Origin == null || request.Destination == null)
            {
                return BadRequest("Invalid request data.");
            }

            var waypoints = new List<AirportDto> { request.Origin, request.Destination };
            int distance = _distanceCalculator.CalculateGreatCircleDistance(waypoints);

            return Ok(new { DistanceKm = distance });
        }

        [HttpPost("calculate-and-save")]
        public async Task<IActionResult> CalculateAndSaveRoute([FromBody] Routes route)
        {
            if (route == null || route.Origin == null || route.Destination == null)
                return BadRequest("Invalid route data");

            // Build full route: origin → waypoints → destination
            var fullRoute = new List<AirportDto> { route.Origin };
            fullRoute.AddRange(route.Waypoints.Select(wp => new AirportDto
            {
                Latitude = wp.Latitude,
                Longitude = wp.Longitude
            }));
            fullRoute.Add(route.Destination);

            // Calculate distance
            route.GreatCircleDistance = _distanceCalculator.CalculateGreatCircleDistance(fullRoute);
            route.TotalDistanceInKm = route.GreatCircleDistance; // or modify if using roads later

            // Save to DB
            _context.RoutesDb.Add(route);
            await _context.SaveChangesAsync();

            return Ok(route);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteById(string id)
        {
            var route = await _context.RoutesDb
                .Include(r => r.Waypoints)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null)
                return NotFound();

            return Ok(route);
        }
    }
}
