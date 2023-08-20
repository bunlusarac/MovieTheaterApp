using BookingService.Domain.Entities;

namespace BookingService.Application.Persistence;

public interface ITicketRepository: IRepositoryAsync<Ticket>
{
    public Task<List<Ticket>> GetByCustomerId(Guid customerId);
}