using Core.Application.Common;
using Core.Domain.Enums;

namespace Core.Application.Queries.Sos;

public sealed class GetSosMapQuery : BboxQuery
{
    public SosStatus? Status { get; set; }
}

