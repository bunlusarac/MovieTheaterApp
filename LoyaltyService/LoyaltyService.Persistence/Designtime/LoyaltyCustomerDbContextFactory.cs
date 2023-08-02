using LoyaltyService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LoyaltyService.Persistence.Designtime;

public class LoyaltyCustomerDbContextFactory: IDesignTimeDbContextFactory<LoyaltyCustomerDbContext>
{
    public LoyaltyCustomerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LoyaltyCustomerDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=9004;Database=loyaltydb;User Id=postgres;Password=loyalty123;");

        var context = new LoyaltyCustomerDbContext(optionsBuilder.Options);
        return context;
    }
}