using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Persistence.Contexts;

public class BookingDbContext: DbContext
{
    public DbSet<PurchaseTransaction> PurchaseTransactions { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseTransaction>(tx =>
        {
            tx.HasKey(t => t.Id);

            tx.Property(t => t.CampaignsRedeemed).IsRequired();
            tx.Property(t => t.PaymentComplete).IsRequired();
            tx.Property(t => t.SeatReserved).IsRequired();
        });

        modelBuilder.Entity<Ticket>(t =>
        {
            t.HasKey(tc => tc.Id);
            t.HasIndex(tc => tc.CustomerId);
            
            t.Property(tc => tc.Type).IsRequired();
            t.Property(tc => tc.SessionId).IsRequired();

            t.OwnsOne(tc => tc.Seat, s =>
            {
                s.Property(se => se.Number);
                s.Property(se => se.Row);
            });
        });
        
        base.OnModelCreating(modelBuilder);
    }
}