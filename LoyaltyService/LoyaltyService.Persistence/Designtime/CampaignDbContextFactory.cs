using LoyaltyService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LoyaltyService.Persistence.Designtime;

public class CampaignDbContextFactory: IDesignTimeDbContextFactory<CampaignDbContext>
{
    public CampaignDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CampaignDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=9004;Database=loyaltydb;User Id=postgres;Password=loyalty123;");

        var context = new CampaignDbContext(optionsBuilder.Options);
        return context;
    }
}