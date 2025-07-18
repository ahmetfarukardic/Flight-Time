namespace APIGateway.Services
{
    public interface IAirportLocationClient
    {
        Task<string> GetLocationAsync(string iatacode);
    }
    public interface IDistanceServiceClient
    {
        Task<string> GetDistanceAsync(string departure, string destination);
        Task<string> GetFlightTimeAsync(string departure, string destination, string aircraftType);
    }
    public interface IAircraftServiceClient
    {
        Task<string> GetAircraftByTypeNameAsync(string typeName);
    }

}
