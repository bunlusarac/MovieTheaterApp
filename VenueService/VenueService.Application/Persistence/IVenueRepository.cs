using VenueService.Domain.Entities;

namespace VenueService.Application.Persistence;

public interface IVenueRepository: IRepositoryAsync<Venue>
{
    public Task<IQueryable<Venue>> GetAllQueryable();
}