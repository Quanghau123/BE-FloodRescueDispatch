using AutoMapper;
using Core.Application.Queries.Geo;
using Core.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs.Responses;

namespace Web.Controllers;

[ApiController]
[Route("api/alerts")]
public sealed class AlertsController : ControllerBase
{
    private readonly FloodAlertService _floodAlertService;
    private readonly IMapper _mapper;

    public AlertsController(FloodAlertService floodAlertService, IMapper mapper)
    {
        _floodAlertService = floodAlertService;
        _mapper = mapper;
    }

    [HttpGet("check")]
    public async Task<ActionResult<IReadOnlyList<AlertResponse>>> Check(
        [FromQuery] Guid userId,
        [FromQuery] double longitude,
        [FromQuery] double latitude,
        CancellationToken cancellationToken)
    {
        var result = await _floodAlertService.CheckAlertAsync(new CheckFloodAlertQuery
        {
            UserId = userId,
            Longitude = longitude,
            Latitude = latitude
        }, cancellationToken);

        return Ok(_mapper.Map<IReadOnlyList<AlertResponse>>(result));
    }
}

