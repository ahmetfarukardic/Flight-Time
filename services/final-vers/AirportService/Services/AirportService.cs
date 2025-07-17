using AirportService.Data;
using AirportService.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportService.Services
{
    public class AirportService
    {
        private readonly AirportDbContext _context;

        public AirportService(AirportDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetDepartureAndDestinationLocations(string departure, string destination)
        {
            var codes = new List<string> { departure.ToUpper(), destination.ToUpper() };

            var airports = await _context.Airports
                                         .Where(a => codes.Contains(a.IataCode.ToUpper()))
                                         .ToListAsync();

            if (airports.Count != 2)
                return null;

            var locationIds = airports.Select(a => a.LocationId).ToList();

            var locations = await _context.Locations
                                          .Where(l => locationIds.Contains(l.Id))
                                          .ToListAsync();

            var result = new
            {
                Departure = airports.Where(a => a.IataCode.Equals(departure, StringComparison.OrdinalIgnoreCase))
                                    .Select(a => new
                                    {
                                        IataCode = a.IataCode,
                                        Location = locations
                                            .Where(l => l.Id == a.LocationId)
                                            .Select(l => new
                                            {
                                                l.Latitude,
                                                l.Longitude
                                            })
                                            .FirstOrDefault()
                                    })
                                    .FirstOrDefault(),

                Destination = airports.Where(a => a.IataCode.Equals(destination, StringComparison.OrdinalIgnoreCase))
                                      .Select(a => new
                                      {
                                          IataCode = a.IataCode,
                                          Location = locations
                                              .Where(l => l.Id == a.LocationId)
                                              .Select(l => new
                                              {
                                                  l.Latitude,
                                                  l.Longitude
                                              })
                                              .FirstOrDefault()
                                      })
                                      .FirstOrDefault()
            };

            return result;
        }
    }
}
