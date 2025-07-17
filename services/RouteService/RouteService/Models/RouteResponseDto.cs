namespace RouteService.Models
{
    public class RouteResponseDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double TotalDistanceInKm { get; set; }
        public double GreatCircleDistance { get; set; }
    }
}
