using BookingService.Application.DTOs;

namespace BookingService.Application.Communicators;

public interface IVenueServiceCommunicator: ICommunicatorBase
{
    public Task SendReserveSeatRequest(
        Guid venueId,
        Guid theaterId,
        Guid sessionId,
        int seatNumber,
        char seatRow,
        string seatConcurrencyToken);
    
    public Task SendRefundSeatRequest(
        Guid venueId,
        Guid theaterId,
        Guid sessionId,
        int seatNumber,
        char seatRow
        /*string seatConcurrencyToken*/);
    
    public Task<SessionDto> SendGetSessionRequest(
        Guid venueId,
        Guid theaterId,
        Guid sessionId);
    
}