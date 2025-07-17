namespace DistanceService.Services
{
    using DistanceService.Models;

    public interface IGreatCircleDistanceCalculator
    {
        Task<int> CalculateGreatCircleDistanceAsync(List<LocationDto> waypoints);
    }

    public class DistanceCalculator : IGreatCircleDistanceCalculator
    {
        private const double EarthRadiusKm = 6371.0;

        public async Task<int> CalculateGreatCircleDistanceAsync(List<LocationDto> waypoints)
        {
            if (waypoints == null || waypoints.Count != 2)
                throw new ArgumentException("Exactly two waypoints are required.", nameof(waypoints));

            return await Task.Run(() =>
            {
                return (int)Math.Round(CalculateDistanceBetween(waypoints[0], waypoints[1]));
            });
        }


        private double CalculateDistanceBetween(LocationDto a, LocationDto b)
        {
            double lat1 = ToRadians(a.Latitude);
            double lon1 = ToRadians(a.Longitude);
            double lat2 = ToRadians(b.Latitude);
            double lon2 = ToRadians(b.Longitude);

            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            double haversine =
                Math.Pow(Math.Sin(dLat / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Pow(Math.Sin(dLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(haversine), Math.Sqrt(1 - haversine));

            double distance = EarthRadiusKm * c;

            return distance;
        }


        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
