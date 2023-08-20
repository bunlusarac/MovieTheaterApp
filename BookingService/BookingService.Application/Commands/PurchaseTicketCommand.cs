
using BookingService.Application.Communicators;
using BookingService.Application.DTOs;

using BookingService.Application.Persistence;

using BookingService.Domain.Entities;
using BookingService.Domain.Utils;
using BookingService.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.Commands;

public class PurchaseTicketCommand: IRequest<TicketPurchasedDto>
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }
    
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }
    public string SeatConcurrencyToken { set; get; }
    
    public TicketType TicketType { get; set; }

    //public List<CampaignTokenPairDto> Campaigns { get; set; }
    
    public Guid CampaignId { get; set; }
    public string CampaignConcurrencyToken { get; set; }
    
    public string Bearer { get; set; }
}

public class PurchaseTicketCommandHandler : IRequestHandler<PurchaseTicketCommand, TicketPurchasedDto>
{
    private readonly ILoyaltyServiceCommunicator _loyaltyServiceCommunicator;
    private readonly IVenueServiceCommunicator _venueServiceCommunicator;
    private readonly IMovieServiceCommunicator _movieServiceCommunicator;
    private readonly IIdentityServiceCommunicator _identityServiceCommunicator;
    private readonly IRabbitCommunicator _rabbitCommunicator;
    private readonly ITicketRepository _ticketRepository;
    private readonly ILogger<PurchaseTicketCommandHandler> _logger;

    public PurchaseTicketCommandHandler(ILoyaltyServiceCommunicator loyaltyServiceCommunicator, IVenueServiceCommunicator venueServiceCommunicator, IMovieServiceCommunicator movieServiceCommunicator, IIdentityServiceCommunicator identityServiceCommunicator, IRabbitCommunicator rabbitCommunicator, ITicketRepository ticketRepository, ILogger<PurchaseTicketCommandHandler> logger)
    {
        _loyaltyServiceCommunicator = loyaltyServiceCommunicator;
        _venueServiceCommunicator = venueServiceCommunicator;
        _movieServiceCommunicator = movieServiceCommunicator;
        _identityServiceCommunicator = identityServiceCommunicator;
        _rabbitCommunicator = rabbitCommunicator;
        _ticketRepository = ticketRepository;
        _logger = logger;
    }

    public async Task<TicketPurchasedDto> Handle(PurchaseTicketCommand request, CancellationToken cancellationToken)
    {
       
        var session =
            await _venueServiceCommunicator.SendGetSessionRequest(request.VenueId, request.TheaterId,
                request.SessionId);
        var movie = await _movieServiceCommunicator.SendGetMovieRequest(session.MovieId);
        var userInfo = await _identityServiceCommunicator.SendGetUserInfoRequest(request.Bearer);
        
        //Get campaign
        var campaign = await _loyaltyServiceCommunicator.SendGetCampaignRequest(request.CampaignId);

        // Age - RTUK SmartSign validation
        var age = DateTime.UtcNow.Year - userInfo.DateOfBirth.Year; 
        
        if (movie.SmartSigns.Contains(SmartSign.ThirteenOver) && age < 13)
            throw new InvalidOperationException(); //TODO

        if (movie.SmartSigns.Contains(SmartSign.EighteenOver) && age < 18)
            throw new InvalidOperationException(); //TODO 

        // Available session ticket type validation
        var availableTicketTypes = session.Pricings.Select(dto => dto.Type); 
        
        if (!availableTicketTypes.Contains(request.TicketType))
            throw new InvalidOperationException(); //TODO

        // Student status validation
        var isStudent = userInfo.StudentExpiration > DateTime.UtcNow;

        if (request.TicketType is TicketType.Student or TicketType.SweetboxStudent && !isStudent)
            throw new InvalidOperationException(); //TODO

        // Session capacity validation
        if (session.Capacity == 0)
            throw new InvalidOperationException(); //TODO
        
        // Processing saga (HTTP)
        await _venueServiceCommunicator.SendReserveSeatRequest(request.VenueId, request.TheaterId, request.SessionId,
            request.SeatNumber, request.SeatRow, request.SeatConcurrencyToken);

        Guid redeemId;

        try
        {
            var redeemDto = await _loyaltyServiceCommunicator.SendRedeemCampaignRequest(request.CampaignId,
                userInfo.SubjectId, request.CampaignConcurrencyToken);

            redeemId = redeemDto.RedeemId;
        }
        catch (Exception e)
        {
            //Rollback
            await _venueServiceCommunicator.SendRefundSeatRequest(request.VenueId, request.TheaterId, request.SessionId,
                request.SeatNumber, request.SeatRow/*, request.SeatConcurrencyToken*/);
            throw new InvalidOperationException("", e); //TODO
        }
        
        // Pricing
        var price = session.Pricings.First(dto => dto.Type == request.TicketType).Price.Amount;
        var finalPrice = price * (1m - (decimal) campaign.DiscountRate); 
        _logger.Log(LogLevel.Information, "The final price to be paid is {}", finalPrice);
        
        //Persist ticket
        var ticket = new Ticket(userInfo.SubjectId, request.SessionId, new Seat(request.SeatRow, request.SeatNumber),
            request.TicketType);

        try
        {
            await _ticketRepository.Add(ticket);
        }
        catch(Exception e)
        {
            //Rollback (venue+loyalty)
            await _venueServiceCommunicator.SendRefundSeatRequest(request.VenueId, request.TheaterId, request.SessionId,
                request.SeatNumber, request.SeatRow/*, request.SeatConcurrencyToken*/);
            await _loyaltyServiceCommunicator.SendRefundCampaignRequest(redeemId,
                userInfo.SubjectId);
            throw new InvalidOperationException(); //TODO
        }
        
        return new TicketPurchasedDto
        {
            TicketId = ticket.Id,
        };
    }
}