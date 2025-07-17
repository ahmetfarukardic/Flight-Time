namespace RouteService.Models
{
    public class Routes
    {
        public string? Id { get; set; }
        public AirportDto Origin { get; set; }
        public AirportDto Destination { get; set; }
        public List<WayPoints> Waypoints { get; set; } = new List<WayPoints>();
        public double TotalDistanceInKm { get; set; }
        public double GreatCircleDistance { get; set; }
        
    }
}
