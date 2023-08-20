using BookingService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookingService.Persistence.Designtime;

public class BookingDbContextFactory: IDesignTimeDbContextFactory<BookingDbContext>
{
    public BookingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookingDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=9005;Database=bookingdb;User Id=postgres;Password=booking123;");

        var context = new BookingDbContext(optionsBuilder.Options);
        return context;
    }
}