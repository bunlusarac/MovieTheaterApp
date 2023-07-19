using Microsoft.EntityFrameworkCore;
using OTPService.Application.Common;
using OTPService.Application.Persistence;
using OTPService.Domain.Entities;
using OTPService.Persistence.Contexts;

namespace OTPService.Persistence.Repositories;

public class OtpUserRepository: IOtpUserRepository
{
    private readonly OtpDbContext _context;

    public OtpUserRepository(OtpDbContext context)
    {
        _context = context;
    }

    public async Task<OtpUser>? GetById(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IList<OtpUser>>? GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<Result> Add(OtpUser entity)
    {
        await _context.Users.AddAsync(entity);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows == 1 ? Result.Ok : Result.Error;
    }

    public async Task<Result> Update(OtpUser entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows == 1 ? Result.Ok : Result.Error;
    }
    
    public async Task<OtpUser>? GetByIssuedUserId(Guid issuedUserId)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.IssuedUserId == issuedUserId);
    }
}