using RouteService.Data;
using RouteService.Models;
using System;
using System.Collections.Generic;

namespace RouteService.Repositories
{
    public interface IRouteRepository
    {
    

    }

    public class RouteRepository : IRouteRepository, IGreatCircleDistanceCalculator
    {
        private readonly ApplicationDbContext _context;

        public RouteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CalculateGreatCircleDistance(List<AirportDto> waypoints)
        {
            var totalDistanceInKm = new DistanceCalculator();
            var result = totalDistanceInKm.CalculateGreatCircleDistance(waypoints);
            return result;
        }
    }
}
