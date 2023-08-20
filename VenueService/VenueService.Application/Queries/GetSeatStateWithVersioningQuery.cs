using MediatR;
using VenueService.Application.DTOs;
using VenueService.Application.Exceptions;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Application.Queries;

public class GetSeatStateWithVersioningQuery: IRequest<SeatingStateWithVersioningDto>
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public Guid SessionId { get; set; }
    public char SeatRow { get; set; }
    public int SeatNumber { get; set; }

    public GetSeatStateWithVersioningQuery(Guid venueId, Guid theaterId, Guid sessionId, char seatRow, int seatNumber)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        SessionId = sessionId;
        SeatRow = seatRow;
        SeatNumber = seatNumber;
    }
}

public class GetSeatStateWithVersioningQueryHandler : IRequestHandler<GetSeatStateWithVersioningQuery, SeatingStateWithVersioningDto>
{
    private readonly IVenueRepository _venueRepository;

    public GetSeatStateWithVersioningQueryHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    public async Task<SeatingStateWithVersioningDto> Handle(GetSeatStateWithVersioningQuery request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new VenueApplicationException(VenueApplicationErrorCode.VenueDoesNotExist);

        var theater = venue.Theaters.FirstOrDefault(s => s.Id == request.TheaterId);
        if (theater == null) throw new VenueApplicationException(VenueApplicationErrorCode.TheaterDoesNotExist);
        
        var session = theater.Sessions.FirstOrDefault(s => s.Id == request.SessionId);
        if (session == null) throw new VenueApplicationException(VenueApplicationErrorCode.SessionDoesNotExist);

        var seatState =
            session.SeatingState.StateSeats.FirstOrDefault(s =>
                s.SeatNumber == request.SeatNumber && s.Row == request.SeatRow);

        if (seatState == null) throw new VenueApplicationException(VenueApplicationErrorCode.SeatDoesNotExist);
        
        return new SeatingStateWithVersioningDto(seatState.Row,
            seatState.Occupied,
            seatState.Type,
            seatState.SeatNumber,
            ConcurrencyTokenHelper.GenerateConcurrencyToken(seatState.Version, seatState.ConcurrencySecret));
    }
}