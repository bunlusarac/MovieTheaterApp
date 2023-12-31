using LoyaltyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyService.Persistence.Contexts;

public class CampaignDbContext: DbContextBase<Campaign>
{
    public CampaignDbContext(DbContextOptions<CampaignDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>(campaign =>
        {
            campaign.ToTable("Campaign");
            
            campaign.HasKey(c => c.Id);

            campaign.Property(c => c.Name).IsRequired();
            campaign.Property(c => c.Description).IsRequired();
            campaign.Property(c => c.Type).IsRequired();
            campaign.Property(c => c.DiscountRate).IsRequired();
            campaign.Property(c => c.ExpirationDate).IsRequired();
            campaign.Property(c => c.MaxRedeems).IsRequired();

            campaign.Property(c => c.Version).IsRowVersion();
            campaign.Property(c => c.ConcurrencySecret).IsRequired();
            
            campaign.OwnsOne(c => c.Cost, cost =>
            {
                cost.Property(x => x.Amount).IsRequired();
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}