using BookingService.API.DTOs;
using BookingService.Application.Commands;
using BookingService.Application.DTOs;
using BookingService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.API.Controllers;

[ApiController]
[Route("ticket")]
public class TicketController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{customerId}")]
    public async Task<List<CustomerTicketDto>> GetTicketsByCustomerId(Guid customerId)
    {
        var cmd = new GetCustomerTicketsQuery(customerId);
        var tickets = await _mediator.Send(cmd);
        return tickets.Select(t => new CustomerTicketDto
        {
            SeatRow = t.Seat.Row,
            SeatNumber = t.Seat.Number,
            SessionId = t.SessionId,
            Type = t.Type,
            TicketId = t.Id
        }).ToList();
    }
    
    [HttpPut("purchase")]
    public async Task<TicketPurchasedDto> PurchaseTicket([FromBody] PurchaseTicketDto dto)
    {
        var cmd = new PurchaseTicketCommand
        {
            Bearer = HttpContext.Request.Headers.Authorization.ToString(),
            CampaignConcurrencyToken = dto.CampaignConcurrencyToken,
            CampaignId = dto.CampaignId,
            SeatConcurrencyToken = dto.SeatConcurrencyToken,
            SeatNumber = dto.SeatNumber,
            SeatRow = dto.SeatRow,
            SessionId = dto.SessionId,
            TheaterId = dto.TheaterId,
            TicketType = dto.TicketType,
            VenueId = dto.VenueId
        };

        var ticketPurchasedDto = await _mediator.Send(cmd);
        return ticketPurchasedDto;
    }
}