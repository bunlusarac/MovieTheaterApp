using BookingService.Domain.Utils;

namespace BookingService.Application.Communicators;

public interface IPaymentServiceCommunicator
{
    public void Checkout(decimal Amount, Currency currency);
}