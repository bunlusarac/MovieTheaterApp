using MediatR;
using VenueService.Application.Persistence;

namespace VenueService.Application.Commands;

public class ReserveSessionSeatCommand: IRequest
{
    public Guid SessionId { get; set; }
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }

    public ReserveSessionSeatCommand(Guid sessionId, char seatRow, int seatNumber)
    {
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
        var queryable = await _venueRepository.GetAllQueryable();
            
        var query = 
            queryable
                .SelectMany(v => v.Theaters, (v, t) => new { Venue = v, Theater = t })
                .SelectMany(x => x.Theater.Sessions, (x, s) => new { x.Venue, Session = s })
                .Where(x => x.Session.Id == request.SessionId)
                .Select(x => x.Venue);

        var venue = query.ToList().First(); 
        if (venue == null) throw new Exception();
        
        var session = venue.Theaters.First(s => s.Id == request.SessionId).Sessions.First(s => s.Id == request.SessionId);
        session.OccupySeat(request.SeatRow, request.SeatNumber);

        await _venueRepository.Update(venue);
    }
}