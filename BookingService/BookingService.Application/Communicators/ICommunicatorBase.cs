using BookingService.Application.Communicators;

namespace BookingService.Application.Communicators;

public interface ICommunicatorBase
{
    public string ServiceName { get; }
}