using Microsoft.EntityFrameworkCore;
using OTPService.Domain.Entities;

namespace OTPService.Persistence.Contexts;

public class OtpDbContext: DbContext
{
    public DbSet<OtpUser> Users { get; set; }
    
    public OtpDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OtpUser>(user =>
        {
            user.ToTable("OTPUsers");
            user.HasKey(u => u.Id);

            user.HasIndex(u => u.IssuedUserId).IsUnique();
            
            user.Property(u => u.MfaEnabled).IsRequired();
            user.Property(u => u.IsBlocked).IsRequired();
            user.Property(u => u.IsDisposed).IsRequired();

            user.Property(u => u.PrimarySecret).IsRequired();
            user.Property(u => u.PrimaryCounter).IsRequired();

            user.Property(u => u.SecondarySecret).IsRequired();
            user.Property(u => u.SecondaryCounter).IsRequired();

            user.Property(u => u.FailedAttempts).IsRequired();
            user.Property(u => u.DisposedAttempts).IsRequired();
            user.Property(u => u.BlockExpirationDate).IsRequired();
            user.Property(u => u.OtpExpirationDate).IsRequired();
            user.Property(u => u.BlockTimeout).IsRequired();
            user.Property(u => u.OtpTimeWindow).IsRequired();

            user.Property(u => u.MaxDisposals).IsRequired();
            user.Property(u => u.MaxRetries).IsRequired();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}