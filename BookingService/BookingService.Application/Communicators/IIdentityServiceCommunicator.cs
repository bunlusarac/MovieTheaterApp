using BookingService.Application.DTOs;

namespace BookingService.Application.Communicators;

public interface IIdentityServiceCommunicator: ICommunicatorBase
{
    public Task<UserInfoDto> SendGetUserInfoRequest(string bearer);
}