namespace RouteService.Models
{
    public class RouteRequestDto
    {
        public AirportDto Origin { get; set; }
        public AirportDto Destination { get; set; }
    }
}
