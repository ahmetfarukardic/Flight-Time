namespace DistanceService.Models
{
    public class AirportLocationsResponseDto
    {
        public NestedAirportLocation Departure { get; set; }
        public NestedAirportLocation Destination { get; set; }
    }

    public class NestedAirportLocation
    {
        public string IataCode { get; set; }
        public LocationDto Location { get; set; }
    }
}
