namespace Core.Application.Commands.Shelters;

public sealed class CreateShelterCommand
{
    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public int Capacity { get; set; }

    public string? ContactPhone { get; set; }

    public bool HasMedicalSupport { get; set; }
}