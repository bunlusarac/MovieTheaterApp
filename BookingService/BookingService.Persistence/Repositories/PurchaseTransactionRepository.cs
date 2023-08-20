using System.Runtime.InteropServices.ComTypes;
using BookingService.Application.Persistence;
using BookingService.Domain.Common;
using BookingService.Domain.Entities;
using BookingService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Persistence.Repositories;

public class PurchaseTransactionRepository: IPurchaseTransactionRepository
{
    private readonly BookingDbContext _context;

    public PurchaseTransactionRepository(BookingDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<PurchaseTransaction>> GetAll()
    {
        return await _context.PurchaseTransactions.ToListAsync();
    }

    public async Task<PurchaseTransaction> GetById(Guid entityId)
    {
        var tx = await _context.PurchaseTransactions.FindAsync(entityId);
        if (tx == null) throw new InvalidOperationException(); //TODO
        return tx;
    }

    public async Task Update(PurchaseTransaction entity)
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

    public async Task Add(PurchaseTransaction entity)
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
}