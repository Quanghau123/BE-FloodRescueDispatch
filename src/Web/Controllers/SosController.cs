using AutoMapper;
using Core.Application.Commands.Sos;
using Core.Application.Queries.Sos;
using Core.Application.Services;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs.Requests;
using Web.DTOs.Responses;

namespace Web.Controllers;

[ApiController]
[Route("api/sos")]
public sealed class SosController : ControllerBase
{
    private readonly SosService _sosService;
    private readonly IMapper _mapper;

    public SosController(SosService sosService, IMapper mapper)
    {
        _sosService = sosService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<SosDetailResponse>> Create(
        [FromBody] CreateSosRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateSosCommand>(request);
        var sos = await _sosService.CreateAsync(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = sos.Id }, _mapper.Map<SosDetailResponse>(sos));
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<SosSummaryResponse>>> Search(
        [FromQuery] SosStatus? status,
        [FromQuery] DateTimeOffset? createdFrom,
        [FromQuery] DateTimeOffset? createdTo,
        [FromQuery] string? sortBy,
        [FromQuery] string? sortDirection,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchSosQuery
        {
            Status = status,
            CreatedFrom = createdFrom,
            CreatedTo = createdTo,
            SortBy = sortBy ?? "priority_score",
            SortDirection = sortDirection ?? "desc",
            Page = page,
            PageSize = pageSize
        };

        var result = await _sosService.SearchAsync(query, cancellationToken);

        return Ok(new PagedResponse<SosSummaryResponse>
        {
            Items = _mapper.Map<IReadOnlyList<SosSummaryResponse>>(result.Items),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalItems = result.TotalItems,
            TotalPages = result.TotalPages
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SosDetailResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var sos = await _sosService.GetDetailAsync(id, cancellationToken);

        if (sos == null)
            return NotFound();

        return Ok(_mapper.Map<SosDetailResponse>(sos));
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, [FromQuery] Guid citizenId, CancellationToken cancellationToken)
    {
        await _sosService.CancelAsync(new CancelSosCommand
        {
            SosRequestId = id,
            CitizenId = citizenId
        }, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        [FromQuery] SosStatus status,
        CancellationToken cancellationToken)
    {
        await _sosService.UpdateStatusAsync(new UpdateSosStatusCommand
        {
            SosRequestId = id,
            Status = status
        }, cancellationToken);

        return NoContent();
    }
}

