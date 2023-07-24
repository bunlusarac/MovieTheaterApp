using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VenueService.Persistence.Contexts;

namespace VenueService.Persistence.Designtime;

public class VenueDbContextFactory: IDesignTimeDbContextFactory<VenueDbContext>
{
    public VenueDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<VenueDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=9002;Database=venuedb;User Id=postgres;Password=venue123;");

        var context = new VenueDbContext(optionsBuilder.Options);
        return context;
    }
}