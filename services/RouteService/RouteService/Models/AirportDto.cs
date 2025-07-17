namespace RouteService.Models
{
    public class AirportDto
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string IataCode { get; set; } = string.Empty;
    }
}
