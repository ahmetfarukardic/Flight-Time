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
        

        public RouteController(
        IHttpClientFactory factory,
        IGreatCircleDistanceCalculator distanceCalculator,
        IRouteRepository routeRepository 
       )
        {
            _httpClient = factory.CreateClient("AirportService");
            _distanceCalculator = distanceCalculator;
            _routeRepository = routeRepository; 
            
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateDistance([FromQuery] RouteRequestDto request)
        {
            if (request == null || request.Origin == null || request.Destination == null)
            {
                return BadRequest("Invalid request data.");
            }

            var waypoints = new List<AirportDto> { request.Origin, request.Destination };
            int distance = await _distanceCalculator.CalculateGreatCircleDistanceAsync(waypoints);

            return Ok(new { DistanceKm = distance });
        }

        [HttpPost("calculate-and-save")]
        public async Task<IActionResult> CalculateAndSaveRoute([FromBody] Routes route)
        {
            if (route == null || route.Origin == null || route.Destination == null)
                return BadRequest("Invalid route data");

            // Build full route: origin → waypoints → destination
            var fullRoute = new List<AirportDto> { route.Origin };
            if (route.Waypoints != null)
            {
                fullRoute.AddRange(route.Waypoints.Select(wp => new AirportDto
                {
                    Latitude = wp.Latitude,
                    Longitude = wp.Longitude,
                    Id = wp.Id,
                    IataCode = wp.Name
                }));
            }
            fullRoute.Add(route.Destination);

            // Calculate distance asynchronously
            int distance = await _distanceCalculator.CalculateGreatCircleDistanceAsync(fullRoute);
            route.GreatCircleDistance = distance;
            route.TotalDistanceInKm = distance; // you can adjust if you have other distance measures later

            // Save using repository
            await _routeRepository.SaveRouteAsync(route);

            return Ok(route);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteById(string id)
        {
            var route = await _routeRepository.GetRouteByIdAsync(id);
            if (route == null)
                return NotFound();

            return Ok(route);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoutes()
        {
            return Ok(new List<Routes>());
        }
    }
}
