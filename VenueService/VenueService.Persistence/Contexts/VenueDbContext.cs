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

            venue.HasMany(v => v.Theaters).WithOne().IsRequired();
        });

        modelBuilder.Entity<Theater>(theater =>
        {
            theater.ToTable("Theaters");
            theater.HasKey(t => t.Id);

            theater.Property(t => t.Name).IsRequired();
            theater.Property(t => t.Type).IsRequired();
            
            theater.HasOne(t => t.Layout).WithOne().HasForeignKey<SeatingLayout>(sl => sl.TheaterId).IsRequired();
            theater.HasMany(t => t.Sessions).WithOne().OnDelete(DeleteBehavior.Cascade).IsRequired();
        });
        
        modelBuilder.Entity<Session>(session =>
        {
            session.ToTable("Session");
            session.HasKey(s => s.Id);

            session.Property(s => s.Localization).IsRequired();
            session.Property(s => s.MovieId).IsRequired();

            session.OwnsOne(s => s.TimeRange, x =>
            {
                x.Property(tr => tr.Start).HasColumnName("StartTime").IsRequired();
                x.Property(tr => tr.End).HasColumnName("EndTime").IsRequired();
            });

            session.HasMany(s => s.Pricings).WithOne().OnDelete(DeleteBehavior.Cascade).IsRequired();
            session.HasOne(s => s.SeatingState).WithOne().HasForeignKey<SeatingState>(ss => ss.SessionId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        });

        modelBuilder.Entity<SeatingLayout>(layout =>
        {
            layout.HasKey(l => l.Id);
            
            layout.Property(l => l.Width).IsRequired();
            layout.Property(l => l.LastRow).IsRequired();

            layout.HasMany(l => l.LayoutSeats).WithOne().IsRequired();
        });
        
        modelBuilder.Entity<LayoutSeat>(layoutSeat =>
        {
            layoutSeat.HasKey(ls => ls.Id);
            
            layoutSeat.Property(ls => ls.Row).IsRequired();
            layoutSeat.Property(ls => ls.SeatNumber).IsRequired();
            layoutSeat.Property(ls => ls.SeatType).IsRequired();
        });
        
        modelBuilder.Entity<SeatingState>(state =>
        {
            state.HasKey(s => s.Id);
            state.Property(s => s.Capacity).IsRequired();

            state.HasMany(s => s.StateSeats).WithOne().OnDelete(DeleteBehavior.Cascade).IsRequired();
        });
        
        modelBuilder.Entity<StateSeat>(stateSeat =>
        {
            stateSeat.HasKey(ls => ls.Id);
            
            stateSeat.Property(ls => ls.Row).IsRequired();
            stateSeat.Property(ls => ls.SeatNumber).IsRequired();
            stateSeat.Property(ls => ls.Type).IsRequired();
            stateSeat.Property(ls => ls.Occupied).IsRequired();
        });
        
        modelBuilder.Entity<Pricing>(pricing =>
        {
            pricing.ToTable("Pricing");

            pricing.HasKey(p => p.Id);
            pricing.Property(p => p.Type).IsRequired();

            pricing.OwnsOne(p => p.Price, x =>
            {
                x.Property(pr => pr.Amount).IsRequired();
                x.Property(pr => pr.Currency).IsRequired();
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}