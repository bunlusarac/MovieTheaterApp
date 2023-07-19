using OTPService.Domain.Entities;

namespace OTPService.Application.Persistence;

public interface IOtpUserRepository: IRepositoryAsync<OtpUser>
{
    public Task<OtpUser>? GetByIssuedUserId(Guid issuedUserId);
}