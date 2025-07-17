using RouteService.Models;

namespace RouteService.Repositories
{
    public interface IGreatCircleDistanceCalculator
    {
        int CalculateGreatCircleDistance(List<AirportDto> waypoints);
    }
    public class DistanceCalculator : IGreatCircleDistanceCalculator
    {
        private const double EarthRadiusKm = 6371.0;

        public int CalculateGreatCircleDistance(List<AirportDto> waypoints)
        {
            if (waypoints == null || waypoints.Count < 2)
                throw new ArgumentException("At least two waypoints are required.");

            double totalDistance = 0.0;

            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                var current = waypoints[i];
                var next = waypoints[i + 1];

                totalDistance += CalculateDistanceBetween(current, next);
            }

            return (int)Math.Round(totalDistance);
        }

        private double CalculateDistanceBetween(AirportDto a, AirportDto b)
        {
            double lat1 = ToRadians(a.Latitude);
            double lon1 = ToRadians(a.Longitude);
            double lat2 = ToRadians(b.Latitude);
            double lon2 = ToRadians(b.Longitude);

            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            double sinDLat = Math.Sin(dLat / 2);
            double sinDLon = Math.Sin(dLon / 2);

            double aCalc = sinDLat * sinDLat +
                           Math.Cos(lat1) * Math.Cos(lat2) *
                           sinDLon * sinDLon;

            double c = 2 * Math.Atan2(Math.Sqrt(aCalc), Math.Sqrt(1 - aCalc));

            return EarthRadiusKm * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
