using OTPService.Domain.Common;

namespace OTPService.Domain.Entities;

public class OtpUser: AggregateRoot
{
    public Guid IssuedUserId;
    public bool MfaEnabled;
    
    public byte[] PrimarySecret;
    public long PrimaryCounter;
    
    public byte[] SecondarySecret;
    public long SecondaryCounter;
    
    public bool IsBlocked;
    public bool IsDisposed;
    
    public DateTime BlockExpirationDate;
    public DateTime OtpExpirationDate;
    
    public int FailedAttempts;
    public int DisposedAttempts;

    //Configuration values
    public int MaxRetries;
    public int MaxDisposals;
    
    public TimeSpan OtpTimeWindow;
    public TimeSpan BlockTimeout;
    
    public OtpUser(Guid issuedUserId, byte[] primarySecret, byte[] secondarySecret, int maxRetries, int maxDisposals, TimeSpan otpTimeWindow, TimeSpan blockTimeout)
    {
        IssuedUserId = issuedUserId;
        PrimarySecret = primarySecret;
        SecondarySecret = secondarySecret;

        MfaEnabled = false;
        IsBlocked = false;
        IsDisposed = false;
        
        BlockExpirationDate = DateTime.MinValue;
        OtpExpirationDate = DateTime.MinValue;
        
        PrimaryCounter = 0;
        SecondaryCounter = 0;
        FailedAttempts = 0;
        
        MaxRetries = maxRetries;
        MaxDisposals = maxDisposals;
        OtpTimeWindow = otpTimeWindow;
        BlockTimeout = blockTimeout;
    }

    public OtpUser()
    {
    }

    public int IncrementFailedAttempts()
    {
        ++FailedAttempts;

        if (FailedAttempts > MaxRetries)
        {
            Dispose();
        }

        return FailedAttempts;
    }

    public void Dispose()
    {
        IsDisposed = true;
        ++DisposedAttempts;

        if (DisposedAttempts > MaxDisposals)
        {
            Block();
        }
    }

    public void Unblock()
    {
        IsBlocked = false;
        BlockExpirationDate = DateTime.MinValue;
        DisposedAttempts = 0;
    }

    public void Block()
    {
        if (!IsBlocked)
        {
            IsBlocked = true;
            BlockExpirationDate = DateTime.UtcNow.Add(BlockTimeout);
        }
    }

    public void IncrementCounters()
    {
        ++PrimaryCounter;
        if (MfaEnabled) ++SecondaryCounter;
    }
    
    public void RequestNewOtp()
    {
        if (IsBlocked)
        {
            if (DateTime.UtcNow >= BlockExpirationDate)
            {
                Unblock();
            }
            else
            {
                //TODO custom exception
                throw new Exception();
            }
        }
   
        IncrementCounters();
        FailedAttempts = 0;
        IsDisposed = false;
        OtpExpirationDate = DateTime.UtcNow.Add(OtpTimeWindow);
    }
    
    public void SwitchMfaStatus(bool mfaEnabled)
    {
        MfaEnabled = mfaEnabled;
    }
    
    public void ValidateOtp()
    {
        if (IsDisposed)
            throw new Exception();

        if (DateTime.UtcNow >= OtpExpirationDate)
            throw new Exception();
        
        IsDisposed = true;
        DisposedAttempts = 0;
    }
}