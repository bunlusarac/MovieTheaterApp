using System.Data;
using System.Runtime.CompilerServices;
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
        var all = await _context.DataSet.ToListAsync();
        return all;
    }

    public async Task<T> GetById(Guid entityId)
    {
        var entity = await _context.DataSet.FindAsync(entityId);

        if (entity == null)
        {
            throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.NotFound);
        }
        
        return entity;
    }
    
    public async Task<List<T>> GetAllWithLockWait()
    {
        using (var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable))
        {
            var tableName = GetType().Name.Replace("Repository", "");
            var sql = $"""SELECT *, "xmin"  FROM "{tableName}" FOR UPDATE""";
            var entities = await _context.DataSet.FromSqlRaw(sql).ToListAsync();
            
            await tx.CommitAsync();
            return entities;
        }
    }

    public async Task<T> GetByIdWithLockWait(Guid entityId)
    {
        using (var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable))
        {
            var tableName = GetType().Name.Replace("Repository", "");
            var sql = $"""SELECT *, "xmin"  FROM "{tableName}" WHERE "Id" = '{entityId}' FOR UPDATE""";
            var entity = await _context.DataSet.FromSqlRaw(sql).SingleOrDefaultAsync();

            if (entity is null)
            {
                await tx.RollbackAsync();
                throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.NotFound);
            }
            
            await tx.CommitAsync();
            return entity;
        }
    }
    
    public async Task<T> GetByIdAndLock(Guid entityId)
    {
        //Only use in transactions and commit afterwards!!!
        var tableName = GetType().Name.Replace("Repository", "");
        var sql = $"""SELECT *, "xmin"  FROM "{tableName}" WHERE "Id" = '{entityId}' FOR UPDATE""";
        var entity = await _context.DataSet.FromSqlRaw(sql).SingleOrDefaultAsync();
        
        if (entity == null)
        {
            throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.NotFound);
        }
        
        return entity;
    }
    
    public async Task Update(T entity)
    {
        using (var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable))
        {
            //GUID already sanitized at API layer
            var tableName = GetType().Name.Replace("Repository", "");
            var sql = $"""SELECT *, "xmin"  FROM "{tableName}" WHERE "Id" = '{entity.Id}' FOR UPDATE""";
            var existingEntity = await _context.DataSet.FromSqlRaw(sql).SingleOrDefaultAsync();

            if (existingEntity == null)
                throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.NotFound);
            
            try
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                
                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch (Exception e)
            {
                await tx.RollbackAsync();
                throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.UpdateFailed, e);
            }
        }
        

        /*
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.UpdateFailed, e);
        }*/
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
        using (var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable))
        {
            try
            {
                //var entity = await _context.DataSet.FindAsync(entityId);
                //Obtain entity with row locking.
                var tableName = GetType().Name.Replace("Repository", "");
                var sql = $"""SELECT *, "xmin"  FROM "{tableName}" WHERE "Id" = '{entityId}' FOR UPDATE""";
                //var entity = await _context.DataSet.FromSql(FormattableStringFactory.Create(sql, entityId)).SingleOrDefaultAsync();
                var entity = await _context.DataSet.FromSqlRaw(sql).SingleOrDefaultAsync();
            
                if (entity == null)
                    throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.NotFound);
                
                _context.DataSet.Remove(entity);
                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch (Exception e)
            {
                await tx.RollbackAsync();
                throw new LoyaltyPersistenceException(LoyaltyPersistenceErrorCode.DeleteFailed, e);
            }
        }
    }
}