using System.Data;
using Microsoft.EntityFrameworkCore;
using VenueService.Application.Exceptions;
using VenueService.Application.Persistence;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;
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
        try
        {   
            //_context.Venues.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            //_context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<int> UpdateSeatWithVersioning(StateSeat seat, int expectedVersion)
    {
        _context.Entry(seat).OriginalValues["Version"] = expectedVersion;
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows == 1 ? expectedVersion : -1;
    }
    
    public async Task UpdateSeatWithConcurrencyToken(StateSeat seat, string concurrencyToken)
    {
        if (!ConcurrencyTokenHelper.ValidateConcurrencyToken(seat.Version, seat.ConcurrencySecret, concurrencyToken))
            throw new VenueApplicationException(VenueApplicationErrorCode.SeatVersionOutdated);
        
        var affectedRows = await _context.SaveChangesAsync();
    }

    public async Task<Venue> Add(Venue entity)
    {
        await _context.Venues.AddAsync(entity);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows == 1 ? entity : null;
    }
}