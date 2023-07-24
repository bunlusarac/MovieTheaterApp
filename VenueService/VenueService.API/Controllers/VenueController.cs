using System.Collections;
using Microsoft.AspNetCore.Mvc;
using VenueService.API.DTOs;
using VenueService.Application.DTOs;

namespace VenueService.API.Controllers;

[ApiController]
[Route("venue")]
public class VenueController : ControllerBase
{
    [HttpGet]
    public IEnumerable<VenueDto> Get()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("{venueId}/session")]
    public IEnumerable<VenueSessionDto> GetSessions(Guid venueId)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("/session/{sessionId}/seating")]
    public SeatingStateDto GetSessionSeating(Guid sessionId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Route("/session/{sessionId}/seating/reserve")]
    public IActionResult ReserveSeat(Guid sessionId, [FromBody] SeatingDto dto)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Route("/session/{sessionId}/seating/release")]
    public IActionResult ReleaseSeat(Guid sessionId, [FromBody] SeatingDto dto)
    {
        throw new NotImplementedException();
    }
}