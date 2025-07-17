using System.ComponentModel.DataAnnotations;

namespace RouteService.Models
{
    public class Routes
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public AirportDto Origin { get; set; }
        public AirportDto Destination { get; set; }
        public List<WayPoints> Waypoints { get; set; } = new List<WayPoints>();
        public double TotalDistanceInKm { get; set; }
        public double GreatCircleDistance { get; set; }
        
    }
}
