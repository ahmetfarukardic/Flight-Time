namespace APIGateway.Services
{
    public interface IAirportServiceClient
    {
        Task<string> GetLocationsAsync(string departure, string destination);
    }
}
