using MediatR;
using VenueService.Application.Persistence;

namespace VenueService.Application.Commands;

public class ReserveSessionSeatCommand: IRequest
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }

    public ReserveSessionSeatCommand(Guid venueId, Guid theaterId, Guid sessionId, char seatRow, int seatNumber)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        SessionId = sessionId;
        SeatRow = seatRow;
        SeatNumber = seatNumber;
    }
}

public class ReserveSessionSeatCommandHandler : IRequestHandler<ReserveSessionSeatCommand>
{
    private readonly IVenueRepository _venueRepository;

    public ReserveSessionSeatCommandHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task Handle(ReserveSessionSeatCommand request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new Exception();

        venue
            .Theaters.First(t => t.Id == request.TheaterId)
            .Sessions.First(s => s.Id == request.SessionId)
            .OccupySeat(request.SeatRow, request.SeatNumber);
        
        await _venueRepository.Update(venue);
    }
}