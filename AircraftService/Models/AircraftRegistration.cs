using System;

public class AircraftRegistration
{
    public Guid Id { get; set; }
    public string? ManufacturerName { get; set; }
    public string? TypeName { get; set; }
    public string? TypeCode { get; set; }
    public string? Icao { get; set; }
    public decimal? CabinHeight { get; set; }
    public decimal? CabinWidth { get; set; }
    public decimal? CabinLength { get; set; }
    public decimal? CabinVolume { get; set; }
    public int? SpeedMph { get; set; }
}
