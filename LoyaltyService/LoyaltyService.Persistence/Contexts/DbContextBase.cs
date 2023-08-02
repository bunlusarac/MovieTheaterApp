using LoyaltyService.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyService.Persistence.Contexts;

public class DbContextBase<T>: DbContext where T: AggregateRoot
{
    public DbSet<T> DataSet { get; set; }

    public DbContextBase(DbContextOptions options) : base(options)
    {
    }
}