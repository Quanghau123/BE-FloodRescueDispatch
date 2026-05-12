using Core.Domain.Common;
using Core.Domain.Enums;
using NetTopologySuite.Geometries;

namespace Core.Domain.Entities;

public sealed class Shelter : SoftDeletableEntity
{
    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public Point Location { get; set; } = default!;

    public int Capacity { get; set; }

    public int CurrentOccupancy { get; set; }

    public ShelterStatus Status { get; set; } = ShelterStatus.Open;

    public string? ContactPhone { get; set; }

    public bool HasMedicalSupport { get; set; }

    public int AvailableSlots => Math.Max(0, Capacity - CurrentOccupancy);
}
