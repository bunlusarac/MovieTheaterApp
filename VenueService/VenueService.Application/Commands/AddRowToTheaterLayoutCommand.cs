using MediatR;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Application.Commands;

public class AddRowToTheaterLayoutCommand: IRequest
{
    public Guid VenueId { get; set; }
    public Guid TheaterId { get; set; }
    public List<SeatType> RowSeats { get; set; }
    public int Times { get; set; }

    public AddRowToTheaterLayoutCommand(Guid venueId, Guid theaterId, List<SeatType> rowSeats, int times)
    {
        VenueId = venueId;
        TheaterId = theaterId;
        RowSeats = rowSeats;
        Times = times;
    }
}

public class AddRowToTheaterLayoutCommandHandler : IRequestHandler<AddRowToTheaterLayoutCommand>
{
    private readonly IVenueRepository _venueRepository;

    public AddRowToTheaterLayoutCommandHandler(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }
    
    public async Task Handle(AddRowToTheaterLayoutCommand request, CancellationToken cancellationToken)
    {
        var venue = await _venueRepository.GetById(request.VenueId);
        if (venue == null) throw new Exception();

        venue.Theaters.First(t => t.Id == request.TheaterId).Layout.AddRows(request.RowSeats, request.Times);
        await _venueRepository.Update(venue);
    }
}
