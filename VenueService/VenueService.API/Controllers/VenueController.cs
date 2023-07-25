using System.Collections;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VenueService.API.DTOs;
using VenueService.Application.Commands;
using VenueService.Application.DTOs;
using VenueService.Application.Queries;
using VenueService.Domain.ValueObjects;

namespace VenueService.API.Controllers;

[ApiController]
[Route("venue")]
public class VenueController : ControllerBase
{
    private readonly IMediator _mediator;

    public VenueController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IList<VenueDto>> GetVenues()
    {
        return await _mediator.Send(new GetVenuesQuery());
    }
    
    [HttpPut]
    public async Task<IActionResult> CreateVenue([FromBody] CreateVenueDto dto)
    {
        try
        {
            await _mediator.Send(new CreateVenueCommand(dto.Name, dto.Location));
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPut]
    [Route("{venueId}/theater")]
    public async Task<IActionResult> CreateTheater(Guid venueId, [FromBody] CreateTheaterDto dto)
    {
        try
        {
            await _mediator.Send(new CreateTheaterCommand(venueId, dto.Name, dto.Type, dto.Width));
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpGet]
    [Route("{venueId}/session")]
    public async Task<IList<VenueSessionDto>> GetAllSessionsOfVenue(Guid venueId)
    {
        return await _mediator.Send(new GetVenueSessionsQuery(venueId));
    }
    
    [HttpGet]
    [Route("{venueId}/theater/{theaterId}/session")]
    public async Task<IList<TheaterSessionDto>> GetAllSessionsOfTheater(Guid venueId, Guid theaterId)
    {
        return await _mediator.Send(new GetTheaterSessionsQuery(venueId, theaterId));
    }
    
    [HttpPut]
    [Route("{venueId}/theater/{theaterId}/session")]
    public async Task<IActionResult> CreateSession(Guid venueId, Guid theaterId, [FromBody] CreateSessionDto dto)
    {
        try
        {
            await _mediator.Send(new CreateSessionCommand(
                venueId, 
                theaterId, 
                dto.MovieId,
                new TimeRange(dto.StartTime, dto.EndTime),
                dto.Localization,
                dto.Pricings));
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpDelete]
    [Route("{venueId}/theater/{theaterId}/session/{sessionId}")]
    public async Task<IActionResult> DeleteSession(Guid venueId, Guid theaterId, Guid sessionId)
    {
        try
        {
            await _mediator.Send(new DeleteSessionCommand(venueId, theaterId,
                sessionId));
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }

    
    [HttpGet]
    [Route("{venueId}/theater/{theaterId}/session/{sessionId}")]
    public async Task<SeatingStateDto> GetSessionSeating(Guid venueId, Guid theaterId, Guid sessionId)
    {
        return await _mediator.Send(new GetSessionSeatingStateQuery(venueId, theaterId, sessionId));
    }
    
    [HttpPost]
    [Route("{venueId}/theater/{theaterId}/session/{sessionId}/reserve")]
    public async Task<IActionResult> ReserveSeat(
        Guid venueId,
        Guid theaterId,
        Guid sessionId, 
        [FromBody] SeatingDto dto)
    {
        try
        {
            await _mediator.Send(new ReserveSessionSeatCommand(venueId, theaterId, sessionId, dto.SeatRow,
                dto.SeatNumber));
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpPost]
    [Route("{venueId}/theater/{theaterId}/session/{sessionId}/release")]
    public async Task<IActionResult> ReleaseSeat(Guid venueId, Guid theaterId, Guid sessionId, [FromBody] SeatingDto dto)
    {
        try
        {
            await _mediator.Send(new ReleaseSessionSeatCommand(venueId, theaterId, sessionId, dto.SeatRow, dto.SeatNumber));
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();    
    }

    [HttpPut]
    [Route("{venueId}/theater/{theaterId}/layout")]
    public async Task<IActionResult> AddRowToTheaterLayout(Guid venueId, Guid theaterId, [FromBody] LayoutRowDto dto)
    {
        try
        {
            await _mediator.Send(new AddRowToTheaterLayoutCommand(venueId, theaterId, dto.RowSeats, dto.Times));
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
}