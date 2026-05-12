using Core.Domain.Enums;

namespace Web.DTOs.Responses;

public sealed class ShelterResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public int Capacity { get; set; }

    public int CurrentOccupancy { get; set; }

    public int AvailableSlots { get; set; }

    public ShelterStatus Status { get; set; }

    public string? ContactPhone { get; set; }

    public bool HasMedicalSupport { get; set; }
}

