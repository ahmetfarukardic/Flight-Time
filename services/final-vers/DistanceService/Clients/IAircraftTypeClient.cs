public interface IAircraftTypeClient
    {
         Task<int?> GetAircraftSpeedAsync(string aircraftType);
    }

