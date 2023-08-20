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
    [HttpGet("{venueId:guid}/session")]
    //[Route("{venueId}/session")]
    public async Task<IList<VenueSessionDto>> GetAllSessionsOfVenue(Guid venueId)
    {
        return await _mediator.Send(new GetVenueSessionsQuery(venueId));
    }
    
    /// <summary>
    /// Get a session
    /// </summary>
    /// <param name="venueId">ID of venue session belongs to</param>
    /// <param name="theaterId">ID of theater session belongs to</param>
    /// <param name="sessionId">ID of the session</param>
    /// <returns></returns>
    [HttpGet("{venueId:guid}/theater/{theaterId:guid}/session/{sessionId:guid}")]
    //[Route("{venueId}/session")]
    public async Task<VenueSessionDto> GetSession(Guid venueId, Guid theaterId, Guid sessionId)
    {
        return await _mediator.Send(new GetSessionQuery(venueId, theaterId, sessionId));
    }
    
    /// <summary>
    /// Get all theaters of a given venue
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <returns>List of theaters</returns>
    [HttpGet("{venueId:guid}/theater")]
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
    [HttpGet("{venueId:guid}/theater/{theaterId:guid}/session/{sessionId:guid}/seating")]
    public async Task<List<SeatingStateDto>> GetSessionSeating(Guid venueId, Guid theaterId, Guid sessionId)
    {
        return await _mediator.Send(new GetSessionSeatingStateQuery(venueId, theaterId, sessionId));
    }

    /// <summary>
    /// Get seating information of a session with versioning.
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <param name="theaterId">ID of the theater</param>
    /// <param name="sessionId">ID of the session</param>
    /// <param name="seating">Object representing position of a specific seat</param>
    /// <param name="seatRow">Seat row</param>
    /// <param name="seatNumber">Seat number</param>
    /// <returns>A list of seat states representing given session's seating status</returns>
    [HttpGet("{venueId:guid}/theater/{theaterId:guid}/session/{sessionId:guid}/seat/{seatRow}/{seatNumber:int}")]
    public async Task<SeatingStateWithVersioningDto> GetSeatStateWithVersioning(Guid venueId, Guid theaterId, Guid sessionId, char seatRow, int seatNumber)
    {
        return await _mediator.Send(new GetSeatStateWithVersioningQuery(venueId,
            theaterId,
            sessionId,
            seatRow,
            seatNumber));
    }
    
    /// <summary>
    /// Get seating information of a session with versioning.
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <param name="theaterId">ID of the theater</param>
    /// <param name="sessionId">ID of the session</param>
    /// <returns>A list of seat states representing given session's seating status</returns>
    [HttpGet("{venueId:guid}/theater/{theaterId:guid}/session/{sessionId:guid}/seating-with-versioning")]
    public async Task<List<SeatingStateWithVersioningDto>> GetSessionSeatingWithVersioning(Guid venueId, Guid theaterId, Guid sessionId)
    {
        return await _mediator.Send(new GetSessionSeatingStateWithVersioningQuery(venueId, theaterId, sessionId));
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
    [HttpPut("{venueId:guid}/theater")]
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
    [HttpPut("{venueId:guid}/theater/{theaterId:guid}/session")]
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
    [HttpPut("{venueId:guid}/theater/{theaterId:guid}/layout")]
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
    /// <param name="releaseSeat">Object representing position of a specific seat</param>
    /// <returns>Information regarding to the success of reservation</returns>
    [HttpPost("{venueId:guid}/theater/{theaterId:guid}/session/{sessionId:guid}/reserve")]
    public async Task<IActionResult> ReserveSeat(
        Guid venueId,
        Guid theaterId,
        Guid sessionId, 
        [FromBody] ReserveSeatDto releaseSeat)
    {
        await _mediator.Send(new ReserveSessionSeatCommand(venueId, theaterId, sessionId, releaseSeat.SeatRow,
                releaseSeat.SeatNumber, releaseSeat.Version));
        return Ok();
    }

    /// <summary>
    /// Release a seat for given session of given theater and venue
    /// </summary>
    /// <param name="venueId">ID of the venue</param>
    /// <param name="theaterId">ID of the theater</param>
    /// <param name="sessionId">ID of the session</param>
    /// <param name="releaseSeat">Object representing position of a specific seat</param>
    /// <returns>Information regarding to the success of releasing</returns>
    [HttpPost("{venueId:guid}/theater/{theaterId:guid}/session/{sessionId:guid}/release")]
    public async Task<IActionResult> ReleaseSeat(Guid venueId, Guid theaterId, Guid sessionId, [FromBody] ReleaseSeatDto releaseSeat)
    {
        await _mediator.Send(new ReleaseSessionSeatCommand(venueId, theaterId, sessionId, releaseSeat.SeatRow, releaseSeat.SeatNumber)); 
        return Ok();    
    }

   /// <summary>
   /// Delete a session
   /// </summary>
   /// <param name="venueId">ID of the venue</param>
   /// <param name="theaterId">ID of the theater</param>
   /// <param name="sessionId">ID of the session</param>
   /// <returns>Information regarding to the success of deletion</returns>
   [HttpDelete("{venueId:guid}/theater/{theaterId:guid}/session/{sessionId:guid}")]
   public async Task<IActionResult> DeleteSession(Guid venueId, Guid theaterId, Guid sessionId)
   { 
       await _mediator.Send(new DeleteSessionCommand(venueId, theaterId, sessionId));
       return Ok();
   }
}