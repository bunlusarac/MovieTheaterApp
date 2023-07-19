using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OTPService.Persistence.Contexts;

namespace OTPService.Persistence.esigntime;

public class OtpDbContextFactory: IDesignTimeDbContextFactory<OtpDbContext>
{
    public OtpDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OtpDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=9001;Database=otpdb;User Id=postgres;Password=otp123;");

        var context = new OtpDbContext(optionsBuilder.Options);
        return context;
    }
}