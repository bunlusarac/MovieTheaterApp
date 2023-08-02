using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.Common;
using LoyaltyService.Domain.Exceptions;
using LoyaltyService.Persistence.Contexts;
using LoyaltyService.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyService.Persistence.Repositories;

public class RepositoryAsync<T>: IRepositoryAsync<T> where T: AggregateRoot 
{
    protected readonly DbContextBase<T> _context;

    protected RepositoryAsync(DbContextBase<T> context)
    {
        _context = context;
    }
    
    public async Task<List<T>> GetAll()
    {
        return await _context.DataSet.ToListAsync();
    }

    public async Task<T> GetById(Guid entityId)
    {
        var entity = await _context.DataSet.FindAsync(entityId);

        if (entity == null)
            throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.NotFound);

        return entity;
    }

    public async Task Update(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.UpdateFailed, e);
        }
    }

    public async Task Add(T entity)
    {
        try
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.UpdateFailed, e);
        }
    }

    public async Task DeleteById(Guid entityId)
    {   
        var entity = await _context.DataSet.FindAsync(entityId);

        if (entity == null)
            throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.NotFound);

        try
        {
            _context.DataSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.NotFound, e);
        }
    }
}