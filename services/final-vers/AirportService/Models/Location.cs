using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportService.Models
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; }

        public string? RegionName { get; set; }

        public string? CountryName { get; set; }

        public string? CountryCode { get; set; }

        public string? StateName { get; set; }

        public string? StateCode { get; set; }

        public string? City { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Longitude { get; set; }

        public bool? IsCity { get; set; }
    }
}
