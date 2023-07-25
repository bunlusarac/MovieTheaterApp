using Microsoft.EntityFrameworkCore;
using VenueService.Domain.Entities;
using VenueService.Domain.Utils;

namespace VenueService.Persistence.Contexts;

public class VenueDbContext: DbContext
{
    public DbSet<Venue> Venues { get; set; }

    public VenueDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venue>(venue =>
        {
            venue.ToTable("Venues");
            venue.HasKey(v => v.Id);

            venue.Property(v => v.Name).IsRequired();
            venue.Property(v => v.Location).IsRequired();

            venue.HasMany(v => v.Theaters);
        });

        modelBuilder.Entity<Theater>(theater =>
        {
            theater.ToTable("Theaters");
            theater.HasKey(t => t.Id);

            theater.Property(t => t.Name);
            theater.Property(t => t.Type);
            
            theater.HasOne(t => t.Layout);
            theater.HasMany(t => t.Sessions);
        });
        
        modelBuilder.Entity<Session>(session =>
        {
            session.ToTable("Session");
            session.HasKey(s => s.Id);

            session.Property(s => s.Localization);
            session.Property(s => s.MovieId);

            session.OwnsOne(s => s.TimeRange, x =>
            {
                x.Property(tr => tr.Start).HasColumnName("StartTime");
                x.Property(tr => tr.End).HasColumnName("EndTime");
            });

            session.HasMany(s => s.Pricings);
            session.HasOne(s => s.SeatingState);
        });

        modelBuilder.Entity<SeatingLayout>(layout =>
        {
            layout.HasKey(l => l.Id);
            
            layout.Property(l => l.Width);
            layout.Property(l => l.LastRow);

            layout.HasMany(l => l.LayoutSeats);
        });
        
        modelBuilder.Entity<LayoutSeat>(layoutSeat =>
        {
            layoutSeat.HasKey(ls => ls.Id);
            
            layoutSeat.Property(ls => ls.Row);
            layoutSeat.Property(ls => ls.SeatNumber);
            layoutSeat.Property(ls => ls.SeatType);
        });
        
        modelBuilder.Entity<SeatingState>(state =>
        {
            state.HasKey(s => s.Id);
            state.Property(s => s.Capacity);

            state.HasMany(s => s.StateSeats);
        });
        
        modelBuilder.Entity<StateSeat>(stateSeat =>
        {
            stateSeat.HasKey(ls => ls.Id);
            
            stateSeat.Property(ls => ls.Row);
            stateSeat.Property(ls => ls.SeatNumber);
            stateSeat.Property(ls => ls.Type);
            stateSeat.Property(ls => ls.Occupied);
        });
        
        modelBuilder.Entity<Pricing>(pricing =>
        {
            pricing.ToTable("Pricing");

            pricing.HasKey(p => p.Id);
            pricing.Property(p => p.Type);

            pricing.OwnsOne(p => p.Price, x =>
            {
                x.Property(pr => pr.Amount);
                x.Property(pr => pr.Currency);
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}