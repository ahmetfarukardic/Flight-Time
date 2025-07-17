using System.ComponentModel.DataAnnotations;

namespace RouteService.Models
{
    public class RouteRequestDto
    {
        [Required]
        public AirportDto Origin { get; set; }

        [Required]
        public AirportDto Destination { get; set; }
    }
}
