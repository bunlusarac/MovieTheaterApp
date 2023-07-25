using MediatR;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;

namespace VenueService.Application.Commands;

public class ReleaseSessionSeatCommand: IRequest
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }

    public ReleaseSessionSeatCommand(Guid venueId, Guid theaterId, Guid sessionId, char seatRow, int seatNumber)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        SessionId = sessionId;
        SeatRow = seatRow;
        SeatNumber = seatNumber;
    }
}

public class ReleaseSessionSeatCommandHandler : IRequestHandler<ReleaseSessionSeatCommand>
{
    private readonly IVenueRepository _venueRepository;

    public ReleaseSessionSeatCommandHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task Handle(ReleaseSessionSeatCommand request, CancellationToken cancellationToken)
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