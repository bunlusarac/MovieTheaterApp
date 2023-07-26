using Microsoft.EntityFrameworkCore;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Persistence.Contexts;

namespace VenueService.Persistence.Repositories;

public class VenueRepository: IVenueRepository
{
    private readonly VenueDbContext _context;

    public VenueRepository(VenueDbContext context)
    {
        _context = context;
    }

    public async Task<Venue> GetById(Guid id)
    {
        return await _context.Venues.FindAsync(id);
    }

    public async Task<IList<Venue>> GetAll()
    {
        return await _context.Venues.ToListAsync();
    }

    public async Task<Venue> Update(Venue entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows == 1 ? entity : null;
        //return null;
    }

    public async Task<Venue> Add(Venue entity)
    {
        await _context.Venues.AddAsync(entity);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows == 1 ? entity : null;
    }
}