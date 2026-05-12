using Core.Domain.Enums;

namespace Web.DTOs.Requests;

public sealed class UpdateShelterRequest
{
    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public int CurrentOccupancy { get; set; }

    public ShelterStatus Status { get; set; }

    public string? ContactPhone { get; set; }

    public bool HasMedicalSupport { get; set; }
}

