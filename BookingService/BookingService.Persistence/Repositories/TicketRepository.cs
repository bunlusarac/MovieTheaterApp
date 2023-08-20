using BookingService.Application.Persistence;
using BookingService.Domain.Entities;
using BookingService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Persistence.Repositories;

public class TicketRepository: ITicketRepository
{
    private readonly BookingDbContext _context;

    public TicketRepository(BookingDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Ticket>> GetAll()
    {
        return await _context.Tickets.ToListAsync();
    }

    public async Task<Ticket> GetById(Guid entityId)
    {
        var tx = await _context.Tickets.FindAsync(entityId);
        if (tx == null) throw new InvalidOperationException(); //TODO
        return tx;
    }

    public async Task Update(Ticket entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("", e); //TODO
        }
    }

    public async Task Add(Ticket entity)
    {
        try
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("", e); //TODO
        }
    }

    public async Task<List<Ticket>> GetByCustomerId(Guid customerId)
    {
        return await _context.Tickets.Where(t => t.CustomerId == customerId).ToListAsync();
    }
}