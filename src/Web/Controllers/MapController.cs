using AutoMapper;
using Core.Application.Queries.Sos;
using Core.Application.Services;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs.Responses;

namespace Web.Controllers;

[ApiController]
[Route("api/map")]
public sealed class MapController : ControllerBase
{
    private readonly SosService _sosService;
    private readonly IMapper _mapper;

    public MapController(SosService sosService, IMapper mapper)
    {
        _sosService = sosService;
        _mapper = mapper;
    }

    [HttpGet("sos")]
    public async Task<ActionResult<IReadOnlyList<SosMapItemResponse>>> GetSosMap(
        [FromQuery] double minLng,
        [FromQuery] double minLat,
        [FromQuery] double maxLng,
        [FromQuery] double maxLat,
        [FromQuery] int? zoom,
        [FromQuery] SosStatus? status,
        CancellationToken cancellationToken)
    {
        var items = await _sosService.GetMapItemsAsync(new GetSosMapQuery
        {
            MinLng = minLng,
            MinLat = minLat,
            MaxLng = maxLng,
            MaxLat = maxLat,
            Zoom = zoom,
            Status = status
        }, cancellationToken);

        return Ok(_mapper.Map<IReadOnlyList<SosMapItemResponse>>(items));
    }
}

