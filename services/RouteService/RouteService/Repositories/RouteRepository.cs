using Microsoft.EntityFrameworkCore;
using RouteService.Data;
using RouteService.Models;
using System;
using System.Collections.Generic;

namespace RouteService.Repositories
{
    public interface IRouteRepository
    {
        Task SaveRouteAsync(Routes route);
        Task<Routes?> GetRouteByIdAsync(string id);
    }

    public class RouteRepository : IRouteRepository
    {
        private readonly ApplicationDbContext _context;

        public RouteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveRouteAsync(Routes route)
        {
            _context.RoutesDb.Add(route);
            await _context.SaveChangesAsync();
        }

        public async Task<Routes?> GetRouteByIdAsync(string id)
        {
            return await _context.RoutesDb
                .Include(r => r.Waypoints)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
