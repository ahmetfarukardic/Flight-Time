using System.ComponentModel.DataAnnotations;

namespace AirportService.Models
{
    public class Airport
    {
        [Key]
        public Guid Id { get; set; }

        public string AirportCode { get; set; }
        public string IcaoCode { get; set; }
        public string IataCode { get; set; }
        public string FaaCode { get; set; }
        public string AirportName { get; set; }

        public Guid LocationId { get; set; }

        public int MaxRunway { get; set; }
        public string MaxRunwayMetric { get; set; }

        public int Elevation { get; set; }
        public string ElevationMetric { get; set; }

        public string Timezone { get; set; }
        public string TimezoneAbbrev { get; set; }
    }
}
