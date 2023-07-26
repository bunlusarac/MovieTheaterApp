using System.Collections;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VenueService.API.DTOs;
using VenueService.Application.Commands;
using VenueService.Application.DTOs;
using VenueService.Application.Queries;
using VenueService.Domain.ValueObjects;

namespace VenueService.API.Controllers;

/// <summary>
/// Controller for managing venues, their theaters and sessions.
/// </summary>
[ApiController]
[Route("venue")]
public class VenueController : ControllerBase
{
    private readonly IMediator _mediator;

    public VenueController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all venues
    /// </summary>
    /// <returns>List of all venues</returns>
    [HttpGet("")]
    public async Task<IList<VenueDto>> GetVenues()
    {
        return await _mediator.Send(new GetVenuesQuery());
    }
    
    /// <summary>
    /// Get all sessions of a given venue
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <returns>List of sessions</returns>
    [HttpGet("{venueId}/session")]
    //[Route("{venueId}/session")]
    public async Task<IList<VenueSessionDto>> GetAllSessionsOfVenue(Guid venueId)
    {
        return await _mediator.Send(new GetVenueSessionsQuery(venueId));
    }
    
    /// <summary>
    /// Get all theaters of a given venue
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <returns>List of theaters</returns>
    [HttpGet("{venueId}/theater")]
    public async Task<IList<VenueTheaterDto>> GetAllTheatersOfVenue(Guid venueId)
    {
        return await _mediator.Send(new GetVenueTheatersQuery(venueId));
    }
    
    /// <summary>
    /// Get seating information of a session
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <param name="theaterId">ID of the theater</param>
    /// <param name="sessionId">ID of the session</param>
    /// <returns>A list of seat states representing given session's seating status</returns>
    [HttpGet("{venueId}/theater/{theaterId}/session/{sessionId}")]
    public async Task<List<SeatingStateDto>> GetSessionSeating(Guid venueId, Guid theaterId, Guid sessionId)
    {
        return await _mediator.Send(new GetSessionSeatingStateQuery(venueId, theaterId, sessionId));
    }
        
    /// <summary>
    /// Create a venue
    /// </summary>
    /// <param name="venue">Object representing venue to be created</param>
    /// <returns>ID of created venue, if successful. Otherwise, an error message</returns>
    [HttpPut("")]
    public async Task<VenueCreatedDto> CreateVenue([FromBody] CreateVenueDto venue)
    {
        return await _mediator.Send(new CreateVenueCommand(venue.Name, venue.Location));
    }

    /// <summary>
    /// Create a theater for a given venue
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <param name="theater">Object representing theater to be created</param>
    /// <returns>ID of created theater, if successful. Otherwise, an error message</returns>
    [HttpPut("{venueId}/theater")]
    public async Task<TheaterCreatedDto> CreateTheater(Guid venueId, [FromBody] CreateTheaterDto theater)
    {
        return await _mediator.Send(new CreateTheaterCommand(venueId, theater.Name, theater.Type, theater.Width));
    }
    
    /// <summary>
    /// Create a session for a given venue and theater
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <param name="theaterId">ID of the theater</param>
    /// <param name="session">Object representing session to be created</param>
    /// <returns>ID of created session, if successful. Otherwise, an error message</returns>
    [HttpPut("{venueId}/theater/{theaterId}/session")]
    public async Task<SessionCreatedDto> CreateSession(Guid venueId, Guid theaterId, [FromBody] CreateSessionDto session)
    {
        return await _mediator.Send(new CreateSessionCommand(
                venueId, 
                theaterId, 
                session.MovieId,
                new TimeRange(session.StartTime, session.EndTime),
                session.Localization,
                session.Pricings)
        );
    }
    
    /// <summary>
    /// Add layout row(s) to given theater's layout
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <param name="theaterId">ID of the theater</param>
    /// <param name="layoutRow">Object representing new layout row that will be added to the theater's layout</param>
    /// <returns></returns>
    [HttpPut("{venueId}/theater/{theaterId}/layout")]
    public async Task<IActionResult> AddRowToTheaterLayout(Guid venueId, Guid theaterId, [FromBody] LayoutRowDto layoutRow)
    {
        await _mediator.Send(new AddRowToTheaterLayoutCommand(venueId, theaterId, layoutRow.RowSeats, layoutRow.Times));
        return Ok();
    }  
    
    /// <summary>
    /// Reserve a seat for given session of given theater and venue
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <param name="theaterId">ID of the theater</param>
    /// <param name="sessionId">ID of the session</param>
    /// <param name="seating">Object representing position of a specific seat</param>
    /// <returns>Information regarding to the success of reservation</returns>
    [HttpPost("{venueId}/theater/{theaterId}/session/{sessionId}/reserve")]
    public async Task<IActionResult> ReserveSeat(
        Guid venueId,
        Guid theaterId,
        Guid sessionId, 
        [FromBody] SeatingDto seating)
    {
        await _mediator.Send(new ReserveSessionSeatCommand(venueId, theaterId, sessionId, seating.SeatRow,
                seating.SeatNumber));
        return Ok();
    }
    
    /// <summary>
    /// Release a seat for given session of given theater and venue
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <param name="theaterId">ID of the theater</param>
    /// <param name="sessionId">ID of the session</param>
    /// <param name="seating">Object representing position of a specific seat</param>
    /// <returns>Information regarding to the success of releasing</returns>
    [HttpPost("{venueId}/theater/{theaterId}/session/{sessionId}/release")]
    public async Task<IActionResult> ReleaseSeat(Guid venueId, Guid theaterId, Guid sessionId, [FromBody] SeatingDto seating)
    {
        await _mediator.Send(new ReleaseSessionSeatCommand(venueId, theaterId, sessionId, seating.SeatRow, seating.SeatNumber)); 
        return Ok();    
    }

   /// <summary>
   /// Delete a session
   /// </summary>
   /// <param name="venueId">ID of the venue</param>
   /// <param name="theaterId">ID of the theater</param>
   /// <param name="sessionId">ID of the session</param>
   /// <returns>Information regarding to the success of deletion</returns>
   [HttpDelete("{venueId}/theater/{theaterId}/session/{sessionId}")]
   public async Task<IActionResult> DeleteSession(Guid venueId, Guid theaterId, Guid sessionId)
   { 
       await _mediator.Send(new DeleteSessionCommand(venueId, theaterId, sessionId));
       return Ok();
   }
}