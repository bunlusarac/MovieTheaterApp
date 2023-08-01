using LoyaltyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyService.Persistence.Contexts;

public class CampaignDbContext: DbContext
{
    public DbSet<Campaign> Campaigns { get; set; }

    public CampaignDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>(campaign =>
        {
            campaign.ToTable("Campaigns");
            
            campaign.HasKey(c => c.Id);

            campaign.Property(c => c.Name).IsRequired();
            campaign.Property(c => c.Description).IsRequired();
            campaign.Property(c => c.Type).IsRequired();
            campaign.Property(c => c.DiscountRate).IsRequired();
            campaign.Property(c => c.ExpirationDate).IsRequired();
            campaign.Property(c => c.MaxRedeems).IsRequired();

            campaign.OwnsOne(c => c.Cost, cost =>
            {
                cost.Property(x => x.Amount).IsRequired();
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}