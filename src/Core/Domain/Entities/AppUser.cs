using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

public sealed class AppUser : SoftDeletableEntity
{
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<UserLocation> Locations { get; set; } = new List<UserLocation>();

    public ICollection<SosRequest> SosRequests { get; set; } = new List<SosRequest>();
}
