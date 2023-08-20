using OTPService.Application.DTOs;

namespace OTPService.Application.Communicators;

public interface IIdentityServiceCommunicator
{
    public Task<ShortSessionCreatedDto> RequestShortSessionCreation(Guid userId);
}