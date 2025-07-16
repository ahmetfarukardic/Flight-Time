namespace AircraftService.Models;

public class Aircraft
{
    public int Id { get; set; }
    public string TailNumber { get; set; } = string.Empty;   // Uçağın kuyruk numarası
    public string Brand { get; set; } = string.Empty;        // Marka (ör. Boeing)
    public string Model { get; set; } = string.Empty;        // Model (ör. 737)
    public int EnginePower { get; set; }                     // Motor gücü (örneğin HP cinsinden)
    public double MaxSpeed { get; set; }                     // Azami hız (örneğin km/h)
}
