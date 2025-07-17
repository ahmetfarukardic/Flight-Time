using System.ComponentModel.DataAnnotations;

namespace RouteService.Models
{
    public class WayPoints
    {
        [Key]
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int AltitudeFeet { get; set; }
        public string Name { get; set; }
    }
}
