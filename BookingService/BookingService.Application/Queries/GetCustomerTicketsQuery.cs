using BookingService.Application.DTOs;
using BookingService.Application.Persistence;
using BookingService.Domain.Entities;
using MediatR;

namespace BookingService.Application.Queries;

public class GetCustomerTicketsQuery: IRequest<List<Ticket>>
{
    public Guid CustomerId { get; set; }

    public GetCustomerTicketsQuery(Guid customerId)
    {
        CustomerId = customerId;
    }
}

public class GetCustomerTicketsQueryHandler : IRequestHandler<GetCustomerTicketsQuery, List<Ticket>>
{
    private readonly ITicketRepository _ticketRepository;

    public GetCustomerTicketsQueryHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<List<Ticket>> Handle(GetCustomerTicketsQuery request, CancellationToken cancellationToken)
    {
        return await _ticketRepository.GetByCustomerId(request.CustomerId);
    }
}