using LoyaltyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyService.Persistence.Contexts;

public class LoyaltyCustomerDbContext: DbContextBase<LoyaltyCustomer>
{
    public LoyaltyCustomerDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoyaltyCustomer>(lc =>
        {
            lc.ToTable("LoyaltyCustomers");

            lc.HasKey(c => c.Id);
            
            lc.Property(c => c.CustomerId).IsRequired();

            lc
                .HasOne(c => c.Wallet)
                .WithOne()
                .HasForeignKey<Wallet>(w => w.LoyaltyCustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            lc
                .HasMany(c => c.Redeems)
                .WithOne()
                .HasForeignKey(r => r.LoyaltyCustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<Wallet>(wallet =>
        {
            wallet.ToTable("Wallets");

            wallet.HasKey(w => w.Id);

            wallet.Property(w => w.LoyaltyCustomerId).IsRequired();
            wallet.OwnsOne(w => w.PointsBalance, pb =>
            {
                pb.Property(b => b.Amount).IsRequired();
            });
        });

        modelBuilder.Entity<Redeem>(redeem =>
        {
            redeem.ToTable("Redeems");

            redeem.HasKey(r => r.Id);

            redeem.Property(r => r.CampaignId);
            redeem.Property(r => r.RedeemDate);

            redeem.OwnsOne(r => r.Transaction, tx =>
            {
                tx.Property(t => t.Amount).IsRequired();
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}